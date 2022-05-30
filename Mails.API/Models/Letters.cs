using System.ComponentModel.DataAnnotations;
namespace Mails.API.Models
{
    public class Letters
    {
        [Key]
        public Guid  Id { get; set; }

        public string IdUser{get; set;}
        public string Topic { get; set; }
        public string Message { get; set; }
    }
}
