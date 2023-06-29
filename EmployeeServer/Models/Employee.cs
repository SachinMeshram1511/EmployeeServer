using System; 
using System.ComponentModel.DataAnnotations;

namespace EmployeeServer.Models
{
    public class Employee
    {
        [Key]
        public int? CustomerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string CountryCode { get; set; }
        public string Gender { get; set; }
        public decimal Balance { get; set; }
    }
}
