using Mails.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Mails.API.Data
{
    public class MailsDbContext : DbContext
    {
        public MailsDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Letters> Letter { get; set; }
        public DbSet<Topics> Topic { get; set; }
        public DbSet<Users> User { get; set; }
    }
}
