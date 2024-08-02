namespace MedUnify.HealthPulseAPI.Repositories.Interface
{
    using MedUnify.Domain.HealthPulse;

    public interface IPatientRepository
    {
        Task<IEnumerable<Patient>> GetPatientsAsync();

        Task<Patient> GetPatientByIdAsync(int id);

        Task AddPatientAsync(Patient patient);

        Task UpdatePatientAsync(Patient patient);

        Task DeletePatientAsync(int id);
    }
}