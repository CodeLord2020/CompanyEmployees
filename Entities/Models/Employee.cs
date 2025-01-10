using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Entities.Models
{
    
    public class Employee
    {

        [Column("EmployeeId")]
        public Guid Id {get; set;}

        [Required(ErrorMessage = "Employee's name is a required field")]
        [MaxLength(60, ErrorMessage = "Maximum character length for Employee's name is 60")]
        public string? Name {get; set;}

        [Required(ErrorMessage = "Employee's age is a required field")]
        public int? Age {get; set;}


        [Required(ErrorMessage = "Position is a required field")]
        [MaxLength(60, ErrorMessage = "Maximum character length for Employee's name is 60")]
        public string? Position {get; set;}

        
        [ForeignKey(nameof(Company))]
        public Guid CompanyId {get; set;}

        public Company? Company {get; set;}
        
    }
}