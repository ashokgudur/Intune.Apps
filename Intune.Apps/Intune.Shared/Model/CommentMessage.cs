using System;

namespace Intune.Shared.Model
{
    public class CommentMessage
    {
        public int Id { get; set; }
        public CommentMessageDirection Direction { get; set; }
        public string Username { get; set; }
        public string Message { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
