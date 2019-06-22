using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace E_CommerceITI.Models
{
    public class Seller 
    {
        [Key]
        [ForeignKey("User")]
        public string SellerId { get; set; }
        public string Description { get; set; }
        public virtual ICollection<Product> Products { get; set; }
        public virtual ICollection<Discount> discounts { get; set; }
        public ApplicationUser User { get; set; }
    }
}