using E_CommerceITI.Models;
using E_CommerceITI.repository;
using E_CommerceITI.Repository;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;

namespace E_CommerceITI.Services
{
    [EnableCors(origins: "http://localhost:54834", headers: "*", methods: "*")]
    public class BillsController : ApiController
    {
        private IBillRepository Repository;
        private ApplicationDbContext db;
        public BillsController()
        {
            Repository = new BillRepository();
            db = new ApplicationDbContext();
        }

        // [Route("AllBillsDetails")]
        // GET: api/Bills
        public List<Bill> GetBills()
        {
            return Repository.GetAll().ToList();
        }

        public Bill GetBill(int id)
        {
            return Repository.Get(id);
        }
        [Route("api/bills/{userid}/GetBillByuserId")]
        public List<Bill> GetBillByuserId(string userid)
        {
            return db.Bills.Where(b => b.Users.User.Id == userid).ToList();
        }
        [Route("api/bills/{id}/GetCountofItems")]
        public int GetNumberOfItems(int id)
        {
            return Repository.GetnumberOfItems(id);
        }
        public void PostBill(Bill bill)
        {
            Repository.Add(bill);
            Repository.Save();
        }

        public void PutBill(Bill bill)
        {
            //var existed = Repository.Get(id);   
            Repository.Update(bill);
            Repository.Save();

        }

        public void DeleteBill(int id)
        {
            Repository.Delete(id);
            Repository.Save();
        }


    }
}