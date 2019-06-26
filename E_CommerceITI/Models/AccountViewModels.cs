using System;
using System.Collections.Generic;
using System.Web;

namespace E_CommerceITI.Models
{
    // Models returned by AccountController actions.

    public class ExternalLoginViewModel
    {
        public string Name { get; set; }

        public string Url { get; set; }

        public string State { get; set; }
    }

    public class ProductModels
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string SellerId { get; set; }
        public DateTime Date { get; set; }
        public bool Authorized { get; set; }
        public string AdminAuthId { get; set; }
        public int categoryId { get; set; }
        public HttpPostedFileBase imgSrc { get; set; }
        public int Amount { get; set; }
        public string Color { get; set; }
    }
    public class ManageInfoViewModel
    {
        public string LocalLoginProvider { get; set; }

        public string Email { get; set; }

        public IEnumerable<UserLoginInfoViewModel> Logins { get; set; }

        public IEnumerable<ExternalLoginViewModel> ExternalLoginProviders { get; set; }
    }

    public class UserInfoViewModel
    {
        public string Email { get; set; }

        public bool HasRegistered { get; set; }

        public string LoginProvider { get; set; }
    }

    public class UserLoginInfoViewModel
    {
        public string LoginProvider { get; set; }

        public string ProviderKey { get; set; }
    }
}
