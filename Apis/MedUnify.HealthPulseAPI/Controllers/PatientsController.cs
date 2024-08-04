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
            // Extract the OrganizationId from the token claims
            var organizationIdClaim = User.FindFirst("OrganizationId");

            if (organizationIdClaim == null)
            {
                return Unauthorized("OrganizationId not found in token.");
            }

            // Convert the organizationIdClaim value to an integer
            if (!int.TryParse(organizationIdClaim.Value, out int organizationId))
            {
                return BadRequest("Invalid OrganizationId in token.");
            }

            // Fetch patients based on the extracted OrganizationId
            var patients = await _patientService.GetAllPatientsAsync(organizationId);
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
            return CreatedAtAction(nameof(GetPatient), new { id = patient.PatientId }, patient);
        }

        [Route("UpdatePatient")]
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> UpdatePatient(int id, [FromBody] Patient patient)
        {
            if (id != patient.PatientId)
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