using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admin.Shared.Models
{
    public class Address
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        [MaxLength(250)]
        public string UserName { get; set; }
        [MaxLength(24)]
        public string RegionOfOrigin { get; set; }
        [MaxLength(24)]
        public string Town { get; set; }
        [MaxLength(24)]
        public string Phone { get; set; }
        [MaxLength(100)]
        public string School { get; set; }
    }
}
