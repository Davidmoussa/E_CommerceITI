using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace E_CommerceITI.Models
{
    public class Rate
    {
      
       
        [Key]
        [Column(Order = 0)]
        [ForeignKey("Product")]
        public int ProductId { get; set; }
       
        [Key]
        [Column(Order = 1)]
        [ForeignKey("Customer")]
        public string CustomerId { get; set; }
        public Product Product { get; set; }
        public Customer Customer { get; set; }
        [Range(maximum:5 , minimum:0)]
        public int Count { get; set; }
    }
}