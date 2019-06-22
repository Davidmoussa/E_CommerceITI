using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace E_CommerceITI.Models
{
   public  class Address
    {
        public string City { get; set; }
        public string Country { get; set; }
        public string  Street  { get; set; }
        public string blockNum { get; set; }
        public string  apartmentNum { get; set; }
        public string  others { get; set; }
    }
}