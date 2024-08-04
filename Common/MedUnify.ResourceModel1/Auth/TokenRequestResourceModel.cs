namespace MedUnify.ResourceModel.Auth
{
    using FluentValidation;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class TokenRequestResourceModel
    {
        [Required]
        public string ClientId { get; set; }

        [Required]
        public string ClientSecret { get; set; }
    }

    public class TokenRequestResourceModelValidator : AbstractValidator<TokenRequestResourceModel>
    {
        public TokenRequestResourceModelValidator()
        {
            RuleFor(x => x.ClientId).NotEmpty().WithMessage("ClientId is required.");
            RuleFor(x => x.ClientSecret).NotEmpty().WithMessage("ClientSecret is required.");
        }
    }
}