using System.ComponentModel.DataAnnotations;
namespace Mails.API.Models
{
    public class Users
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
    }
}
