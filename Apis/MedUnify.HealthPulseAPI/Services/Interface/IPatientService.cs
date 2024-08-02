namespace MedUnify.HealthPulseAPI.Services.Interface
{
    using MedUnify.Domain.HealthPulse;

    public interface IPatientService
    {
        Task<IEnumerable<Patient>> GetAllPatientsAsync();

        Task<Patient> GetPatientByIdAsync(int id);

        Task AddPatientAsync(Patient patient);

        Task UpdatePatientAsync(Patient patient);

        Task DeletePatientAsync(int id);
    }
}