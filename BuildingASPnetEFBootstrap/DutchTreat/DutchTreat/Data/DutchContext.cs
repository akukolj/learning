
using DutchTreat.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace DutchTreat.Data
{
    public class DutchContext : DbContext
    {
        private readonly DbContextOptions<DutchContext> _options;

        public DutchContext(DbContextOptions<DutchContext> options) : base(options)
        {
            _options = options;
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
    }
}
