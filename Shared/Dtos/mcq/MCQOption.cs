using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admin.Shared.Dtos.mcq
{
    public class MCQOption
    {
        public int Id { get; set; }
        public string Option { get; set; }
        public string OptionSelectedValue { get; set; } = string.Empty;
        public string AnswerStatus { get; set; }
    }
}
