using System.Xml.Linq;

namespace CSUI_Teams_Sync.Models
{
    public class FilesFolders
    {
        public string id { get; set; }
        public string name { get; set; }
        public FilesFoldersFolder folder { get; set; }
        public FilesFoldersParentReference parentReference { get; set; }
    }
    public class FilesFoldersParentReference
    {
        public string driveId { get; set; }
        public string driveType { get; set; }
    }
    public class FilesFoldersFolder
    {
        public int childCount { get; set; }
    }
}
