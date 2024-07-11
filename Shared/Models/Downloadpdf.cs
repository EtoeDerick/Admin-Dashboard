using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admin.Shared.Models
{
    public class Downloadpdf
    {
        [Key]
        public string Id { get; set; }

        [MaxLength(50), Required]
        public string Title { get; set; }

        [MaxLength(30), Required]
        public string PaperYear { get; set; }

        [Required]
        public int PaperNumber { get; set; }

        [MaxLength(2048)]
        public string Thumbnail { get; set; }

        [MaxLength(1024)]
        public string Url { get; set; }

        [MaxLength(1024)]
        public string ZipFileUrl { get; set; }
        public bool IsApproved { get; set; }
        public bool IsFree { get; set; }

        //Grants the user ability to use some past papers
        //Status = true ==> This given past paper is free even if the student must pay for the subject

        //Navigation Properties
        public int SubjectId { get; set; }

        public string SetId()
        {
            Id = SubjectId.ToString() + "_Download" + "_" + PaperNumber + "_" + PaperYear;
            return Id;
        }
    }
}
