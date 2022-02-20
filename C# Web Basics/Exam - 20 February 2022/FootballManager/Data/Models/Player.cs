namespace FootballManager.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using static DataConstants;

    public class Player
    {
        [Key]
        public int Id { get; init; } 

        [Required]
        [MaxLength(FullNameMaxLength)]
        public string FullName { get; set; }
        [Required]
        public string ImageUrl { get; set; }
        [Required]
        [MaxLength(DefaultMaxLength)]
        public string Position { get; set; }
        [Range(0, MaxSpeed)]
        public byte Speed { get; set; }
        [Range(0, MaxEndurance)]
        public byte Endurance { get; set; }
        [Required]
        [MaxLength(DescriptionMaxLength)]
        public string Description { get; set; }
        public IEnumerable<UserPlayer> UserPlayers { get; init; } = new List<UserPlayer>();
    }
}
