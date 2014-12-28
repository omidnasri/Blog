using System;

namespace Blog.Models
{
    public class CommentEntry
    {
        public int Id { get; set; }
        public string CommenterName { get; set; }
        public string Comment { get; set; }
        public string Title { get; set; }
        public DateTime CommentDate { get; set; }
        public bool IsApproved { get; set; }
        public string EmailAddress { get; set; }
    }
}
