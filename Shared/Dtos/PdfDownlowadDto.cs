using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admin.Shared.Dtos
{
    public class PdfDownloadDto
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string PaperYear { get; set; }

        public int PaperNumber { get; set; }

        public string Thumbnail { get; set; }

        public string Url { get; set; }
        public string ZipFileUrl { get; set; }
    }
    public class PdfDownloadDtoGroupedByYear
    {
        public string PaperYear { get; set; }
        public List<PdfDownloadDto> DownloadDtos { get; set; }
    }
}
