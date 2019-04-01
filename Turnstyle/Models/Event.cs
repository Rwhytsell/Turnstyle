using System;
using System.Collections.Generic;

namespace Turnstyle.Models
{
    public class Event
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime Eventdate { get; set; }
        public string Opponent { get; set; }
        public bool Isvisible { get; set; }
        public int Createdby { get; set; }

        public virtual Account CreatedbyNavigation { get; set; }
    }
}
