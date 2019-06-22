using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace E_CommerceITI.Models
{
    public class Customer
    {
        [Key]
        [ForeignKey("User")]
        public string SellerId { get; set; }
        public ApplicationUser User { get; set; }
        public virtual ICollection<AddToWishList> productWishList { get; set; }
        public virtual ICollection<Review> productReview { get; set; }
        public virtual ICollection<Bill> bills { get; set; }
        public virtual ICollection<Rate> rates { get; set; }
    }
}