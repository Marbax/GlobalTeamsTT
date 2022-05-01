using Microsoft.EntityFrameworkCore;
using SecretSanta.Domain.Models;

namespace SecretSanta.Domain.Context
{
    public class SecretSantaDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public SecretSantaDbContext(DbContextOptions<SecretSantaDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("User");
            modelBuilder.Entity<SecretSantaPair>().ToTable("SecretSantaPair");

            modelBuilder.Entity<SecretSantaPair>()
                .HasOne(sc => sc.Giver)
                .WithMany(s => s.RecieveFrom)
                .HasForeignKey(sc => sc.GiverId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<SecretSantaPair>()
                .HasOne(sc => sc.Receiver)
                .WithMany(s => s.GiveTo)
                .HasForeignKey(sc => sc.ReceiverId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
