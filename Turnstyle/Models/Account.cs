using System;
using System.Collections.Generic;

namespace Turnstyle.Models
{
    public class Account
    {
        public Account()
        {
            Comment = new HashSet<Comment>();
            Event = new HashSet<Event>();
            ImageNavigation = new HashSet<Image>();
            Teammember = new HashSet<Teammember>();
            Thread = new HashSet<Thread>();
            UserroleGrantedbyNavigation = new HashSet<Userrole>();
            UserroleUser = new HashSet<Userrole>();
        }

        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Image { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime Lastlogin { get; set; }

        public virtual ICollection<Comment> Comment { get; set; }
        public virtual ICollection<Event> Event { get; set; }
        public virtual ICollection<Image> ImageNavigation { get; set; }
        public virtual ICollection<Teammember> Teammember { get; set; }
        public virtual ICollection<Thread> Thread { get; set; }
        public virtual ICollection<Userrole> UserroleGrantedbyNavigation { get; set; }
        public virtual ICollection<Userrole> UserroleUser { get; set; }
    }
}
