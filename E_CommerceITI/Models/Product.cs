using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace E_CommerceITI.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        [ForeignKey("Seller")]
        public string SellerId { get; set; }
        public DateTime Date { get; set; }
        public bool Authorized { get; set; }
        [ForeignKey("AdminAuth")]
        public string AdminAuthId { get; set; }
        public Admin AdminAuth { get; set; }
        public Seller Seller { get; set; }
        [ForeignKey("Category")]
        public int categoryId { get; set; }
        public Category Category { get; set; }
        public virtual ICollection<ProductImage> ProductImage { get; set; }
        public virtual ICollection<ProductAmount> ProductAmount { get; set; }
        public virtual ICollection<BillItem> items { get; set; }
        public virtual ICollection<Review> productReview { get; set; }
        public virtual ICollection<Rate> rates { get; set; }
    }
}