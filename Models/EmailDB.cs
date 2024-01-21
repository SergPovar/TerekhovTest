using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using TerekhovTest;

namespace TestTerekhov.Models
{
    /// <summary>
    ///  Message model for saving in the database
    /// </summary>
    [Serializable]
    public class EmailDB
    {
        /// <summary>
        ///  unique key used to search the database
        /// </summary>
        [Key]
        public int Id { get; set; }
        /// <summary>
        ///  Letter subject
        /// </summary>
        public string? Subject { get; set; }
        /// <summary>
        ///  Letter to be sent
        /// </summary>
        public string? Body { get; set; }
        /// <summary>
        ///  List of recipients
        /// </summary>
        public string[]? Recipients { get; set; }
        /// <summary>
        /// Date and time the letter was sent
        /// </summary>
 
        [JsonConverter(typeof(DateTimeJsonConverter))]
        public DateTime? DateTime { get; set; }
        /// <summary>
        ///  result of sending a letter
        /// </summary>
        public string? Result { get; set; }
        /// <summary>
        ///  Error message if the email was not sent
        /// </summary>
        public string? FailedMessage { get; set; }

    }
}
