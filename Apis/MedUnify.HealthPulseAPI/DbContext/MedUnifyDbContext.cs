namespace MedUnify.HealthPulseAPI.DbContext
{
    using MedUnify.Domain.Auth;
    using MedUnify.Domain.HealthPulse;
    using Microsoft.EntityFrameworkCore;

    public class MedUnifyDbContext : DbContext
    {
        public MedUnifyDbContext(DbContextOptions<MedUnifyDbContext> options) : base(options)
        {
        }

        public DbSet<Patient> Patients { get; set; }
        public DbSet<Visit> Visits { get; set; }
        public DbSet<ProgressNote> ProgressNotes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure Patient entity
            modelBuilder.Entity<Patient>()
                .HasKey(p => p.PatientId);

            modelBuilder.Entity<Patient>()
                .HasMany(p => p.Visits)
                .WithOne(v => v.Patient)
                .HasForeignKey(v => v.PatientId);

            // Configure Visit entity
            modelBuilder.Entity<Visit>()
                .HasKey(v => v.VisitId);

            modelBuilder.Entity<Visit>()
                .HasMany(v => v.ProgressNotes)
                .WithOne(pn => pn.Visit)
                .HasForeignKey(pn => pn.VisitId);

            // Configure ProgressNote entity
            modelBuilder.Entity<ProgressNote>()
                .HasKey(pn => pn.ProgressNoteId);

            base.OnModelCreating(modelBuilder);
        }
    }
}