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
    public class AddToWishListsController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/AddToWishLists
        [Route("api/getWishLists")]
        public IHttpActionResult GetAddToWishLists()
        {
            List<AddToWishList> wishlist = db.AddToWishLists.Where(i => i.Block == false).Include(i => i.Customer).Include(i => i.Product).OrderByDescending(i => i.Date).ToList();
            var mess = new { message = "Success", data = wishlist };
            return Ok(mess);
        }

        // GET: api/AddToWishLists/5
        //[ResponseType(typeof(AddToWishList))]
        [Route("api/getWishList")]
        public IHttpActionResult PostAddToWishList(RateModel wishlist)
        {
            AddToWishList addToWishList = db.AddToWishLists.Where(i => i.ProducId == wishlist.ProductId && i.CustomerId == wishlist.CustomerId && i.Block == false).Include(i => i.Customer).Include(i => i.Product).FirstOrDefault();
            if (addToWishList == null)
            {
                return NotFound();
            }

            return Ok(addToWishList);
        }

        // PUT: api/AddToWishLists/5
        [ResponseType(typeof(void))]
        [Route("api/addToWishList/edit")]
        public IHttpActionResult PutAddToWishList(AddToWishList addToWishList)
        {
            AddToWishList wishList = db.AddToWishLists.Where(i => i.ProducId == addToWishList.ProducId && i.CustomerId == addToWishList.CustomerId && i.Block == false).Include(i => i.Customer).Include(i => i.Product).FirstOrDefault();
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (wishList == null)
            {
                return BadRequest("not found");
            }

            wishList.Block = addToWishList.Block;
            wishList.Date = DateTime.Now;

            //db.Entry(addToWishList).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                //if (!AddToWishListExists(id))
                //{
                //    return NotFound();
                //}
                //else
                //{
                throw;
                //}
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/AddToWishLists
        [ResponseType(typeof(AddToWishList))]
        [Route("api/addToWishList/new")]
        public IHttpActionResult PostAddToWishList(AddToWishList addToWishList)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Product prd = db.Products.Find(addToWishList.ProducId);
            Customer customer = db.Customer.Find(addToWishList.CustomerId);

            if (prd == null)
            {
                return BadRequest("product not found");
            }

            if (customer == null)
            {
                return BadRequest("customer not found");
            }

            db.AddToWishLists.Add(addToWishList);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (db.AddToWishLists.Where(i => i.ProducId == addToWishList.ProducId && i.CustomerId == addToWishList.CustomerId).FirstOrDefault() != null)
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }
            var mess = new { message = "success", data = db.AddToWishLists.Where(i => i.CustomerId == addToWishList.CustomerId).Include(i => i.Product).Include(i => i.Customer).ToList() };
            return Ok(mess);
        }

        // DELETE: api/AddToWishLists/5
        //[ResponseType(typeof(AddToWishList))]
        [Route("api/addToWishlist/delete")]
        public IHttpActionResult PostDeleteWishItem(AddToWishList wishList)
        {
            if (db.Customer.Find(wishList.CustomerId) == null)
            {
                return BadRequest("customer not exist");
            }

            if (db.Products.Find(wishList.ProducId) == null)
            {
                return BadRequest("product not exist");
            }
            AddToWishList addToWishList = db.AddToWishLists.Where(i => i.CustomerId == wishList.CustomerId && i.ProducId == wishList.ProducId).FirstOrDefault();
            if (addToWishList == null)
            {
                return NotFound();
            }


            db.AddToWishLists.Remove(addToWishList);
            db.SaveChanges();

            return Ok(new { message = "deleted successfully" });
        }

        //get customer wish list
        [Route("api/customer/wishList/{CustId}")]
            public IHttpActionResult GetCustomerWishList(String CustId)
        {
            if (db.Customer.Find(CustId) == null)
            {
                return BadRequest("Customer not exist");
            }

            List<AddToWishList> wishList = db.AddToWishLists.Where(i => i.CustomerId == CustId).Include(i => i.Customer).Include(i => i.Product).ToList();
            return Ok(new { message = "success", data = wishList });
        }

        //get product wish list
        [Route("api/product/wishList/{prdId}")]
        public IHttpActionResult GetProductWishList(int prdId)
        {
            if (db.Products.Find(prdId) == null)
            {
                return BadRequest("product not exist");
            }

            List<AddToWishList> wishList = db.AddToWishLists.Where(i => i.ProducId == prdId).Include(i => i.Customer).Include(i => i.Product).ToList();
            return Ok(new { message = "success", data = wishList });
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