using AuditQueue.DbModels;
using Microsoft.EntityFrameworkCore;

namespace AuditQueue.Persistence
{
    public class ViolationDbContext: DbContext
    {
        public ViolationDbContext(DbContextOptions dbContextOptions): base(dbContextOptions) { }
       
        public DbSet<Violation> ViolationSet { get; set;}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Violation>()
            //    .HasMany(v => v.Medias)
            //    .WithOne(v => v.ViolationReferenceRecord);

            modelBuilder.Entity<Media>()
              .HasOne<Violation>(s => s.Violation)
              .WithMany(g => g.Medias)
              .HasForeignKey(s => s.ViolationId);


            base.OnModelCreating(modelBuilder);
        }
    }
}
