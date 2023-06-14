using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Runtime.InteropServices;

namespace ScanDrive
{
    public class FileSummary
    {
        public FileSummary(FileInfo f)
        {
            this.extension = f.Extension;
            this.count = 0;
            this.size = 0;
            this.PathList = new List<string>();

            AddValues(f);
        }

        public void AddValues(FileInfo f)
        {
            if (this.extension != f.Extension)
                throw new ArgumentException();

            this.count++;
            this.size += f.Length;
            this.PathList.Add(f.FullName);
        }

        #region Properties

        public string extension { get; private set; }
        public int count { get; set; }
        public long size { get; set; }
        public readonly List<string> PathList;

        #endregion
    }
}
