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
    public class BillItemsController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/BillItems
        public List<BillItem> GetBillItems()
        {
            return db.BillItems.ToList();
        }

        // GET: api/BillItems/5
        [ResponseType(typeof(BillItem))]
        public IHttpActionResult GetBillItem(int id)
        {
            BillItem billItem = db.BillItems.Find(id);
            if (billItem == null)
            {
                return NotFound();
            }

            return Ok(billItem);
        }

        // PUT: api/BillItems/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutBillItem(int id, BillItem billItem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != billItem.id)
            {
                return BadRequest();
            }

            db.Entry(billItem).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BillItemExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/BillItems
        [ResponseType(typeof(BillItem))]
        public IHttpActionResult PostBillItem(BillItem billItem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            db.ProductAmounts.Find(db.Products.Find(billItem.prodId)).Amount--;
            db.BillItems.Add(billItem);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = billItem.id }, billItem);
        }

        // DELETE: api/BillItems/5
        [ResponseType(typeof(BillItem))]
        public IHttpActionResult DeleteBillItem(int id)
        {
            BillItem billItem = db.BillItems.Find(id);
            if (billItem == null)
            {
                return NotFound();
            }
            db.ProductAmounts.Find(db.Products.Find(billItem.prodId)).Amount++;
            db.BillItems.Remove(billItem);
            db.SaveChanges();

            return Ok(billItem);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool BillItemExists(int id)
        {
            return db.BillItems.Count(e => e.id == id) > 0;
        }
    }
}