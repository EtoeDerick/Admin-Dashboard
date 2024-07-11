using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Admin.Server.Repositories.GcePastPaperPdfs
{
    public interface IGcePastPaperPdfsRepository
    {
        Task<ActionResult<IEnumerable<Admin.Shared.Dtos.PdfDownloadDtoGroupedByYear>>> Get(int subjectId);
    }
}
