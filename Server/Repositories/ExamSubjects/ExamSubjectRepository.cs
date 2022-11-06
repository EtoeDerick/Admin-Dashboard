using Admin.Server.Data;
using Admin.Shared.Dtos;
using Admin.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Admin.Server.Repositories.ExamSubjects
{
    public class ExamSubjectRepository : /*ControllerBase,*/ IExamSubjectRepository
    {
        private readonly ApplicationDbContext _context;
        public ExamSubjectRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Subject>> Get(string examId)
        {
            var subjects = await _context.Subjects.Where(s => s.ExaminationId == examId).ToListAsync();
            //var subjs = await _context.Subjects.ToListAsync();

            if (subjects == null)
            {
                return null;
            }

            return subjects;
        }

        public async Task<IEnumerable<McqPastPaperFormDto>> GetAllexamSubjects()
        {
            var exams = from examsubjects in _context.Examinations
                                    .Include(e => e.Subjects)
                        select new McqPastPaperFormDto
                        {
                            ExamTitle = examsubjects.Title,
                            ExamId = examsubjects.Id,
                             McqSubjects = new List<McqSubjectFormDto>() { }
                        };
            var allSubs = await exams.ToListAsync();

            foreach(var s in allSubs)
            {
                var subs = from subj in _context.Subjects.Where(mysub => mysub.ExaminationId == s.ExamId)
                           select new McqSubjectFormDto()
                           {
                               SubjectTitle = subj.Title,
                               SubjectId = subj.Id
                           };
                s.McqSubjects = await subs.ToListAsync();
            }

            return allSubs;
        }
    }
}
