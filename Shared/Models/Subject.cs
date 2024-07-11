using Admin.Shared.Models.Tutorials;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admin.Shared.Models
{
    public class Subject
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [MaxLength(80), Required]
        public string Title { get; set; }

        [MaxLength(1024)]
        public string Description { get; set; }

        [MaxLength(255)]
        public string ImageUrl { get; set; }

        [MaxLength(255)]
        public string MarqueeImageUrl { get; set; }
        public decimal Price { get; set; }
        public int MonthlyPrice { get; set; }
        public int TenMonths { get; set; }

        [MaxLength(50)]
        public string Year { get; set; }

        [MaxLength(20)]
        public string SubjectExamNickName { get; set; } = "GCE";

        [MaxLength(20)]
        public string Category { get; set; }

        public bool IsFree { get; set; } = true;

        public bool IsApproved { get; set; } = true;


        [MaxLength(1220)]
        public string VideoPreviewUrl { get; set; }

        //To create UserSubjectDto
        public bool IsPaper1ContentAvailable { get; set; } = true;
        public bool IsPaper2ContentAvailable { get; set; }
        public bool IsPaper3ContentAvailable { get; set; }
        public bool IsTutorialContentAvailable { get; set; }

        //Navigation Properties

        public virtual List<InstructorSubject> InstructorSubjects { get; set; } = new List<InstructorSubject>();


        public string ExaminationId { get; set; }
        public virtual Examination Examination { get; set; } = new Examination();

        public virtual List<UserSubject> UserSubjects { get; set; } = new List<UserSubject>();

        public virtual List<PastPaper> PastPapers { get; set; } = new List<PastPaper>();
        public virtual List<Downloadpdf> Downloads { get; set; } = new List<Downloadpdf>();

        public virtual List<Topic> Topics { get; set; } = new List<Topic>();

        public virtual List<Tutorial> Tutorials { get; set; } = new List<Tutorial>();

        public virtual List<MCQ> MCQS { get; set; } = new List<MCQ>();
        public virtual List<Chapter> Chapters { get; set; } = new List<Chapter>();
    }
}

