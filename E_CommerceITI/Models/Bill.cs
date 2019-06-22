using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace E_CommerceITI.Models
{
    public class Bill
    {
        [Key]
        public int Id { get; set; }
        public Address Addres{ set; get;  }
        [ForeignKey("Users")]
        public string UserId { get; set; }
        public DateTime Date  { get; set; }
        public Customer Users { get; set; }
        public virtual ICollection<BillItem> items { set; get; }


    }

}
