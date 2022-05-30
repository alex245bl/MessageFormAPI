using System.ComponentModel.DataAnnotations;

namespace Mails.API.Models
{
    public class Topics
    {
        [Key]
        public Guid Id { get; set; }
        public string Topic { get; set; }
    }
}
