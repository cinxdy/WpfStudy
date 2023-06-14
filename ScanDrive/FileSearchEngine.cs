using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ScanDrive
{
    public class FileSearchEngine
    {
        public FileSearchEngine() 
        {
            GetRoot();
            TotalCount = 0;
        }

        public void GetRoot()
        {
            DriveInfo drive = DriveInfo.GetDrives().First<DriveInfo>();

            RootVolume = drive.Name;
            RootDir = drive.RootDirectory;
        }

        public void Expand(DirectoryInfo parent)
        {
            foreach (var f in parent.GetFiles())
            {
                if (fileSummaries.ContainsKey(f.Extension))
                    fileSummaries[f.Extension].AddValues(f);
                else
                    fileSummaries[f.Extension] = new FileSummary(f);

                TotalCount++;
            }
        }

        public void ExpandAll(DirectoryInfo parent)
        {
            Expand(parent);
            foreach (var dir in parent.GetDirectories())
                ExpandAll(dir);
        }

        public readonly Dictionary<string, FileSummary> fileSummaries = new Dictionary<string, FileSummary>();

        public string RootVolume { get; private set; }
        public DirectoryInfo RootDir { get; private set; }
        public int TotalCount { get; private set; }

    }

}
