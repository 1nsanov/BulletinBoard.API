using BulletinBoard.API.EntityDB.Models;
using Microsoft.EntityFrameworkCore;

namespace BulletinBoard.API.EntityDB
{
    public sealed class DataBaseContext : DbContext
    {
        private const string NameServer = "DESKTOP-OL7HB4A";
        private const string NameDb = "BulletinBoardDB";

        public DbSet<Town>? Towns { get; set; } = null!;
        public DbSet<Category>? Categories { get; set; } = null!;
        public DbSet<SubCategory> ? SubCategories { get; set; } = null!;
        public DbSet<Advertisement>? Advertisements { get; set; } = null!;
        public DbSet<User>? Users { get; set; } = null!;

        public DataBaseContext()
        {
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@$"Server={NameServer};Database={NameDb};Trusted_Connection=True;");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
