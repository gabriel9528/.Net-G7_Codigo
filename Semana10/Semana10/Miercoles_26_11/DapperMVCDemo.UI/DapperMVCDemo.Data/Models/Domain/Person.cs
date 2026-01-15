using System.ComponentModel.DataAnnotations;

namespace DapperMVCDemo.Data.Models.Domain
{
    public class Person
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Address { get; set; }
    }
}
