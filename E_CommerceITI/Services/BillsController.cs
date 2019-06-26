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
using E_CommerceITI.repository;
using E_CommerceITI.Repository;

namespace E_CommerceITI.Services
{
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

        public List<Bill> GetBillByuserName(string name)
        {
            return db.Bills.Where(b => b.Users.User.UserName == name).ToList();
        }

        public void PostBill(Bill bill)
        {
            Repository.Add(bill);
            Repository.Save();
        }

        public void PutBill( Bill bill)
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