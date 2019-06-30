using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace E_CommerceITI.Models
{
    public class ProductImage
    {
        [Key]
        public int Id { get; set; }
        public string imgSrc { get; set; }
        [ForeignKey("Product")]
        public int  productId { get; set; }
        public Product Product { get; set; }
    }
}