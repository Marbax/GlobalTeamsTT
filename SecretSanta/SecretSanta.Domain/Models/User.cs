using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SecretSanta.Domain.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 2)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 2)]
        public string LastName { get; set; }

        public IList<SecretSantaPair> GiveTo { get; set; }
        public IList<SecretSantaPair> RecieveFrom { get; set; }
    }
}
