using System;
using System.Collections.Generic;

namespace Turnstyle.Models
{
    public class Comment
    {
        public Comment()
        {
            InverseParentcommentNavigation = new HashSet<Comment>();
        }

        public int Id { get; set; }
        public string Content { get; set; }
        public int Parentthread { get; set; }
        public int Parentcomment { get; set; }
        public int Author { get; set; }
        public int Image { get; set; }

        public virtual Account AuthorNavigation { get; set; }
        public virtual Image ImageNavigation { get; set; }
        public virtual Comment ParentcommentNavigation { get; set; }
        public virtual Thread ParentthreadNavigation { get; set; }
        public virtual ICollection<Comment> InverseParentcommentNavigation { get; set; }
    }
}
