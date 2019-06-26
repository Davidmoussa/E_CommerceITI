using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System;

namespace E_CommerceITI.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
        

        public bool Block { get; set; }
        //[RegularExpression(@"^[A-Z]+[a-zA-Z""'\s-]*$")]
        [StringLength(50, MinimumLength = 1)]
        public string FirstName { get; set; }
        //[RegularExpression(@"^[A-Z]+[a-zA-Z""'\s-]*$")]
        [StringLength(50, MinimumLength = 1)]
        public string LastName { get; set; }
        
        public Address Address { get; set; }
        public Admin Admin { get; set; }
        public Seller Seller { get; set; }
        public Customer Customer { get; set; }

        internal async Task<ClaimsIdentity> GenerateUserIdentityAsync(ApplicationUserManager userManager, string authenticationType)
        {
            var userIdentity =await userManager.CreateIdentityAsync(this,DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
            //throw new NotImplementedException();
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
        //Dbset 
        public virtual DbSet<Seller> Sellers { get; set; }
        public virtual DbSet<Admin> Admins { get; set; }
        public virtual DbSet<Customer> Customer { get; set; }

        public virtual DbSet<Category> Categorys { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Bill> Bills { get; set; }
        public virtual DbSet<BillItem> BillItems { get; set; }
        public virtual DbSet<Review> Reviews { get; set; }
        public virtual DbSet<Rate> Rates { get; set; }
        public virtual DbSet<ProductImage> ProductImages { get; set; }
        public virtual DbSet<ProductAmount> ProductAmounts { get; set; }
        public virtual DbSet<Discount> Discounts { get; set; }
        public virtual DbSet<AddToWishList> AddToWishLists { get; set; }
    }
}