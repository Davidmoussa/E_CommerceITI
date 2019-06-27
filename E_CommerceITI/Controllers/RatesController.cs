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
            List<Rate> rateList = db.Rates.Include(i=>i.Customer).Include(i => i.Product).ToList();
            var response = new { message = "Success", data = rateList };
            return Ok(response);
        }

        // GET: api/Rate
        //[ResponseType(typeof(Rate))]
        [Route("api/rate")]
        public IHttpActionResult PostRate(RateModel rateModel)
        {
            Rate rate = db.Rates.Where(i=>i.CustomerId == rateModel.CustomerId && i.ProductId == rateModel.ProductId).Include(i=>i.Customer).Include(i => i.Customer).FirstOrDefault();
            if (rate == null)
            {
                return NotFound();
            }

            return Ok(rate);
        }

        // PUT: api/Rates/5
        //[ResponseType(typeof(void))]
        [Route("api/rate/edit")]
        public IHttpActionResult PutRate(Rate rate)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Rate ra = db.Rates.Where(i => i.CustomerId == rate.CustomerId && i.ProductId == rate.ProductId).FirstOrDefault();

            if (ra == null)
            {
                return BadRequest("product not Exist");
            }

            //Customer customer = db.Customer.Find(rate.CustomerId);
            //if (customer == null)
            //{
            //    var notFoundMess = new { message = "Customer ID not Exist" };
            //    return Content(HttpStatusCode.BadRequest, notFoundMess);
            //}

            ra.Count = rate.Count;
            //db.Entry(rate).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                //if (!RateExists(id))
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

        //// POST: api/Rates
        [ResponseType(typeof(Rate))]
        [Route("api/add/rate")]
        public IHttpActionResult PostRate(Rate rate)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Customer customer = db.Customer.Find(rate.CustomerId);
            Product product = db.Products.Find(rate.ProductId);

            if (customer == null && product == null)
            {
                return BadRequest("product or customer deos not exist");
            }

            db.Rates.Add(rate);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (db.Rates.Where(i=>i.CustomerId == rate.CustomerId && i.ProductId == rate.ProductId).FirstOrDefault() != null)
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
        //[ResponseType(typeof(Rate))]
        [Route("api/rate/delete")]
        public IHttpActionResult PostDeleteRate(RateModel rateModel)
        {
            Rate rate = db.Rates.Where(i => i.CustomerId == rateModel.CustomerId && i.ProductId == rateModel.ProductId).FirstOrDefault();

            if (rate == null)
            {
                return NotFound();
            }

            db.Rates.Remove(rate);
            db.SaveChanges();

            return Ok("Deleted Successfully");
        }
        [Route("api/product/rate/{prdId}")]
        // get avrage Rate of the product
        public IHttpActionResult GetProductRate(int prdId)
        {
            List<Rate> Rates = db.Rates.Where(r => r.ProductId == prdId).Include(i=>i.Customer).Include(i => i.Product).ToList();
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
            List<Rate> Rates = db.Rates.Where(r => r.CustomerId == CustId).Include(i => i.Product).Include(i => i.Customer).ToList();
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