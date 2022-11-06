using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admin.Shared.Dtos
{
    public class ExamCategoryDto
    {
        public string Id { get; set; }
        public string ExamTitle { get; set; }
        public string QuestionRange { get; set; }
        public string ImageUrl { get; set; }
        public DateTime ExamDate { get; set; }
        public bool IsExamDateVisible { get; set; } = false;

        public bool IsNotExamDateVisible { get; set; } = false;
    }

    public class ExamCategoryGroup
    {
        public string CategoryName { get; set; }
        public string CategoryBgColor { get; set; } = "#D6EAF8";
        public string CategoryTextColor { get; set; } = "DodgerBlue";
        public List<ExamCategoryDto> ExamCategoryDtos { get; set; } = new List<ExamCategoryDto>();
        public ExamCategoryGroup(string categoryName, List<ExamCategoryDto> examCategories) 
        {
            CategoryName = categoryName;
            ExamCategoryDtos = examCategories;
        }
        public ExamCategoryGroup(string categoryName, string bgColor, string txtColor, List<ExamCategoryDto> examCategories)
        {
            CategoryName = categoryName;
            ExamCategoryDtos = examCategories;
            CategoryBgColor = bgColor;
            CategoryTextColor = txtColor;
        }
    }
}
