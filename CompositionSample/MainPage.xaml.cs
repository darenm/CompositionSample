using System.Collections.ObjectModel;
using System.ComponentModel;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using CompositionSample.Models;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace CompositionSample
{
    /// <summary>
    ///     An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page, INotifyPropertyChanged
    {
        private Photo _selectedPhoto;

        public MainPage()
        {
            InitializeComponent();
            PhotosGrid.Loaded += PhotosGrid_Loaded;
            PhotosGrid.SelectionChanged += PhotosGrid_SelectionChanged;
            SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility =
                AppViewBackButtonVisibility.Collapsed;
        }

        public ObservableCollection<Photo> Photos { get; } = Photo.CreatePhotoCollection();

        public Photo SelectedPhoto
        {
            get { return _selectedPhoto; }
            set
            {
                if (_selectedPhoto == value)
                {
                    return;
                }

                _selectedPhoto = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SelectedPhoto)));
            }
        }

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        private void AppBarButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(ListPage));
        }

        private void PhotosGrid_Loaded(object sender, RoutedEventArgs e)
        {
            PhotosGrid.SelectedIndex = 0;
        }

        private void PhotosGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (PhotosGrid.SelectedItem != null)
            {
                SelectedPhoto = (Photo) PhotosGrid.SelectedItem;
            }
        }

        private void UIElement_OnTapped(object sender, TappedRoutedEventArgs e)
        {
            new ContentDialog {Content = "I was clicked", PrimaryButtonText = "Ok", IsPrimaryButtonEnabled = true}
                .ShowAsync();
        }
    }
}