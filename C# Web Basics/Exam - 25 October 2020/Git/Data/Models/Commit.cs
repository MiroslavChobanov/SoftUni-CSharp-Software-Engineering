namespace Git.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using static DataConstants;
    public class Commit
    {
        [Key]
        [Required]
        [MaxLength(IdMaxLength)]
        public string Id { get; init; } = Guid.NewGuid().ToString();
        [Required]
        public string Description { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatorId { get; set; }
        public User Creator { get; set; }
        public string RepositoryId { get; set; }
        public Repository Repository { get; set; }
    }
}
