using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Threading;

namespace ScanDrive
{
    public class ViewModel : INotifyPropertyChanged
    {
        public ViewModel()
        {
            _cancelTokenSource = new CancellationTokenSource();
            StopText = "중지하기";

            Engine = new FileSearchEngine(_cancelTokenSource);

            RootVolume = Engine.RootVolume;
            RootVolumeName = Engine.RootVolumeName;

            Engine.ListChanged += UpdateUI;
        }

        #region Properties
        public string RootVolume { get; }
        public string RootVolumeName { get; }

        private int _totalCount;
        public int TotalCount
        {
            get => _totalCount;
            private set
            {
                _totalCount = value;
                OnPropertyChanged(nameof(TotalCount));
            }
        }

        private int _failedCount;
        public int FailedCount
        {
            get => _failedCount;
            private set
            {
                _failedCount = value;
                OnPropertyChanged(nameof(FailedCount));
            }
        }

        private string _stopText;
        public string StopText
        {
            get => _stopText;
            set
            {
                _stopText = value;
                OnPropertyChanged(nameof(StopText));
            }
        }

        private ExtensionSummary _selectedExtension;
        public ExtensionSummary SelectedExtension
        { 
            get => _selectedExtension; 
            set 
            {
                _selectedExtension = value;
                OnPropertyChanged(nameof(SelectedExtension)); 
            } 
        }

        #endregion

        private FileSearchEngine Engine;
        private CancellationTokenSource _cancelTokenSource;

        private List<ExtensionSummary> _topTenList;
        public List<ExtensionSummary> TopTenList
        {
            get => _topTenList;
            private set
            {
                _topTenList = value;
                OnPropertyChanged(nameof(TopTenList));
            }
        }

        #region Command

        private ICommand _beginCommand;
        public ICommand BeginCommand
        {
            get
            {
                if (_beginCommand == null)
                    _beginCommand = new DelegateCommand(beginExtraction);
                return _beginCommand;
            }
        }

        private ICommand _stopCommand;

        public ICommand StopCommand
        {
            get
            {
                if (_stopCommand == null)
                    _stopCommand = new DelegateCommand(stopExtraction);
                return _stopCommand;
            }
        }

        private ICommand _selectCommand;
        public ICommand SelectCommand
        {
            get
            {
                if (_selectCommand == null)
                    _selectCommand = new DelegateCommand(selectExtension);
                return _selectCommand;
            }
        }
        public void UpdateUI(object sender, EventArgs e)
        {
            TotalCount = Engine.TotalCount;
            FailedCount = Engine.FailedCount;

            TopTenList = Engine.ExtensionSummaries.Take(10).ToList();
        }



        #endregion

        #region INotifyProperties

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

        #region Methods
        private void beginExtraction()
        {
            //Engine.ExpandAll();
            var task = Task.Factory.StartNew(Engine.ExpandAll, _cancelTokenSource.Token);
        }

        private void stopExtraction()
        {
            _cancelTokenSource.Cancel();
            StopText = "중지됨";
        }

        private void selectExtension()
        {
            // Extension List
            //ExtensionList?.Clear();

            //foreach (var node in NodeList)
            //{
            //    if (node.Extension == SelectedExtension)
            //        ExtensionList.Add(node.FullPath);
            //}
        }

        #endregion
    }
}