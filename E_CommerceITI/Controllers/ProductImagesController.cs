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
    public class ProductImagesController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/ProductImages
        public IQueryable<ProductImage> GetProductImages()

        {
          //  new ProductAmountsController().GetProductAmounts();
            return db.ProductImages;
        }

        // GET: api/ProductImages/5
        [ResponseType(typeof(ProductImage))]
        public IHttpActionResult GetProductImage(int id)
        {
            ProductImage productImage = db.ProductImages.Find(id);
            if (productImage == null)
            {
                return NotFound();
            }

            return Ok(productImage);
        }

        // PUT: api/ProductImages/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutProductImage(int id, ProductImage productImage)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != productImage.Id)
            {
                return BadRequest();
            }

            db.Entry(productImage).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductImageExists(id))
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

        // POST: api/ProductImages
        [ResponseType(typeof(ProductImage))]
        public IHttpActionResult PostProductImage(ProductImage productImage)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ProductImages.Add(productImage);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = productImage.Id }, productImage);
        }

        // DELETE: api/ProductImages/5
        [ResponseType(typeof(ProductImage))]
        public IHttpActionResult DeleteProductImage(int id)
        {
            ProductImage productImage = db.ProductImages.Find(id);
            if (productImage == null)
            {
                return NotFound();
            }

            db.ProductImages.Remove(productImage);
            db.SaveChanges();

            return Ok(productImage);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ProductImageExists(int id)
        {
            return db.ProductImages.Count(e => e.Id == id) > 0;
        }
    }
}