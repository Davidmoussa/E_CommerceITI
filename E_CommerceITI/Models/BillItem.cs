using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace E_CommerceITI.Models
{
    public class BillItem

    {
        [Key]
        public  int id { get; set; }
        public decimal price { get; set; }
        public string color { get; set; }
        [ForeignKey("Product")]
        public int prodId { get; set; }
        [ForeignKey("bill")]
        public int BillId { get; set; }
        public Product Product { set; get; } 
        public Bill bill { set; get; }
    }
}