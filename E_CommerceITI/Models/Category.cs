using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace E_CommerceITI.Models
{
    public class Category
    {
        [Key]
        public int id { get; set; }
        public string title { get; set; }
        public string Description { get; set; }
        public byte[] image { get; set; }
        public virtual ICollection<Product> products { set; get; }

    }
}