using Microsoft.EntityFrameworkCore;
using ReadApi.Application;
using ReadApi.Core.Entities;

namespace ReadApi.Infrastructure.Peristence.EfCore
{
    public class AppDbContext : DbContext, IAppDbContext
    {
      
        public AppDbContext(DbContextOptions<AppDbContext> options)
      : base(options)
        {
        }
        public DbSet<Violation> Violations { get; set; }
       // public DbSet<Media> Media => Set<Media>();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder
            .UseNpgsql()
         //  .UseLazyLoadingProxies()
            .UseSnakeCaseNamingConvention();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Violation>(entity =>
            {
                entity.HasKey(e => e.VioId);
                entity.Property(e => e.ClientId).IsRequired();
                entity.Property(e => e.Location).HasMaxLength(50);
                entity.Property(e => e.VehicleSpeed).HasPrecision(10, 3);

                entity.HasMany(e => e.Medias)
                .WithOne(e=>e.Violation)
                .HasForeignKey(s=>s.VioId)
                .OnDelete(DeleteBehavior.Cascade);

                entity.ToTable("violation");
            });

            modelBuilder.Entity<Media>(entity =>
            {
                entity.HasKey(e => e.MediaId);
            });

            //modelBuilder.Entity<Media>(entity =>
            //{
            //    entity.HasKey(e => e.PKId);
            //    entity.HasOne<Violation>(s => s.Violation)
            //   .WithMany(g => g.Medias)
            //   .HasForeignKey(s => s.ViolationId);


            //});
            base.OnModelCreating(modelBuilder);
        }
    }
}
