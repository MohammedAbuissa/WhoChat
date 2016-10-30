using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WhoChat.Models.ViewModels.Messages
{
    public class MessageBubble
    {
        public bool Me { get; set; }

        public Message Msg { get; set; }
    }
}