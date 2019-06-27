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
        public IQueryable<AddToWishList> GetAddToWishLists()
        {
            return db.AddToWishLists;
        }

        // GET: api/AddToWishList/5
        [ResponseType(typeof(AddToWishList))]
        public IHttpActionResult GetAddToWishList(int id)
        {
            AddToWishList addToWishList = db.AddToWishLists.Find(id);
            if (addToWishList == null)
            {
                return NotFound();
            }

            return Ok(addToWishList);
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

            db.Entry(addToWishList).State = EntityState.Modified;

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

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/AddToWishList
        [ResponseType(typeof(AddToWishList))]
        public IHttpActionResult PostAddToWishList(AddToWishList addToWishList)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.AddToWishLists.Add(addToWishList);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (AddToWishListExists(addToWishList.ProducId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = addToWishList.ProducId }, addToWishList);
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