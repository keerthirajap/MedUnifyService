namespace MedUnify.HealthPulseAPI.Controllers
{
    using MedUnify.Domain.HealthPulse;
    using MedUnify.HealthPulseAPI.Services.Interface;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PatientsController : ControllerBase
    {
        private readonly IPatientService _patientService;

        public PatientsController(IPatientService patientService)
        {
            _patientService = patientService;
        }

        [Route("GetPatients")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<Patient>))]
        public async Task<ActionResult<List<Patient>>> GetPatients()
        {
            var patients = await _patientService.GetAllPatientsAsync();
            return Ok(patients);
        }

        [Route("GetPatient")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Patient))]
        public async Task<ActionResult<Patient>> GetPatient(int id)
        {
            var patient = await _patientService.GetPatientByIdAsync(id);
            if (patient == null)
            {
                return NotFound();
            }
            return Ok(patient);
        }

        [Route("AddPatient")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Patient))]
        public async Task<ActionResult> AddPatient([FromBody] Patient patient)
        {
            await _patientService.AddPatientAsync(patient);
            return CreatedAtAction(nameof(GetPatient), new { id = patient.Id }, patient);
        }

        [Route("UpdatePatient")]
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> UpdatePatient(int id, [FromBody] Patient patient)
        {
            if (id != patient.Id)
            {
                return BadRequest();
            }

            await _patientService.UpdatePatientAsync(patient);
            return NoContent();
        }

        [Route("DeletePatient")]
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> DeletePatient(int id)
        {
            await _patientService.DeletePatientAsync(id);
            return NoContent();
        }
    }
}