namespace MUSACA.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using static DataConstants;
    public class Product
    {
        [Key]
        [Required]
        [MaxLength(IdMaxLength)]
        public string Id { get; init; } = Guid.NewGuid().ToString();
        [Required]
        public string Name { get; set; }
        public decimal Price { get; set; }

        public long Barcode { get; set; }
        [Required]
        public string Picture { get; set; }

    }
}
