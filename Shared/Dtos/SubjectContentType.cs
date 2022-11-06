using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admin.Shared.Dtos
{
    public class SubjectContentType
    {
        public int Id { get; set; }
        public string ContentTitle { get; set; }
        public double PercentageComplete { get; set; }
        public string BoxViewPercentageWidth { get; set; }
    }
}
