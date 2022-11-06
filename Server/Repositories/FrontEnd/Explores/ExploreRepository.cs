using Admin.Server.Data;
using Admin.Shared.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Admin.Server.Repositories.FrontEnd.Explores
{
    public class ExploreRepository : IExploreRepository
    {
        private readonly ApplicationDbContext _context;
        public ExploreRepository(ApplicationDbContext applicationDbContext)
        {
            _context = applicationDbContext;
        }
        public async Task<IEnumerable<ExamCategoryGroup>> GetAllExaminationsByCategory(string categoryType)
        {
            var examGroupCategories = new List<ExamCategoryGroup>();
            
            var examCategories = new List<Shared.Models.ExamCategory>();
            //examCategories = await _context.ExamCategories.ToListAsync();

            if(categoryType == "gce")
            {
                examCategories = await _context.ExamCategories.Where(e => e.Description == "gce").ToListAsync();
            }
            else
            {
                examCategories = await _context.ExamCategories.Where(e => e.Description != "gce").ToListAsync();
            }

            
            

            foreach (var examCategory in examCategories)
            {
                var exams = await _context.Examinations.Where(e => e.ExamCategoryId == examCategory.Id && e.IsApproved == true).ToListAsync();

                var examCategoryDtos = new List<ExamCategoryDto>();

                foreach(var e in exams)
                {
                    var examCategoryDto = new ExamCategoryDto()
                    {
                        Id = e.Id,
                        ExamTitle = e.Title,
                        QuestionRange = e.QuestionRange,
                        ImageUrl = e.ImageUrl,
                        ExamDate = e.WrittenOn,
                        
                        IsExamDateVisible = DateTime.Now > e.WrittenOn.Date ? false : true,
                    };
                    examCategoryDto.IsNotExamDateVisible = !examCategoryDto.IsExamDateVisible;

                    examCategoryDtos.Add(examCategoryDto);
                }

                //var examGroup = new ExamCategoryGroup(examCategory.Title, examCategoryDtos);
                
                examGroupCategories.Add(new ExamCategoryGroup(examCategory.Title, examCategory.CategoryBgColor, examCategory.CategoryTextColor, examCategoryDtos));
            }

            return examGroupCategories;
        }
    }
}
