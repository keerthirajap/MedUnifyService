namespace MedUnify.HealthPulseAPI.Repositories.Concrete
{
    using System.Collections.Generic;
    using System.Data;
    using System.Threading.Tasks;
    using MedUnify.Domain.HealthPulse;
    using MedUnify.HealthPulseAPI.DbContext;
    using MedUnify.HealthPulseAPI.Repositories.Interface;
    using Microsoft.EntityFrameworkCore;

    public class PatientRepository : IPatientRepository
    {
        private readonly MedUnifyDbContext _context;

        public PatientRepository(MedUnifyDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Patient>> GetPatientsAsync()
        {
            return await _context.Patients.Where(p => !p.IsDeleted).ToListAsync();
        }

        public async Task<Patient> GetPatientByIdAsync(int id)
        {
            return await _context.Patients.Include(p => p.ProgressNotes)
                                          .FirstOrDefaultAsync(p => p.Id == id && !p.IsDeleted);
        }

        public async Task AddPatientAsync(Patient patient)
        {
            await _context.Patients.AddAsync(patient);
            await _context.SaveChangesAsync();
        }

        public async Task UpdatePatientAsync(Patient patient)
        {
            _context.Patients.Update(patient);
            await _context.SaveChangesAsync();
        }

        public async Task DeletePatientAsync(int id)
        {
            var patient = await _context.Patients.FindAsync(id);
            if (patient != null)
            {
                patient.IsDeleted = true;
                await _context.SaveChangesAsync();
            }
        }
    }
}