using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrphanageV3.ViewModel.Tools
{
    public class DownloadDataModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public FileExtentionEnum DataType { get; set; }
        public byte[] Data { get; set; }
    }
}