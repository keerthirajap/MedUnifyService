namespace MedUnify.HealthPulseAPI.Services.Concrete
{
    using MedUnify.Domain.HealthPulse;
    using MedUnify.HealthPulseAPI.Repositories.Interface;
    using MedUnify.HealthPulseAPI.Services.Interface;

    public class PatientService : IPatientService
    {
        private readonly IPatientRepository _patientRepository;

        public PatientService(IPatientRepository patientRepository)
        {
            _patientRepository = patientRepository;
        }

        public async Task<IEnumerable<Patient>> GetAllPatientsAsync()
        {
            return await _patientRepository.GetPatientsAsync();
        }

        public async Task<Patient> GetPatientByIdAsync(int id)
        {
            return await _patientRepository.GetPatientByIdAsync(id);
        }

        public async Task AddPatientAsync(Patient patient)
        {
            await _patientRepository.AddPatientAsync(patient);
        }

        public async Task UpdatePatientAsync(Patient patient)
        {
            await _patientRepository.UpdatePatientAsync(patient);
        }

        public async Task DeletePatientAsync(int id)
        {
            await _patientRepository.DeletePatientAsync(id);
        }
    }
}