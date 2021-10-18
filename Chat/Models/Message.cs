using System;

namespace Chat.Models
{
    public class Message
    {
        public int Id { get; set; }
        public string GroupId { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public string Content { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
