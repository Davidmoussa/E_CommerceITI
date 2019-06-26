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
    public class RatesController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/Rates
        public IHttpActionResult GetRates()
        {
            List<Rate> rateList = db.Rates.ToList();
            var response = new { message = "Success", data = rateList };
            return Ok(response);
        }

        // GET: api/Rates/5
        [ResponseType(typeof(Rate))]
        public IHttpActionResult GetRate(int id)
        {
            Rate rate = db.Rates.Find(id);
            if (rate == null)
            {
                return NotFound();
            }

            return Ok(rate);
        }

        // PUT: api/Rates/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutRate(int id, Rate rate)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != rate.ProductId)
            {
                return BadRequest("product ID not Exist");
            }

            Customer customer = db.Customer.Find(rate.CustomerId);
            if (customer == null)
            {
                var notFoundMess = new { message = "Customer ID not Exist" };
                return Content(HttpStatusCode.BadRequest, notFoundMess);
            }

            

            db.Entry(rate).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RateExists(id))
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

        // POST: api/Rates
        [ResponseType(typeof(Rate))]
        public IHttpActionResult PostRate(Rate rate)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Customer customer = db.Customer.Find(rate.CustomerId);
            if(customer == null)
            {
                return BadRequest("Customer ID not Found");
            }

            Product product = db.Products.Find(rate.ProductId);
            if(product == null)
            {
                return BadRequest("Product ID not Found");
            }

            db.Rates.Add(rate);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (RateExists(rate.ProductId))
                {
                    //return Conflict();
                    return  ResponseMessage(Request.CreateResponse(HttpStatusCode.Conflict, "rate already Exist"));
                }
                else
                {
                    throw;
                }
            }
            var addMess = new { message = "Success", data = db.Rates.Where(i => i.ProductId == rate.ProductId && i.CustomerId == rate.CustomerId) };
            return Ok(addMess);
            //return CreatedAtRoute("DefaultApi", new { id = rate.ProductId }, rate);
        }

        // DELETE: api/Rates/5
        [ResponseType(typeof(Rate))]
        public IHttpActionResult DeleteRate(int id)
        {
            Rate rate = db.Rates.Find(id);
            if (rate == null)
            {
                return NotFound();
            }

            db.Rates.Remove(rate);
            db.SaveChanges();

            return Ok(rate);
        }
        [Route("api/product/rate/{prdId}")]
        // get avrage Rate of the product
        public IHttpActionResult GetProductRate(int prdId)
        {
            List<Rate> Rates = db.Rates.Where(r => r.ProductId == prdId).ToList();
            var rateSum = 0;
            if (Rates.Count() != 0)
            {
                foreach (var Rate in Rates)
                {
                    rateSum += Rate.Count;
                }
                var AverageRate = rateSum / (db.Rates.Where(r => r.ProductId == prdId).ToList().Count());
                var RateRes =new { Message="Success",RateValue = AverageRate };
                return Ok(RateRes);

            }
            else
            {
                var notFoundMess = new {message= "product not exist"};
                return Content(HttpStatusCode.NotFound, notFoundMess);

            }

        }

        //get customer rate to all products
        [Route("api/customer/rates/{CustId}")]
        public IHttpActionResult GetCustomerRates(string CustId)
        {
            List<Rate> Rates = db.Rates.Where(r => r.CustomerId == CustId).ToList();
            if (Rates.Count() != 0)
            {
                var CustomerRates = new { message = "success", data = Rates };
                return Ok(CustomerRates);
            }
            else
            {
                var notFoundMess = new { message = "user do not have rates yet" };
                return Content(HttpStatusCode.NotFound, notFoundMess);
            }

        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool RateExists(int id)
        {
            return db.Rates.Count(e => e.ProductId == id) > 0;
        }
    }
}