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
    public class ReviewsController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/Reviews
        public IHttpActionResult GetAllReviews()
        {
            List<Review> reviewList = db.Reviews.Include(i => i.Customer).Include(i => i.Product).ToList();
            var response = new { message = "Success", data = reviewList };
            return Ok(response);
            //return db.Reviews;
        }

        //get review by id
        [Route("api/review")]
        [ResponseType(typeof(Review))]

        public IHttpActionResult PostReviewbyId(ReviewModel reviewModel)
        {
            var review = db.Reviews.Include(i => i.Product).Include(i => i.Customer).SingleOrDefault(i => i.ProducId == reviewModel.ProductId && i.CustomerId == reviewModel.CustomerId);
            if (review == null)
            {
                return NotFound();
            }

            return Ok(review);
        }

        //edit review
        [Route("api/review/edit/{prdId}")]
        //[ResponseType(typeof(void))]
        public IHttpActionResult PutEditReview(int prdId,Review review)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Review re = db.Reviews.Where(i => i.CustomerId == review.CustomerId && i.ProducId == prdId).FirstOrDefault();
            if (re == null)
            {
                return NotFound();
            }
            re.ProducId = review.ProducId;
            re.CustomerId = review.CustomerId;
            re.Content = review.Content;
            //Customer customer = db.Customer.Find(review.CustomerId);
            //if(customer == null)
            //{
            //    return BadRequest("Customer ID does not exist");

            //}

            //db.Entry(review).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                //if ((db.Reviews.Where(i => i.CustomerId == reviewModel.CustomerId && i.ProducId == reviewModel.ProductId)) == null)
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

        // POST: api/Reviews
        [ResponseType(typeof(Review))]
        public IHttpActionResult PostReview(Review review)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Product product = db.Products.Find(review.ProducId);
            Customer customer = db.Customer.Find(review.CustomerId);

            if (product == null)
            {
                return BadRequest("Product ID doesnot exist");
            }

            if (customer == null)
            {
                return BadRequest("Customer ID doesnot exist");
            }

            db.Reviews.Add(review);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (ReviewExists(review.ProducId))
                {
                    //return Conflict();
                    return ResponseMessage(Request.CreateResponse(HttpStatusCode.Conflict, "review already Exist"));

                }
                else
                {
                    throw;
                }
            }

            //return CreatedAtRoute("DefaultApi", new { id = review.ProducId }, review);
            var addMess = new { message = "Success", data = review };
            return Ok(addMess);
        }

        // DELETE: api/Reviews/5
        [Route("api/review/delete")]
        //[ResponseType(typeof(Review))]
        public IHttpActionResult PostDeleteReview(ReviewModel reviewModel)
        {
            Review review = db.Reviews.Where(i=>i.CustomerId == reviewModel.CustomerId && i.ProducId == reviewModel.ProductId).FirstOrDefault();
            if (review == null)
            {
                return NotFound();
            }

            db.Reviews.Remove(review);
            db.SaveChanges();
            var mess = new { message = "deleted successfully" };
            return Ok(mess);
        }

        //get review per product
        [Route("api/product/reviews/{prdId}")]
        public IHttpActionResult GetProductReviews(int prdId)
        {
            List<Review> prdReviewList = db.Reviews.Where(i => i.ProducId == prdId).Include(i => i.Product).Include(i => i.Customer).ToList();
            if (prdReviewList.Count() != 0)
            {
                var mess = new { message = "Success", data = prdReviewList };
                return Ok(mess);
            }
            else
            {
                //var mess = new { message = HttpStatusCode.NotFound };
                //return Ok(mess);
                return NotFound();
            }
        }

        //get review per customer
        [Route("api/customer/reviews/{customerId}")]
        public IHttpActionResult GetCustomerReviews(string customerId)
        {
            List<Review> customerReviewList = db.Reviews.Where(i => i.CustomerId == customerId.ToString()).Include(i => i.Customer).Include(i => i.Product).ToList();
            if (customerReviewList.Count() != 0)
            {
                var mess = new { message = "Success", data = customerReviewList };
                return Ok(mess);
            }
            else
            {
                //var mess = new { message = HttpStatusCode.NotFound };
                //return Ok(mess);
                return NotFound();
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

        private bool ReviewExists(int id)
        {
            return db.Reviews.Count(e => e.ProducId == id) > 0;
        }
    }
}