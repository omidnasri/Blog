using System;
using System.Collections.Generic;

namespace Blog.Models
{
    public class BlogEntry
    {
        public int Id { get; set; }
        public string ShortTitle { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Summary { get; set; }
        public DateTime CreateDate { get; set; }
        public string Language { get; set; }
        public IList<CommentEntry> Comments { get; set; }
        public IList<Tag> Tags { get; set; }
    }
}