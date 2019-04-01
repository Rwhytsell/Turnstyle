using System;
using System.Collections.Generic;

namespace Turnstyle.Models
{
    public class Thread
    {
        public Thread()
        {
            Comment = new HashSet<Comment>();
            Threadtags = new HashSet<Threadtags>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime Dateposted { get; set; }
        public int Author { get; set; }
        public int Headerimage { get; set; }

        public virtual Account AuthorNavigation { get; set; }
        public virtual Image HeaderimageNavigation { get; set; }
        public virtual ICollection<Comment> Comment { get; set; }
        public virtual ICollection<Threadtags> Threadtags { get; set; }
    }
}
