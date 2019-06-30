using E_CommerceITI.Models;
using E_CommerceITI.Repository;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;

namespace E_CommerceITI.Services
{

    [EnableCors(origins: "http://localhost:54834", headers: "*", methods: "*")]
    public class BillItemsController : ApiController
    {
        private ApplicationDbContext db;
        BillItemsRepository Repository;
        public BillItemsController()
        {
            Repository = new BillItemsRepository();
            db = new ApplicationDbContext();
        }
        // GET: api/BillItems
        public List<BillItem> GetBillItems()
        {
            return Repository.GetAll().ToList();
        }

        // GET: api/BillItems/5
        [ResponseType(typeof(BillItem))]
        public IHttpActionResult GetBillItem(int id)
        {
            BillItem billItem = Repository.Get(id);
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

            Repository.Update(billItem);
            try
            {
                Repository.Save();
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
            Repository.Add(billItem);
            Repository.Save();

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
            Repository.Delete(billItem);
            Repository.Save();
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