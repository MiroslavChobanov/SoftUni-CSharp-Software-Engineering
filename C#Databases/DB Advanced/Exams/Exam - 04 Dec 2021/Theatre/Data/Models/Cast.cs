using System.ComponentModel.DataAnnotations;

namespace Theatre.Data.Models
{
    public class Cast
    {
        public int Id { get; set; }
        [Required]
        [MinLength(4)]
        [MaxLength(30)]
        public string FullName { get; set; }
        public bool IsMainCharacter { get; set; }
        public string PhoneNumber { get; set; }
        [Required]
        [RegularExpression(@"^[+][4]{2}[-][0-9]{2}[-][0-9]{3}[-][0-9]{4}$")]
        public int PlayId { get; set; }
        public Play Play { get; set; }
    }
}