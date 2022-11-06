using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admin.Shared.Dtos
{
    public class EssayPaperNTopicsDto
    {
        public List<ETQTopicsDto> ETQTopicsDtos { get; set; }
        public List<PastPaper2n3Dto> PastPaper2N3Dtos { get; set; }

        public EssayPaperNTopicsDto()
        {
            ETQTopicsDtos = new List<ETQTopicsDto>();
            PastPaper2N3Dtos = new List<PastPaper2n3Dto>();
        }
    }
}
