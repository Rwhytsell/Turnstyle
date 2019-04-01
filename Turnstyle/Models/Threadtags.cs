using System;
using System.Collections.Generic;

namespace Turnstyle.Models
{
    public class Threadtags
    {
        public int Id { get; set; }
        public int Thread { get; set; }
        public int Tag { get; set; }

        public virtual Tag TagNavigation { get; set; }
        public virtual Thread ThreadNavigation { get; set; }
    }
}
