using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GPKadryPlace.Model
{
    [Table("employee", Schema = "dbo")]
    public class Employee
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID_employee { get; set; }

        [Required]
        [MaxLength(30)]
        public string EmployeeName { get; set; }

        [Required]
        public DateTime MonthlyWorkHours { get; set; }

        [Required]
        public bool Available { get; set; } = true;
    }
}