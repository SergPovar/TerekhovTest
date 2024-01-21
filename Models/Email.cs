namespace TerekhovTest.Models
{
    /// <summary>
    ///  Model for transmitting data to send a letter
    /// </summary>
    public class Email
    {
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
        public string []? Recipients { get; set; }
        
    }
}
