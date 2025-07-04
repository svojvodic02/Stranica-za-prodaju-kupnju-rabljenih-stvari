using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vjezba.Model
{
    public class Attachment
    {
        public int Id { get; set; }
    
        public string? AttachmentPath { get; set; }

        [ForeignKey(nameof(Listing))]

        public int ListingID { get; set; }

        public Listing? Listing { get; set; }
    }
}
