using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace E_CommerceITI.Models
{
    public class Discount
    {
        [Key]
        [Column(Order = 0)]
        [ForeignKey("Product")]
        public int productId { get; set; }
        [ForeignKey("seller")]
        public string sellerId { get; set; }
        public Product Product { set; get; }
        public Seller seller { set; get; }
        [Key]
        [Column(Order = 1)]
        
        public DateTime StartDate { get; set; }
        [Key]
        [Column(Order = 2)]
        
        public DateTime EndDate { get; set; }
        public decimal percentage { get; set; }
        public ICollection<Discount> discounts { get; set; }




    }
}