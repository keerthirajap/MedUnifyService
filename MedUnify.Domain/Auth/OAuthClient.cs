namespace MedUnify.Domain.Auth
{
    using MedUnify.Domain.HealthPulse;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class OAuthClient
    {
        public int Id { get; set; }
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public int OrganizationId { get; set; }
    }

    //public class Organization
    //{
    //    public int OrganizationId { get; set; }
    //    public string ClientId { get; set; }
    //    public string ClientSecret { get; set; }

    //    public string Name { get; set; }

    //    // Navigation property for Patients
    //    public ICollection<PatientOrganization> PatientOrganizations { get; set; }
    //}
}