using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WhoChat.Models
{
    public class CryptoSettings
    {
        public int Id { get; set; }

        public byte[] Key { get; set; }

        public byte[] IV { get; set; }

        public ApplicationUser User { get; set; }
    }
}