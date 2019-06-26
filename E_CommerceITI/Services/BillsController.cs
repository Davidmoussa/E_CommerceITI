using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using E_CommerceITI.Models;

namespace E_CommerceITI.Services
{
    public class BillsController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

       // [Route("AllBillsDetails")]
        // GET: api/Bills
        public List<Bill> GetBills()
        {
            return db.Bills.ToList();
        }

        public Bill GetBill(int id)
        {
            return db.Bills.Find(id);
        }

        public List<Bill> GetBillByuserName(string name)
        {
            return db.Bills.Where(b => b.Users.User.UserName == name).ToList();
        }

        public void PostBill(Bill bill)
        {
             db.Bills.Add(bill);
            db.SaveChanges();
        }

        public void PutBill(int id, Bill bill)
        {
            db.Entry(bill).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void DeleteBill(int id)
        {
            db.Bills.Remove(db.Bills.Find(id));
            db.SaveChanges();
        }


    }
}