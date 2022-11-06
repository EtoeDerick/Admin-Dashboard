using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admin.Shared.Models
{
    public class Constants
    {
        public int Id { get; set; }

        [MaxLength(250)]
        public string Title { get; set; } //Title of the Constant

        [MaxLength(250)]
        public string Key { get; set; }

        [MaxLength(250)]
        public string Value { get; set; } //The value the  constant should hold

        [MaxLength(250)]
        public string Code { get; set; }

        [MaxLength(1024)]
        public string Description { get; set; } //Text description of the role of the contant

    }
}
