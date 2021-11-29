using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using VaporStore.Data.Models.Enums;

namespace VaporStore.Data.Models
{
    public class Card
    {
        public int Id { get; set; }
        [Required]
        [RegularExpression(@"^.*\b(\d{4})\b.*\b(\d{4})\b.*\b(\d{4})\b.*\b(\d{4})\b.*$")]
        public string Number { get; set; }
        [Required]
        [RegularExpression(@"\d{3}$")]
        public string Cvc { get; set; }
        public CardType Type { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public ICollection<Purchase> Purchases { get; set; } = new HashSet<Purchase>();
    }
}

