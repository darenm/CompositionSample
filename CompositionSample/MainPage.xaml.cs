using CompositionSample.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace CompositionSample
{
   /// <summary>
   /// An empty page that can be used on its own or navigated to within a Frame.
   /// </summary>
    public sealed partial class MainPage : Page, INotifyPropertyChanged
    {
        private Photo _selectedPhoto;

        public Photo SelectedPhoto
        {
            get { return _selectedPhoto; }
            set
            {
                if (_selectedPhoto == value)
                    return;

                _selectedPhoto = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SelectedPhoto)));
            }
        }

        public ObservableCollection<Photo> Photos { get; } = Photo.CreatePhotoCollection();

        public MainPage()
        {
            this.InitializeComponent();
            PhotosGrid.Loaded += PhotosGrid_Loaded;
            PhotosGrid.SelectionChanged += PhotosGrid_SelectionChanged;
            SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = AppViewBackButtonVisibility.Collapsed;

        }

        private void PhotosGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (PhotosGrid.SelectedItem != null)
            {
                SelectedPhoto = (Photo)PhotosGrid.SelectedItem;
            }
        }

        private void PhotosGrid_Loaded(object sender, RoutedEventArgs e)
        {
            PhotosGrid.SelectedIndex = 0;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void AppBarButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(ListPage));
        }
    }
}
