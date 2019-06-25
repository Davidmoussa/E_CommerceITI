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
        public IQueryable<Rate> GetRates()
        {
            return db.Rates;
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
                return BadRequest();
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

            db.Rates.Add(rate);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (RateExists(rate.ProductId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = rate.ProductId }, rate);
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