using System.ComponentModel.DataAnnotations;

namespace SecretSanta.Domain.Models
{
    public class SecretSantaPair
    {
        public int Id { get; set; }

        [Required]
        public int GiverId { get; set; }
        public virtual User Giver { get; set; }
        
        [Required]
        public int ReceiverId { get; set; }
        public virtual User Receiver { get; set; }
    }
}
