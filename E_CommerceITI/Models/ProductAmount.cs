using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace E_CommerceITI.Models
{
    public class ProductAmount
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("Product")]
        public int ProducId { get; set; }
        [ForeignKey("Seller")]
        public string SellerId { get; set; }
        public int Amount { get; set; }
        public DateTime Date { get; set; }
        public string Color { get; set; }

        public Seller Seller { get; set; }
        public Product Product { get; set; }



    }
}