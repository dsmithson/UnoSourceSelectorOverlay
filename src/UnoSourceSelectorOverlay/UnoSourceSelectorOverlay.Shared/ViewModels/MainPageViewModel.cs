using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;
using Windows.UI;

namespace UnoSourceSelectorOverlay.ViewModels
{
    public class MainPageViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName]string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private Visibility menuVisibility = Visibility.Visible;
        public Visibility MenuVisibility
        {
            get { return menuVisibility; }
            set
            {
                if(menuVisibility != value)
                {
                    menuVisibility = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Source currently selected in source selection UI
        /// </summary>
        public SourceViewModel SelectedSource
        {
            get { return selectedSource; }
            set
            {
                if(selectedSource != value)
                {
                    selectedSource = value;
                    OnPropertyChanged();
                }
            }
        }
        private SourceViewModel selectedSource;

        /// <summary>
        /// Source actively being displayed on the output
        /// </summary>
        public SourceViewModel ActiveSource
        {
            get { return activeSource; }
            set
            {
                if (activeSource != value)
                {
                    activeSource = value;
                    OnPropertyChanged();
                }
            }
        }
        private SourceViewModel activeSource;

        private List<SourceViewModel> sourceList;
        public List<SourceViewModel> SourceList
        {
            get { return sourceList; }
            set
            {
                if(sourceList != value)
                {
                    sourceList = value;
                    OnPropertyChanged();
                }
            }
        }

        private bool sourceListLoading;
        public bool SourceListLoading
        {
            get
            {
                return sourceListLoading;
            }
            set
            {
                if(sourceListLoading != value)
                {
                    sourceListLoading = value;
                    OnPropertyChanged();
                }
            }
        }

        public async Task SelectActiveSource(SourceViewModel source)
        {
            if (source == null)
                return;

            //Simulate some network call to set the source
            await Task.Delay(100);

            //Update our selected source
            this.ActiveSource = source;
        }

        public async Task RefreshSourceList()
        {
            if (SourceListLoading)
                return;

            SourceListLoading = true;

            //Simulate some network call to get a source list
            await Task.Delay(500);

            var newSourceList = new List<SourceViewModel>()
            {
                new SourceViewModel() { Name = "Source 1", Thumbnail = new SolidColorBrush(Colors.Black) },
                new SourceViewModel() { Name = "Source 2", Thumbnail = new SolidColorBrush(Colors.Purple) },
                new SourceViewModel() { Name = "Source 3", Thumbnail = new SolidColorBrush(Colors.Blue) },
                new SourceViewModel() { Name = "Source 4", Thumbnail = new SolidColorBrush(Colors.Brown) },
                new SourceViewModel() { Name = "Source 5", Thumbnail = new SolidColorBrush(Colors.Chartreuse) },
                new SourceViewModel() { Name = "Source 6", Thumbnail = new SolidColorBrush(Colors.Goldenrod) }
            };

            //Set the new Source List
            SourceList = newSourceList;

            //Set a default selected source
            if(newSourceList == null || !newSourceList.Contains(this.SelectedSource))
            {
                this.SelectedSource = newSourceList?.FirstOrDefault();
            }
            SourceListLoading = false;

        }

        public void ToggleMenuVisibility()
        {
            if (MenuVisibility == Visibility.Visible)
                MenuVisibility = Visibility.Collapsed;
            else
                MenuVisibility = Visibility.Visible;
        }
    }
}
