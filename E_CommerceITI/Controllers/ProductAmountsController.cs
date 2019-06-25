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
    public class ProductAmountsController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/ProductAmounts
        public IQueryable<ProductAmount> GetProductAmounts()
        {
            return db.ProductAmounts;
        }

        // GET: api/ProductAmounts/5
        [ResponseType(typeof(ProductAmount))]
        public IHttpActionResult GetProductAmount(int id)
        {
            ProductAmount productAmount = db.ProductAmounts.Find(id);
            if (productAmount == null)
            {
                return NotFound();
            }

            return Ok(productAmount);
        }

        // PUT: api/ProductAmounts/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutProductAmount(int id, ProductAmount productAmount)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != productAmount.Id)
            {
                return BadRequest();
            }

            db.Entry(productAmount).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductAmountExists(id))
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

        // POST: api/ProductAmounts
        [ResponseType(typeof(ProductAmount))]
        public IHttpActionResult PostProductAmount(ProductAmount productAmount)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ProductAmounts.Add(productAmount);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = productAmount.Id }, productAmount);
        }

        // DELETE: api/ProductAmounts/5
        [ResponseType(typeof(ProductAmount))]
        public IHttpActionResult DeleteProductAmount(int id)
        {
            ProductAmount productAmount = db.ProductAmounts.Find(id);
            if (productAmount == null)
            {
                return NotFound();
            }

            db.ProductAmounts.Remove(productAmount);
            db.SaveChanges();

            return Ok(productAmount);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ProductAmountExists(int id)
        {
            return db.ProductAmounts.Count(e => e.Id == id) > 0;
        }
    }
}