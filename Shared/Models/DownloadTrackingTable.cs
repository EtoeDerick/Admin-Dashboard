using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admin.Shared.Models
{
    public class DownloadTrackingTable
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string ObjectId { get; set; }
        public bool IsDownloaded { get; set; }
        public string PastPaperId { get; set; }
        public DateTime Date { get; set; }
        public string DownloadSize { get; set; }
    }
}
