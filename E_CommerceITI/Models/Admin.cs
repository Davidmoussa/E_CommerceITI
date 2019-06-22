using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace E_CommerceITI.Models
{
    public class Admin 
    {
        [Key]
        [ForeignKey("User")]
        public string AdminId { get; set; }
        public virtual ICollection<Product> Products { get; set; }
        public ApplicationUser User { get; set; }
    }
}