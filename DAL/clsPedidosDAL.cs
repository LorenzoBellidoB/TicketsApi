﻿using ENT;
using Microsoft.EntityFrameworkCore;

namespace DAL
{
    public class clsPedidosDAL
    {
        private readonly AppDbContext _context;

        public clsPedidosDAL(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<clsPedido>> ObtenerPedidos()
        {
            return await _context.Pedidos.Include(p => p.Cliente)
                .Include(p => p.Empresa)
                .Include(p => p.Dependiente)
                .ToListAsync();
        }

        public async Task<List<clsPedido>> ObtenerPedidosPorIdEmpresaEntregados(int idEmpresa)
        {
            return await _context.Pedidos.Include(p => p.Cliente)
                .Include(p => p.Empresa)
                .Include(p => p.Dependiente)
                .Where(p => p.Entregado && p.IdEmpresa == idEmpresa)
                .ToListAsync();
        }

        public async Task<List<clsPedido>> ObtenerPedidosPorIdEmpresaNoEntregados(int idEmpresa)
        {
            return await _context.Pedidos.Include(p => p.Cliente)
                .Include(p => p.Empresa)
                .Include(p => p.Dependiente)
                .Where(p => !p.Entregado && p.IdEmpresa == idEmpresa)
                .ToListAsync();
        }

        public async Task<clsPedido> ObtenerPedidoPorId(int id)
        {
            return await _context.Pedidos
                .Include(p => p.Cliente)
                .Include(p => p.Empresa)
                .Include(p => p.Dependiente)
                .FirstOrDefaultAsync(p => p.IdPedido == id);
        }

        public async Task<List<clsPedido>> ObtenerPedidosPorIdEmpresa(int idEmpresa)
        {
            return await _context.Pedidos
                .Include(p => p.Empresa)
                .Where(p => p.IdEmpresa == idEmpresa)
                .ToListAsync();
        }

        public async Task<bool> EntregarAsync(int id)
        {
            var pedido = await _context.Pedidos.FindAsync(id);
            if (pedido == null || pedido.Entregado)
                return false;

            pedido.Entregado = true;
            await _context.SaveChangesAsync();
            return true;
        }


        public async Task<int> InsertarPedido(clsPedido pedido)
        {
            _context.Pedidos.Add(pedido);
            await _context.SaveChangesAsync();
            return pedido.IdPedido;
        }

        public async Task<bool> ActualizarPedido(clsPedido pedido)
        {
            var original = await _context.Pedidos
                .FirstOrDefaultAsync(p => p.IdPedido == pedido.IdPedido);

            if (original == null) return false;

            original.IdCliente = pedido.IdCliente;
            original.IdDependiente = pedido.IdDependiente;
            original.FechaCreado = pedido.FechaCreado;
            original.FechaEntrega = pedido.FechaEntrega;
            original.IdEmpresa = pedido.IdEmpresa;
            original.Descripcion = pedido.Descripcion;
            original.Entregado = pedido.Entregado;

            return await _context.SaveChangesAsync() > 0;
        }


        public async Task<bool> EliminarPedido(int id)
        {
            var pedido = await _context.Pedidos.FindAsync(id);

            if (pedido == null || pedido.DeletedAt != DateTime.SpecifyKind(DateTime.Parse("1111-01-01T00:00:00Z"), DateTimeKind.Utc))
                return false;

            pedido.DeletedAt = DateTime.UtcNow;
            _context.Pedidos.Update(pedido);

            return await _context.SaveChangesAsync() > 0;
        }
    }
}
