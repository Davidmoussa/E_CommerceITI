using E_CommerceITI.Models;
using E_CommerceITI.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace E_CommerceITI.repository
{
    public class BillRepository : GenericRepository<Bill>, IBillRepository
    {
        ApplicationDbContext context = new ApplicationDbContext();
        //public Bill GetLastBill()
        //{
        //    return context.Bills.FirstOrDefault(b => b.Date == DateTime.Now);
        //}
        public int GetnumberOfItems(int Id)
        {
            return context.Bills.Where(b => b.Id == Id ).SelectMany(x=> x.items).Count();
        }
    }
}