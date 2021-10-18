using Chat.Models;
using Microsoft.EntityFrameworkCore;

namespace Chat.Chat
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
            
        }

        public DbSet<Message> Chat { get; set; }
        public DbSet<Group> ChatGroup { get; set; }
        public DbSet<UsersGroup> UsersGroup { get; set; }
    }
}
