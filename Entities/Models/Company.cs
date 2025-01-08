using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class Company
    {

        [Column("EmployeeID")]
        public Guid Id {get; set;}

        [Required(ErrorMessage = "Employee's name is a required field")]
        [MaxLength(60, ErrorMessage = "Maximum character length for Employee's name is 60")]
        public string? Name {get; set;}

        [Required(ErrorMessage = "Employee's age is a required field")]
        public int? Age {get; set;}


        [Required(ErrorMessage = "Position is a required field")]
        [MaxLength(60, ErrorMessage = "Maximum character length for Employee's name is 60")]
        public string? Position {get; set;}

        public int 
        public Company()
        {
            
        }
        
        public ICollection<Employee> Employees {get; set;} 

    }
}