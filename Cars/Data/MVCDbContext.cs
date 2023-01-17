using Cars.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace Cars.Data

{
    public class MVCDbContext : DbContext
    {
        public MVCDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Car> Cars { get; set; }
    }
}
