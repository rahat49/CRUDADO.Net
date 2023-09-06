using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CRUDADOdotNet.Models
{
    public class Employee
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [DisplayName("Fist Name")]
        public string FirstName { get; set; }
        [Required]
        [DisplayName("Last Name")]
        public string LastName { get; set; }
        [Required]
        [DisplayName("Date Of Birth")]
        public DateTime DateofBirth { get; set; }
        [Required]
        [DisplayName("E-Mail")]
        public string Email { get; set; }
        [Required]
        public double Salary { get; set; }
        [NotMapped]
        public string FullName
        { get { return FirstName + " " + LastName;  }  }

    }
}
