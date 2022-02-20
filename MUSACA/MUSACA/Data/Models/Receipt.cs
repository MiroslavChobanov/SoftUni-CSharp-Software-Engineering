namespace MUSACA.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using static DataConstants;
    public class Receipt
    {
        [Key]
        [Required]
        [MaxLength(IdMaxLength)]
        public string Id { get; init; } = Guid.NewGuid().ToString();

        public DateTime IssuedOn { get; set; }
        public IEnumerable<Order> Orders { get; init; } = new List<Order>();
        public User Cashier { get; set; }
    }
}
