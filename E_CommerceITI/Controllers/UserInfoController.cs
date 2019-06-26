using E_CommerceITI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace E_CommerceITI.Controllers
{
    public class UserInfoController : ApiController
    {
       public IHttpActionResult PutUser([FromUri] string Id ,[FromBody] UpDateUserModel Model)
        {
            using(ApplicationDbContext db =new ApplicationDbContext())
            {
                if (Id == null)
                {
                    return BadRequest("Id is Null ");
                }
                var Resalt = db.Users.SingleOrDefault(i => i.Id == Id);
                if (Resalt == null)
                {
                    return NotFound();
                }
                else
                {
                    Resalt.LastName = Model.LastName;
                    Resalt.FirstName = Model.FirstName;
                    Resalt.PhoneNumber = Model.phonNum;
                    Resalt.Address = Model.Address;
                    db.SaveChanges();
                    return Ok(Model);
                }
            } 
        }
        [Route("api/UserInfo/getuser")]
        public IHttpActionResult GetUser( string Id)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                if (Id == null)
                {
                    return BadRequest("Id is Null ");
                }
                var Resalt = db.Users.SingleOrDefault(i => i.Id == Id);
                if (Resalt == null)
                {
                    return NotFound();
                }
                else
                {
                   
                    return Ok(Resalt);
                }
            }
        }
        [Route("api/UserInfo/SellerList")]
        public IHttpActionResult getSeller()
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                var Result = db.Sellers.OrderBy(i => i.User.FirstName).Select(i => new {
                    i.SellerId,
                    i.User.FirstName,
                    i.User.LastName,
                    i.User.Email,
                    i.User.PhoneNumber,
                    i.User.Address
                }).ToList();
                return Ok(Result);
            }
        }
        [Route("api/UserInfo/CustomerList")]
        public IHttpActionResult GetCustomer()
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                var Result = db.Customer.OrderBy(i => i.User.FirstName).Select(i=>new {i.CustomerId,i.User.FirstName ,
                    i.User.LastName,i.User.Email,i.User.PhoneNumber,i.User.Address}).ToList();
                return Ok(Result.ToList());
            }
        }
        [Route("api/UserInfo/AdminsList")]
        public IHttpActionResult GetAdmins()
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                var Result = db.Admins.OrderBy(i => i.User.FirstName).Select(i => new {
                    i.AdminId,
                    i.User.FirstName,
                    i.User.LastName,
                    i.User.Email,
                    i.User.PhoneNumber,
                    i.User.Address
                }).ToList();
                return Ok(Result);
            }
        }
    }
}
