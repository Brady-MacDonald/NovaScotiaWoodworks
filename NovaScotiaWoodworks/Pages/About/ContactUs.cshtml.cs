using AspNetCoreHero.ToastNotification.Abstractions;
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
        private readonly INotyfService _notyf;
        [BindProperty]
        public ContactModel ContactRequest { get; set; }
        public ContactUsModel(INotyfService notyf)
        {
            _notyf = notyf;
            ContactRequest = new ContactModel();
        }

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                _notyf.Error("Invalid");
                return Page();
            }
            MailMessage mailMessage = new MailMessage();

            //Configure the mail message object
            mailMessage.From = new MailAddress("novascotiawoodworks@gmail.com");
            mailMessage.To.Add("novascotiawoodworks@gmail.com");
            mailMessage.Subject = ContactRequest.Subject;
            mailMessage.Body = 
                "<b>Sender Name: </b>"  + ContactRequest.Name   +"<br/>" +
                "<b>Sender Email: </b>" + ContactRequest.Email  + "<br/>" + 
                "<b>Comments: </b>"     + ContactRequest.Comment + "<br/>";
            mailMessage.IsBodyHtml = true;

            //Configure the smtp client to work with gmail
            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);
            smtpClient.EnableSsl = true;
            smtpClient.Credentials = 
                new System.Net.NetworkCredential("novascotiawoodworks@gmail.com", "NSadmin!");
            smtpClient.Send(mailMessage);

            _notyf.Success("Message Sent");
            return Page();
        }
    }
}
