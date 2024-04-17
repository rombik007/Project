using System.ComponentModel.DataAnnotations;
namespace Project.Models
{
    public class Employee
    {
        [Key] public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string phone { get; set; }
        public string Country { get; set; }
    }
}
