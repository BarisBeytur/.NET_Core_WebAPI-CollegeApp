using CollegeApp.Validators;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace CollegeApp.Models
{
    public class StudentDTO
    {
        
        public int Id { get; set; }

        [Required(ErrorMessage = "Student name is required")]
        [StringLength(30)]
        public string StudentName { get; set; }

        [EmailAddress(ErrorMessage ="Please enter valid email address")]
        public string Email { get; set; }

        [Required]
        public string Address { get; set; }

        //[DateCheck]
        //public DateTime AdmissionDate { get; set; }
    }
}
