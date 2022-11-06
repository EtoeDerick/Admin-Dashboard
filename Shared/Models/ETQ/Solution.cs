using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admin.Shared.Models.ETQ
{
    public class Solution
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public string ImageUrl { get; set; }
        public int Position { get; set; }

        //Navigation Properties
        public int SubquestionId { get; set; }
        public Subquestion Subquestion { get; set; }

    }
}
