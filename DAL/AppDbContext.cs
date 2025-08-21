using ENT;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

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
        public DbSet<clsServicio> Servicios { get; set; }

        public DbSet<clsPedido> Pedidos { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                if (typeof(SoftDeletableEntity).IsAssignableFrom(entityType.ClrType))
                {
                    var parameter = Expression.Parameter(entityType.ClrType, "e");
                    var deletedAtProperty = Expression.Property(parameter, nameof(SoftDeletableEntity.DeletedAt));

                    var notDeletedValue = Expression.Constant(
                        new DateTime(1111, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                        typeof(DateTime)
                    );

                    var filter = Expression.Equal(deletedAtProperty, notDeletedValue);
                    var lambda = Expression.Lambda(filter, parameter);

                    modelBuilder.Entity(entityType.ClrType).HasQueryFilter(lambda);
                }
            }
        }

    }

}
