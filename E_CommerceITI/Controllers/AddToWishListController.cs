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

namespace E_CommerceITI.Controllers
{
    public class AddToWishListController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/AddToWishList
        public IHttpActionResult GetAddToWishLists()
        {
            /* var add = from a in db.AddToWishLists
             select new
             {
                 CustomerId = a.CustomerId,
                 ProducId = a.ProducId,
                 Block = a.Block,
                 Date = a.Date,
             };
             return Ok(add.ToList());*/
            return Ok(db.AddToWishLists.Where(i => i.Block == false).OrderByDescending(i=>i.Date).ToList());
        }

        // GET: api/AddToWishList/5
        [ResponseType(typeof(AddToWishList))]
        public IHttpActionResult GetAddToWishList(string id)
        {
            ICollection<AddToWishList> addToWishList = db.AddToWishLists.Where(i => i.CustomerId == id &&
            i.Block == false).OrderByDescending(i => i.Date).ToList();
            if (addToWishList == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(addToWishList);
            }
        }
        // PUT: api/AddToWishList/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutAddToWishList(int id, AddToWishList addToWishList)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != addToWishList.ProducId)
            {
                return BadRequest();
            }
            else
            {
                AddToWishList add = db.AddToWishLists.FirstOrDefault(i => i.ProducId == id);
                add.CustomerId = addToWishList.CustomerId;
                add.ProducId = addToWishList.ProducId;
                add.Date = DateTime.Now;
                add.Block = false;
                db.SaveChanges();
                return StatusCode(HttpStatusCode.NoContent);
            }
            /* db.Entry(addToWishList).State = EntityState.Modified;

             try
             {
                 db.SaveChanges();
             }
             catch (DbUpdateConcurrencyException)
             {
                 if (!AddToWishListExists(id))
                 {
                     return NotFound();
                 }
                 else
                 {
                     throw;
                 }
             }

             return StatusCode(HttpStatusCode.NoContent);*/
        }

        // POST: api/AddToWishList
        [ResponseType(typeof(AddToWishList))]

        public IHttpActionResult PostAddToWishList(AddToWishList addToWishList)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Customer c = db.Customer.Find(addToWishList.CustomerId);
            if (c == null)
            {
                return BadRequest("customer not found");
            }

            Product p = db.Products.Find(addToWishList.ProducId);
            if (p == null)
            {
                return BadRequest("product not found");
            }

            AddToWishList add = new AddToWishList();
            add.ProducId = addToWishList.ProducId;
            add.CustomerId = addToWishList.CustomerId;
            add.Date = DateTime.Now;
            add.Customer = db.Customer.SingleOrDefault(i => i.SellerId == addToWishList.CustomerId);
            add.Block = false;
            db.AddToWishLists.Add(add);
            db.SaveChanges();
            return Ok();

        }

        // DELETE: api/AddToWishList/5
        [ResponseType(typeof(AddToWishList))]
        public IHttpActionResult DeleteAddToWishList(int id)
        {
            AddToWishList addToWishList = db.AddToWishLists.Find(id);
            if (addToWishList == null)
            {
                return NotFound();
            }

            db.AddToWishLists.Remove(addToWishList);
            db.SaveChanges();
            return Ok(addToWishList);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AddToWishListExists(int id)
        {
            return db.AddToWishLists.Count(e => e.ProducId == id) > 0;
        }
    }
}