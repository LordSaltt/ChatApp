using System;

namespace ChatApp.Dto
{
    public class MessageDto
    {
         public int Id { get; set; }
        public string Name { get; set; }
        public string Text { get; set; }
        public DateTime Timestamp { get; set; }
        public int ChatId { get; set; }
    }
}