using E_CommerceITI.repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_CommerceITI.Models;

namespace E_CommerceITI.Repository
{
    public interface IBillRepository : IGenericRepository<Bill>
    {
        //Bill GetLastBill();
        int GetnumberOfItems(int Id);
    }
}
