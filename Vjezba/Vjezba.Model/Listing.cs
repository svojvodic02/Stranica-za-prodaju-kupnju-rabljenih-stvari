using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vjezba.Model;

namespace Vjezba.Model
{
    public class Listing
    {
        [Key]
        public int ID { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        [ForeignKey(nameof(City))]
        public int? CityID { get; set; }

        [ForeignKey(nameof(User))]
        public int UserID { get; set; }

        [ForeignKey(nameof(ListingType))]
        public int ListingTypeID { get; set; }

        public City? City { get; set; }
        public User? User { get; set; }
        public ListingType? ListingType { get; set; }

        public virtual ICollection<Attachment>? Attachments { get; set; }
    }
}
