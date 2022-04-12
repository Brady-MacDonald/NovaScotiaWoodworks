using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NovaScotiaWoodworks.Models;
using System.Net.Mail;

namespace NovaScotiaWoodworks.Pages.About
{
    [Authorize]
    public class ContactUsModel : PageModel
    {
        [BindProperty]
        public ContactModel ContactRequest { get; set; }
        public ContactUsModel()
        {
            ContactRequest = new ContactModel();
        }

        public void OnGet()
        {
        }

        public void OnPost()
        {
            MailMessage mailMessage = new MailMessage();
            //Compose mail message object
            mailMessage.From = new MailAddress("novascotiawoodworks@gmail.com");
            mailMessage.To.Add("novascotiawoodworks@gmail.com");
            mailMessage.Subject = ContactRequest.Subject;
            mailMessage.Body = "<b>Sender Name: </b>" + ContactRequest.Name +"<br/>" +
                "<b>Sender Email: </b>" + ContactRequest.Email + "<br/>" + 
                "<b>Comments: </b>" + ContactRequest.Comment + "<br/>";
            mailMessage.IsBodyHtml = true;

            //Configure the smtp client to work with gmail
            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);
            smtpClient.EnableSsl = true;
            smtpClient.Credentials = 
                new System.Net.NetworkCredential("novascotiawoodworks@gmail.com", "NSadmin!");
            smtpClient.Send(mailMessage);

            //Display message to user for success
        }
    }
}
