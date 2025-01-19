using System.ComponentModel.DataAnnotations;

namespace Shared.DataTransferObjects{
    public abstract record CompanyManipulationDto
    {

        [Required(ErrorMessage = "Company name is a required field.")]
        [MaxLength(30, ErrorMessage = "Maximum length for the Name is 30 characters.")] 
        public string? Name { get; init; }

        [Required(ErrorMessage = "Address is a required field.")]
        [MaxLength(60, ErrorMessage = "Maximum length for the address is 30 characters.")]
        public string? Address { get; init; } 

        [Required(ErrorMessage = "Country is a required field.")]
        [MaxLength(20, ErrorMessage = "Maximum length for the country is 20 characters.")]
        public string? Country { get; init; } 
        
    }

}