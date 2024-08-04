namespace MedUnify.HealthPulseAPI.Services.Interface
{
    using MedUnify.Domain.HealthPulse;

    public interface IPatientService
    {
        Task<IEnumerable<Patient>> GetAllPatientsAsync(int organizationId);

        Task<Patient> GetPatientByIdAsync(int patientId);

        Task AddPatientAsync(Patient patient);

        Task UpdatePatientAsync(Patient patient);

        Task DeletePatientAsync(int patientId);
    }
}