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
        [HttpGet]
        // GET: api/Products
        public IHttpActionResult GetAuthorizedProducts()
        {
            var AllProduct = from p in db.Products
                             where p.Authorized == true &&p.deleted==false
                             orderby p.items.Count(i=>i.prodId==p.ProductId) descending
                      select new
                      {
                        title = p.Title ,
                        price = p.Price,
                        TotalAmountInStock= p.ProductAmount.Select(o=>o.Amount).Sum()- p.items.Count(i => i.prodId == p.ProductId),
                        //p.items.Count(p.items.SingleOrDefault(i=>i.prodId==p.ProductId).prodId),
                        image = p.ProductImage.Select(o => o.imgSrc),
                        count = p.items.Count(i => i.prodId == p.ProductId)
                      };
            if (AllProduct == null) { return NotFound(); }
            else { return Ok(AllProduct.ToList()); }   
        }

        [Route("api/UnAuthorizedProducts")]
        public IHttpActionResult GetUnAuthorizedProducts()
        {
            var AllProduct = from p in db.Products
                             where p.Authorized == false && p.deleted==false
                             //orderby p.items.Count(i => i.prodId == p.ProductId) descending
                             select new
                             {
                                 title = p.Title,
                                 price = p.Price,
                                 TotalAmountInStock= p.ProductAmount.Select(o=>o.Amount).Sum()- p.items.Count(i => i.prodId == p.ProductId),
                                 image = p.ProductImage.Select(o => o.imgSrc),
                                 Discount = p.Discount.Where(i=>i.EndDate>=DateTime.Now)
                             };
            if (AllProduct == null) { return NotFound(); }
            else { return Ok(AllProduct.ToList()); }
        }


        [HttpPut]
        [Route("api/TOGetUnAuthorizedProductsAndAuthorizeIt/{PId}")]
        public IHttpActionResult ToGetUnAuthorizedProductsAndAuthorizeIt([FromUri]int PId , [FromBody]string AId )
        {
            var AllProduct = db.Products.SingleOrDefault(i => i.ProductId == PId && i.Authorized == false && i.deleted==false);
            if (AllProduct == null) { return NotFound(); }
            else
            {
                AllProduct.Authorized = true;
                AllProduct.AdminAuthId =  AId;
                db.SaveChanges();
                return Ok(AllProduct);
            }
        }

        // GET: api/Products/5
        [HttpGet]
        [ResponseType(typeof(Product))]
        public IHttpActionResult GetProductById(int id)
        {

            var PrductObject = from product in db.Products
                               where product.ProductId==id &&product.Authorized==true &&product.deleted==false
                      select new
                      {
                          title = product.Title,
                          price = product.Price,
                          TotalAmountInStock = product.ProductAmount.Select(o => o.Amount).Sum() - product.items.Count(i => i.prodId == product.ProductId),
                          Image = product.ProductImage.Select(im=>im.imgSrc)
                      };

            if (PrductObject == null)
            {
                return NotFound();
            }
            return Ok(PrductObject.ToList());
        }

        [HttpGet]
        [Route("api/Products/{ProductName:alpha}")]
        //[ResponseType(typeof(Product))]
        public IHttpActionResult GetProductByName(string ProductName)
        {
            var productByName = from product in db.Products
                                where product.Title == ProductName && product.Authorized == true && product.deleted == false
                      select new
                      {
                          title = product.Title,
                          price = product.Price,
                          TotalAmountInStock = product.ProductAmount.Select(o => o.Amount).Sum() - product.items.Count(i => i.prodId == product.ProductId),
                          image = product.ProductImage.Select(o => o.imgSrc)
                      };
            if (productByName == null) { return NotFound(); }
            else { return Ok(productByName.ToList()); }
        }
        // PUT: api/Products/5
        //  [ResponseType(typeof(void))]
        public IHttpActionResult PutProduct(int id, ProductModels product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
          
            else
            {
                Product P = db.Products.FirstOrDefault(i => i.ProductId == id);
               // P.categoryId = 1;
               // P.Authorized = true;
               // P.AdminAuthId = null;
               // P.SellerId = "1";
                P.Description = product.Description;
               // P.Date = DateTime.Now;
                P.Title = product.Title;
                P.Price = product.Price;

                ProductAmount proAmout = db.ProductAmounts.FirstOrDefault(i => i.ProducId == id);
                proAmout.ProducId = P.ProductId;
                proAmout.SellerId = P.SellerId;
                proAmout.Amount = product.Amount;
                proAmout.Date = DateTime.Now;
                proAmout.Color = product.Color;
                
                db.ProductAmounts.Add(proAmout);

                ProductImage proImage = db.ProductImages.FirstOrDefault(i => i.productId == id);
                proImage.imgSrc = null;//img;
                
                //proImage.productId = P.ProductId;
                // db.ProductImages.Add(proImage);
                db.SaveChanges();
                return Ok(product);
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
            if (product == null)
            {
                return BadRequest();
            }
             // byte[] img = new byte[product.imgSrc.ContentLength];
             // product.imgSrc.InputStream.Read(img, 0, product.imgSrc.ContentLength);
            else
            {
                Product NewProduct = new Product();
                NewProduct.categoryId = 1;
                NewProduct.Authorized = false;
                NewProduct.deleted = false;
                NewProduct.AdminAuthId = null;
                NewProduct.SellerId = "1";
                NewProduct.Description = product.Description;
                NewProduct.Date = DateTime.Now;
                NewProduct.Title = product.Title;
                NewProduct.Price = product.Price;

                ProductAmount NewProductAmount = new ProductAmount();
                NewProductAmount.ProducId = NewProduct.ProductId;
                NewProductAmount.SellerId = NewProduct.SellerId;
                NewProductAmount.Amount = product.Amount;
                NewProductAmount.Date = DateTime.Now;
                NewProductAmount.Color = product.Color;

                ProductImage NewProductImage = new ProductImage();
                NewProductImage.imgSrc = null;
                NewProductImage.productId = NewProduct.ProductId;

                db.Products.Add(NewProduct);
                db.ProductAmounts.Add(NewProductAmount);
                db.ProductImages.Add(NewProductImage);
                db.SaveChanges();

                return CreatedAtRoute("DefaultApi", new { id = NewProduct.ProductId }, product);
            }
        }
       
        // DELETE: api/Products/5
        // [ResponseType(typeof(Product))]
        public IHttpActionResult DeleteProduct(int id)
        {
            Product productToDelete = db.Products.Find(id);
            productToDelete.deleted = true;
            db.SaveChanges();
            return Ok(productToDelete);
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