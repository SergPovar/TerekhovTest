using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TerekhovTest.Data;
using TerekhovTest.Models;
using TestTerekhov.Models;
using System.Text.Json;
using Microsoft.EntityFrameworkCore.Migrations;
namespace TerekhovTest.Controllers
{
    [Route("api/mails")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private readonly IEmailSender _emailSender;
        private EmailDbContext _db;
        public EmailController(IEmailSender emailSender, EmailDbContext db) { 
            _emailSender = emailSender;
            _db= db;
         }

        /// <summary>
        ///  in this method we call the method for sending messages and the method for adding a message to the database
        /// </summary>
        /// <param name="emailDTO">
        /// The method accepts email addresses for sending letters, the letter itself and the subject for the letter
        /// </param>
        /// <returns>
        /// Returns "Ok" if the message was sent successfully and "Failde" with a description of the error if sending failed
        /// </returns>
        [HttpPost]
        public async Task<IActionResult> SendMessage(Email emailDTO)
        {
           var result =  await _emailSender.SendEmailAsync(emailDTO.Subject, emailDTO.Body, emailDTO.Recipients);

            await AddMessageToDB(result, emailDTO);

            if(result == "Ok")
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        /// <summary>
        ///  Method for adding emails to database
        /// </summary>
        /// <param name="emailDTO">
        /// The method accepts email addresses for sending letters, the letter itself, the subject for the letter
        /// </param>
        /// /// <param name="result">
        /// The method receives the result of sending messages
        /// </param>
        private async Task AddMessageToDB(string? result, Email? emailDTO)
        {          
            var newEmail = new EmailDB();

            newEmail.Subject = emailDTO.Subject;
            newEmail.Body = emailDTO.Body;
            newEmail.Recipients = emailDTO.Recipients;
            newEmail.Result = result;
          
            newEmail.DateTime = DateTime.Now;
          
            if (result == "Ok")
            {
                newEmail.Result = result;
                newEmail.FailedMessage = "";
            }
            else
            {
                newEmail.Result = "Failed";
                newEmail.FailedMessage = result;
            }
            _db.EmailDB.Add(newEmail);
            _db.SaveChanges();
        }

        /// <summary>
        /// The method retrieves all sent messages from the database with "result" == "Ok"
        /// </summary>
        /// <returns>
        /// returns data in Json format
        /// </returns>
        [HttpGet]
        public string GetAllEmail()
        {
            var emails = new List<EmailDB>();
            
            emails = _db.EmailDB.Where(i => i.Result == "Ok").ToList();
            if (emails.Count == 0)
            {
                return "Database is empty";
            }
            var jsonEmails = System.Text.Json.JsonSerializer.Serialize(emails);
            
            return jsonEmails;
        }
    }
}
