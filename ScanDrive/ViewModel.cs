using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;

namespace ScanDrive
{
    public class ViewModel
    {
        public ViewModel()
        {
            var engine = new FileSearchEngine();

            RootVolume = engine.RootVolume;
            TotalCount = engine.TotalCount;
            engine.fileSummaries.ToList();
            TopTenList = new ObservableCollection<FileSummary>();
            SelectedExtension = TopTenList.First<FileSummary>();
        }

        public int TotalCount { get; private set; }
        public FileSummary SelectedExtension { get; set; }
        public ObservableCollection<FileSummary> TopTenList { get; private set; }
        public string RootVolume { get; }
    }
}
