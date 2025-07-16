using ENT;
using Microsoft.EntityFrameworkCore;

namespace DAL
{
    public class AppDbContext : DbContext
    {
        public DbSet<clsProducto> Productos { get; set; }
        public DbSet<clsTicket> Tickets { get; set; }
        public DbSet<clsDetalleTicket> DetallesTicket { get; set; }
        public DbSet<clsDependiente> Dependientes { get; set; }
        public DbSet<clsAlbaran> Albaranes { get; set; }
        public DbSet<clsCliente> Clientes { get; set; }
        public DbSet<clsEmpresa> Empresas { get; set; }
        public DbSet<clsProveedor> Proveedores { get; set; }
        public DbSet<clsAlbaranDetalle> AlbaranesDetalles { get; set; }
        public DbSet<clsProductoUnidad> ProductosUnidades { get; set; }

        public DbSet<clsPedido> Pedidos { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }
    }

}
