using Microsoft.EntityFrameworkCore;
using API_STPS.Models;

namespace API_STPS.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Normas> Normas { get; set; }

    }
}
