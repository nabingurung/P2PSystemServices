using Microsoft.EntityFrameworkCore;
using ReadApi.Models;

namespace ReadApi.Peristence
{
    public class ViolationDbContext : DbContext
    {
        public ViolationDbContext(DbContextOptions<ViolationDbContext> dbContextOptions) : base (dbContextOptions){ }
            
        public DbSet<Violation> ViolationSet { get; set; }
        public DbSet<Media> Media { get; set; }
      

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
          base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            /*
            modelBuilder.Entity<Violation>(v =>
            {
                v.HasKey(e => e.Id);
                v.Property(e => e.Id).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<Media>()
                      .HasOne<Violation>(s => s.Violation)
                      .WithMany(g => g.Medias)
                      .HasForeignKey(s => s.ViolationId);
            modelBuilder.Entity<Media>( m =>
            {
                m.HasKey(e => e.Id);
                m.Property(e =>e.Id).ValueGeneratedOnAdd();
            });                      
            */
            base.OnModelCreating(modelBuilder);
        }
        public override int SaveChanges()
        {
           // ChangeTracker.DetectChanges();
            return base.SaveChanges();
        }

    }
}
