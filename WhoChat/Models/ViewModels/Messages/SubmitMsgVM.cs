using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WhoChat.Models.ViewModels.Messages
{
    public class SubmitMsgVM
    {
        public string Key { get; set; }

        public string IV { get; set; }

        public string MsgText { get; set; }

        public string ToEmail { get; set; }
    }
}