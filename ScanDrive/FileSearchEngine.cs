using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Navigation;
using System.Windows.Threading;

namespace ScanDrive
{
    public class FileSearchEngine
    {
        public FileSearchEngine(CancellationTokenSource cts)
        {
            TotalCount = 0;
            FailedCount = 0;
            this._cts = cts;
            ExtensionSummaries = new List<ExtensionSummary>();

            getDriveInfo();
        }

        public EventHandler ListChanged;
        void OnListChanged()
        {
            ListChanged?.Invoke(this, EventArgs.Empty);
        }

        private void UpdateCount(object sender, EventArgs e)
        {
            if (sender is ExtensionSummary item)
                TotalCount += item.FullPathList.Count;

            ExtensionSummaries.Sort((item1, item2) => item1.FullPathList.Count - item2.FullPathList.Count);

            OnListChanged();
        }

        public void ExpandAll()
        {
            expandAll(_rootDir, _cts.Token);
        }

        private void getDriveInfo()
        {
            DriveInfo drive = DriveInfo.GetDrives().First();

            RootVolume = drive.Name;
            RootVolumeName = drive.VolumeLabel;
            _rootDir = drive.RootDirectory;
        }

        private void addOrUpdate(string extension, long size, string path)
        {
            var item = ExtensionSummaries.Find(s => s.Extension.Equals(extension));

            if (item == null) // Add
            {
                item = new ExtensionSummary(extension, size, path);
                item.Changed += UpdateCount;
                ExtensionSummaries.Add(item);
            }
            else // UpdateCount
            {
                item.AddData(size, path);
            }
        }

        private void expand(DirectoryInfo parent)
        {
            foreach (var f in parent.GetFiles())
                addOrUpdate(f.Extension, f.Length, f.FullName);
        }

        private void expandAll(DirectoryInfo parent, CancellationToken ct)
        {
            if (ct.IsCancellationRequested) return;

            try
            {
                expand(parent);
            }
            catch
            {
                FailedCount++;
            }

            try
            {
                foreach (var dir in parent.GetDirectories())
                    expandAll(dir, ct);
            }
            catch
            {
                FailedCount++;
            }
        }

        public string RootVolume { get; private set; }
        public string RootVolumeName { get; private set; }
        private DirectoryInfo _rootDir;

        public List<ExtensionSummary> ExtensionSummaries { get; }

        public int FailedCount { get; private set; }

        private CancellationTokenSource _cts { get; set; }
        public int TotalCount { get; set; }
    }

    public class ExtensionSummary
    {
        public ExtensionSummary(string extension, long size, string path)
        {
            Extension = extension;
            TotalSize = 0;
            FullPathList = new List<string>();

            AddData(size, path);
        }

        #region Method

        public void AddData(long size, string path)
        {
            TotalSize += size;
            FullPathList.Add(path);

            OnChanged();
        }

        #endregion


        #region Properties
        public string Extension { get; private set; }
        public long TotalSize { get; private set; }
        public List<string> FullPathList { get; private set; }
        #endregion

        #region Event

        public event EventHandler Changed;
        protected void OnChanged()
        {
            Changed?.Invoke(this, new EventArgs());
        }

        #endregion
    }
}
