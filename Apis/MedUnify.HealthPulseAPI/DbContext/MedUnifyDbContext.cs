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
        public DbSet<ProgressNote> ProgressNotes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Patient>()
                .HasKey(p => p.Id);

            modelBuilder.Entity<ProgressNote>()
                .HasKey(pn => pn.NoteId);

            modelBuilder.Entity<ProgressNote>()
                .HasOne(pn => pn.Patient)
                .WithMany(p => p.ProgressNotes)
                .HasForeignKey(pn => pn.PatientId);

            base.OnModelCreating(modelBuilder);
        }
    }
}