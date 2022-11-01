using Microsoft.EntityFrameworkCore;
using senddatatest.Models;

namespace senddatatest.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Register> Registers { get; set; }
        public DbSet<Recorddata> Recorddatas { get; set; }
    }
}
