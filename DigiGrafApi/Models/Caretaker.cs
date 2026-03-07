using System.ComponentModel.DataAnnotations;

namespace DigiGrafWeb.Models
{
    public class Caretaker
    {
        [Key]
        public Guid? Id { get; set; } = Guid.NewGuid();
        public string DisplayName { get; set; } = null!;
        public string Email { get; set; } = null!;
    }
}
