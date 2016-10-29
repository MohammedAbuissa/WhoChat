using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WhoChat.Models
{
    public class Message
    {
        public int ID { get; set; }

        public virtual ApplicationUser To { get; set; }

        public virtual ApplicationUser From { get; set; }

        public string MsgText { get; set; }

        public DateTime DateCreated { get; set; }

        public bool IsRead { get; set; }
    }
}