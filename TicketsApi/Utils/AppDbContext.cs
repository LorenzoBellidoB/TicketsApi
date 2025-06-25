using ENT;
using Microsoft.EntityFrameworkCore;

namespace TicketsApi.Utils
{
    public class AppDbContext : DbContext
    {
        public DbSet<clsProducto> Productos { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }
    }

}
