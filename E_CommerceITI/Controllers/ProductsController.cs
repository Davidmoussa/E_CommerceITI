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
    public class ProductsController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/Products
        public IHttpActionResult GetProducts()
        {
            //   var a = new ProductAmount();

            var pro = from p in db.Products
                     // group p by p.Category into myp
                    //  where myp.All(a =>a.ProductAmount.Count > 0)
                      select new
                      {
                        title = p.Title ,
                        price =  p.Price,
                        Amount= p.ProductAmount.Select(o=>o.Amount),
                        image = p.ProductImage.Select(o => o.imgSrc),
                      };
            return Ok(pro.ToList());
        }

        // GET: api/Products/5
        [ResponseType(typeof(Product))]
        public IHttpActionResult GetProduct(int id)
        {
            
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        // PUT: api/Products/5
        //  [ResponseType(typeof(void))]
        public IHttpActionResult PutProduct(int id, ProductModels product)//, ProductAmount productAmount , ProductImage productImage)

        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //if (id != product.ProductId)
            //{
            //    return BadRequest();
            //}
            else
            {
                Product P = db.Products.FirstOrDefault(i => i.ProductId == id);
                P.categoryId = 1;//product.categoryId;
                P.Authorized = true;
                P.AdminAuthId = null;
                P.SellerId = "1"; //product.SellerId;
                P.Description = product.Description;
                P.Date = DateTime.Now;
                P.Title = product.Title;
                P.Price = product.Price;

                ProductAmount proAmout = db.ProductAmounts.FirstOrDefault(i => i.ProducId == id);
                proAmout.ProducId = P.ProductId;
                proAmout.SellerId = P.SellerId;
                proAmout.Amount = product.Amount;
                proAmout.Date = DateTime.Now;
                proAmout.Color = product.Color;
                // P.ProductAmount.Add(new ProductAmount() {Amount=product.Amount});

                ProductImage proImage = db.ProductImages.FirstOrDefault(i => i.productId == id);
                proImage.imgSrc = null;//img;
                proImage.productId = P.ProductId;
                // P.ProductAmount.Add(new ProductAmount() { Color = product.Color, Amount = product.Amount });
                //db.Entry(P).State = EntityState.Modified;
                //db.Entry(proAmout).State = EntityState.Modified;
                //db.Entry(proImage).State = EntityState.Modified;
                db.SaveChanges();
                return StatusCode(HttpStatusCode.NoContent);

                //}
                //try
                //{
                //    db.SaveChanges();
                //}
                //catch (DbUpdateConcurrencyException)
                //{
                //    if (!ProductExists(id))
                //    {
                //        return NotFound();
                //    }
                //    else
                //    {
                //        throw;
                //    }
                //}

                //return StatusCode(HttpStatusCode.NoContent);
            }
        }

        // POST: api/Products
        //[ResponseType(typeof(Product))]
        public IHttpActionResult PostProduct(ProductModels product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
          //  byte[] img = new byte[product.imgSrc.ContentLength];
           // product.imgSrc.InputStream.Read(img, 0, product.imgSrc.ContentLength);

            Product P = new Product();
            P.categoryId = 1;//product.categoryId;
            P.Authorized = true;
            P.AdminAuthId = null;
            P.SellerId ="1"; //product.SellerId;
            P.Description = product.Description;
            P.Date = DateTime.Now;
            P.Title = product.Title;
            P.Price = product.Price;
           
            ProductAmount proAmout = new ProductAmount();
            proAmout.ProducId = P.ProductId;
            proAmout.SellerId = P.SellerId;
            proAmout.Amount = product.Amount;
            proAmout.Date= DateTime.Now;
            proAmout.Color = product.Color;
            // P.ProductAmount.Add(new ProductAmount() {Amount=product.Amount});

            ProductImage proImage = new ProductImage();
            proImage.imgSrc = null;//img;
            proImage.productId = P.ProductId;
           
            // P.ProductAmount.Add(new ProductAmount() { Color = product.Color, Amount = product.Amount });

            db.Products.Add(P);
            db.ProductAmounts.Add(proAmout);
            db.ProductImages.Add(proImage);
           db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = P.ProductId }, product);
        }
        //public IHttpActionResult PostProduct(ProductModels product)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }
        //    byte[] img = new byte[product.imgSrc.ContentLength];
        //    product.imgSrc.InputStream.Read(img, 0, product.imgSrc.ContentLength);
        //    Product P = new Product();
        //    P.categoryId = product.categoryId;
        //    P.Authorized = product.Authorized;
        //    P.SellerId = product.SellerId;

        //    P.Description = product.Description;
        //    P.Date = DateTime.Now;
        //    P.Title = product.Title;
        //    P.Price = product.Price;
        //    P.ProductAmount.Add(new ProductAmount() { Amount = product.Amount });
        //    P.ProductImage.Add(new ProductImage() { imgSrc = img });
        //    P.ProductAmount.Add(new ProductAmount() { Color = product.Color, Amount = product.Amount });

        //    db.Products.Add(P);
        //    db.SaveChanges();

        //    return CreatedAtRoute("DefaultApi", new { id = P.ProductId }, product);
        //}


        // DELETE: api/Products/5
        [ResponseType(typeof(Product))]
        public IHttpActionResult DeleteProduct(int id)
        {
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return NotFound();
            }

            db.Products.Remove(product);
            db.SaveChanges();

            return Ok(product);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ProductExists(int id)
        {
            return db.Products.Count(e => e.ProductId == id) > 0;
        }
    }
}