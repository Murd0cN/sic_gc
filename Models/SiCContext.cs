using Microsoft.EntityFrameworkCore;

namespace SiC_GC.Models
{
    public class SiCContext : DbContext
    {
        public SiCContext(DbContextOptions<SiCContext> options) : base(options) { }

        public DbSet<Product> Products { get; set; }

    }

}