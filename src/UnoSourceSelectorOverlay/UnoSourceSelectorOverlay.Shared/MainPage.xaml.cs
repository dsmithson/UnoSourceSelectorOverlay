using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using UnoSourceSelectorOverlay.ViewModels;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.System;
using Windows.UI.Core;
using System.Threading.Tasks;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace UnoSourceSelectorOverlay
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();

            CoreWindow.GetForCurrentThread().Dispatcher.AcceleratorKeyActivated += Dispatcher_AcceleratorKeyActivated;
        }

        private VirtualKey lastKey;
        private async void Dispatcher_AcceleratorKeyActivated(CoreDispatcher sender, AcceleratorKeyEventArgs e)
        {
            if (e.EventType != CoreAcceleratorKeyEventType.KeyDown)
                return;

            if (e.VirtualKey == VirtualKey.Scroll)
            {
                if (lastKey == VirtualKey.Scroll)
                {
                    //Double scroll lock
                    viewModel.ToggleMenuVisibility();

                    if (sourceList.Visibility == Visibility.Visible)
                    {
                        await Task.Delay(50);
                        sourceList.Focus(FocusState.Programmatic);
                    }

                    //Force different last key so two new ScrLk presses are requried
                    lastKey = VirtualKey.A;
                    return;
                }
            }
            else if (e.VirtualKey == VirtualKey.Escape)
            {
                //Hide the menu on return or enter
                viewModel.MenuVisibility = Visibility.Collapsed;
            }
            else if (e.VirtualKey == VirtualKey.Enter)
            {
                //Select the currently selected source in our listView
                if(sourceList.SelectedItems.Count > 0)
                    await viewModel.SelectActiveSource(viewModel.SelectedSource);
            }

            lastKey = e.VirtualKey;
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            await viewModel.RefreshSourceList();
        }
    }
}
