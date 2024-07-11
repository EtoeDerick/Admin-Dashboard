using Admin.Server.Data;
using Admin.Shared.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Admin.Server.Repositories.GcePastPaperPdfs
{
    public class GcePastPaperPdfsRepository : ControllerBase, IGcePastPaperPdfsRepository
    {
        private readonly ApplicationDbContext _context;
        public GcePastPaperPdfsRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<ActionResult<IEnumerable<PdfDownloadDtoGroupedByYear>>> Get(int subjectId)
        {
            var downloads = await _context.Downloadpdfs.Where(d => d.SubjectId == subjectId).ToListAsync();
            var downloaddtos = new List<PdfDownloadDto>();
            var downloadsGroupedByYear = new List<PdfDownloadDtoGroupedByYear>();

            var i = 0;
            foreach(var d in downloads)
            {
                var downloadDto = new PdfDownloadDto()
                {
                    Id = i,
                    Title = d.Title,
                    PaperNumber = d.PaperNumber,
                    PaperYear = d.PaperYear,
                    Thumbnail = d.Thumbnail,
                    Url = d.Url,
                    ZipFileUrl = d.ZipFileUrl
                };
                i = i + 1;
                downloaddtos.Add(downloadDto);
            }
            var downloaddts = downloaddtos.OrderBy(x => x.PaperNumber).GroupBy(x => x.PaperYear);

            foreach(var d in downloaddts)
            {
                var downloadGroupedByYear = new PdfDownloadDtoGroupedByYear()
                {
                    PaperYear = d.Key,
                    DownloadDtos = d.ToList()
                };
                downloadsGroupedByYear.Add(downloadGroupedByYear);
            }

            return downloadsGroupedByYear;
        }
    }
}
