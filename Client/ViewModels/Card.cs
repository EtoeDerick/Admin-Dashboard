using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Admin.Client.ViewModels
{
    public class Card
    {
        public int id { get; set; }
        public string backgroundColor { get; set; }
        public int count { get; set; }
        public string description { get; set; }
        public string icon { get; set; }
        public string url { get; set; }
    }
}
