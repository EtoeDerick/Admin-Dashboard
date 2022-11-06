using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admin.Shared.Models.Tutorials
{
    public class Objective
    {
        public int Id { get; set; }
        public string Title { get; set; }

        //Navigation Properties
        public int ChapterId { get; set; }
        public Chapter Chapter { get; set; }
    }
}
