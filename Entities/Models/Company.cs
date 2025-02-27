
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    public class Company
    {

        [Column("CompanyId")]
        public Guid Id {get; set;}

        [Required(ErrorMessage = "Company's name is a required field")]
        [MaxLength(60, ErrorMessage = "Maximum character length for company's name is 60")]
        public string? Name {get; set;}
        
        [Required(ErrorMessage = "Company's  address is a required field")]
        [MaxLength(60, ErrorMessage = "Maximum character length for company's addres is 60")]
        public string? Address {get; set;}

        public string? Country {get; set;}

        public ICollection<Employee>? Employees {get; set;} 

        
    }
}