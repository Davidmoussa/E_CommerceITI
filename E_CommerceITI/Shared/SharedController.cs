using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Text;
using System.Web.Http;

namespace E_CommerceITI
{
    public class SharedController : ApiController
    {
        public void PostProblem(string title, string body,string SenderMail) {
            string fullData = title + "-----" + body;
            
            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);  //(host , port)
            smtpClient.EnableSsl = true;
            smtpClient.Credentials = new NetworkCredential("ecommerce.problems2020@gmail.com", "ECommerceProblems@2020");
            smtpClient.Send("ECommerceProblems@2020", "ECommerceProblems@2020",SenderMail, fullData);

        }

    }
}
