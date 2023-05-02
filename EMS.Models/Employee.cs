using System.ComponentModel.DataAnnotations;

namespace EMS.Models
{
    public class Employee
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string? Name { get; set; }
        [EmailAddress]
        public string? Email { get; set; }
        public DateTime DOB { get; set; }
        public string? Department { get; set; }
    }
}
