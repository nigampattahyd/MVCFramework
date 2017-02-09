using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMHub.Helper
{
    public class Content
    {
        public long bytes { get; set; }
        public string client_mtime { get; set; }
        public string contents { get; set; }
        public string Extension { get; set; }
        public string hash { get; set; }
        public string icon { get; set; }
        public string is_deleted { get; set; }
        public string is_dir { get; set; }
        public string mime_type { get; set; }
        public string modified { get; set; }
        public string modifier { get; set; }
        public string Name { get; set; }
        public string parent_shared_folder_id { get; set; }
        public string path { get; set; }
        public string photo_info { get; set; }
        public string read_only { get; set; }
        public string rev { get; set; }
        public string revision { get; set; }
        public string root { get; set; }
        public string shared_folder { get; set; }
        public string size { get; set; }
        public string thumb_exists { get; set; }
        public string video_info { get; set; }
    }
}
