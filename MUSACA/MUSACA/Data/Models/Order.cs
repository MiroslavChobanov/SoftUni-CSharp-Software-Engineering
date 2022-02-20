namespace MUSACA.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using static DataConstants;
    public class Order
    {
        [Key]
        [Required]
        [MaxLength(IdMaxLength)]
        public string Id { get; init; } = Guid.NewGuid().ToString();
        [Required]
        public string Status { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
        public User Cashier { get; set; }

    }
}
