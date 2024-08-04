﻿namespace MedUnify.HealthPulseAPI.Controllers
{
    using MedUnify.Domain.HealthPulse;
    using MedUnify.HealthPulseAPI.Infrastructure.Filters;
    using MedUnify.HealthPulseAPI.Infrastructure.Handlers;
    using MedUnify.HealthPulseAPI.Services.Interface;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PatientsController : ControllerBase
    {
        private readonly IPatientService _patientService;
        private readonly IOrganizationHandler _organizationHandler;

        public PatientsController(IPatientService patientService, IOrganizationHandler organizationHandler)
        {
            _patientService = patientService;
            _organizationHandler = organizationHandler;
        }

        [Route("GetPatients")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<Patient>))]
        public async Task<ActionResult<List<Patient>>> GetPatients()
        {
            // Use the handler to get the OrganizationId
            int organizationId = _organizationHandler.GetOrganizationIdFromToken(User);

            // Fetch patients based on the extracted OrganizationId
            var patients = await _patientService.GetAllPatientsAsync(organizationId);
            return Ok(patients);
        }

        [Route("GetPatient")]
        [HttpGet]
        [ServiceFilter(typeof(OrganizationIdValidationFilter))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Patient))]
        public async Task<ActionResult<Patient>> GetPatient(int patientId)
        {
            var patient = await _patientService.GetPatientByIdAsync(patientId);
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
            // Validate the model state
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Use the handler to get the OrganizationId
            int organizationId = _organizationHandler.GetOrganizationIdFromToken(User);
            patient.OrganizationId = organizationId;

            await _patientService.AddPatientAsync(patient);
            return CreatedAtAction(nameof(GetPatient), new { id = patient.PatientId }, patient);
        }

        [Route("UpdatePatient")]
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ServiceFilter(typeof(OrganizationIdValidationFilter))]
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