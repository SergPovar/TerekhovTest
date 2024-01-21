using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using TerekhovTest.Models;
using TestTerekhov.Models;

namespace TerekhovTest.Data
{
    public class EmailDbContext : DbContext
    {
       
        public EmailDbContext()
        {

        }

        public EmailDbContext(DbContextOptions<EmailDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
        public DbSet<EmailDB> EmailDB { get; set; }
    }
}
