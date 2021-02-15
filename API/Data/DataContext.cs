using Microsoft.EntityFrameworkCore;
using API.Models;

namespace API.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {}
        
        public DbSet<User> Users { get; set; }  
        public DbSet<Chat> Chats { get; set; }  
        public DbSet<Message> Messages { get; set; }  
        public DbSet<ChatUser> ChatUsers { get; set; }  

        protected override void OnModelCreating(ModelBuilder builder) 
        {
            base.OnModelCreating(builder);
             
            builder.Entity<ChatUser>()
            .HasKey(x=> new { x.ChatId, x.UserId});
        }
    }
}