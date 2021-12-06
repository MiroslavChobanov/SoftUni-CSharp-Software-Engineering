using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Theatre.Data.Models
{
    public class Theatre
    {
        public Theatre()
        {
            this.Tickets = new HashSet<Ticket>();
        }
        public int Id { get; set; }
        [Required]
        [MinLength(4)]
        [MaxLength(30)]
        public string Name { get; set; }
        [Range(1,10)]
        public sbyte NumberOfHalls { get; set; }
        [Required]
        [MinLength(4)]
        [MaxLength(30)]
        public string Director { get; set; }
        public ICollection<Ticket> Tickets { get; set; }
    }
}
