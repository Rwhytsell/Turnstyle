using System;
using System.Collections.Generic;

namespace Turnstyle.Models
{
    public class Image
    {
        public Image()
        {
            Comment = new HashSet<Comment>();
            Thread = new HashSet<Thread>();
        }

        public int Id { get; set; }
        public string Source { get; set; }
        public int Owner { get; set; }

        public virtual Account OwnerNavigation { get; set; }
        public virtual ICollection<Comment> Comment { get; set; }
        public virtual ICollection<Thread> Thread { get; set; }
    }
}
