﻿using System;
using System.Collections.ObjectModel;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

namespace CompositionSample.Models
{
    public class Photo
    {
        public ImageSource PhotoSource => new BitmapImage(Url);
        public Uri Url { get; set; }

        public static ObservableCollection<Photo> CreatePhotoCollection()
        {
            return new ObservableCollection<Photo>
            {
                new Photo {Url = new Uri("ms-appx:///Assets/1.jpg")},
                new Photo {Url = new Uri("ms-appx:///Assets/2.jpg")},
                new Photo {Url = new Uri("ms-appx:///Assets/3.jpg")},
                new Photo {Url = new Uri("ms-appx:///Assets/4.jpg")},
                new Photo {Url = new Uri("ms-appx:///Assets/5.jpg")},
                new Photo {Url = new Uri("ms-appx:///Assets/6.jpg")},
                new Photo {Url = new Uri("ms-appx:///Assets/7.jpg")},
                new Photo {Url = new Uri("ms-appx:///Assets/8.jpg")},
                new Photo {Url = new Uri("ms-appx:///Assets/9.jpg")},
                new Photo {Url = new Uri("ms-appx:///Assets/10.jpg")},
                new Photo {Url = new Uri("ms-appx:///Assets/11.jpg")},
                new Photo {Url = new Uri("ms-appx:///Assets/12.jpg")},
                new Photo {Url = new Uri("ms-appx:///Assets/1.jpg")},
                new Photo {Url = new Uri("ms-appx:///Assets/2.jpg")},
                new Photo {Url = new Uri("ms-appx:///Assets/3.jpg")},
                new Photo {Url = new Uri("ms-appx:///Assets/4.jpg")},
                new Photo {Url = new Uri("ms-appx:///Assets/5.jpg")},
                new Photo {Url = new Uri("ms-appx:///Assets/6.jpg")},
                new Photo {Url = new Uri("ms-appx:///Assets/7.jpg")},
                new Photo {Url = new Uri("ms-appx:///Assets/8.jpg")},
                new Photo {Url = new Uri("ms-appx:///Assets/9.jpg")},
                new Photo {Url = new Uri("ms-appx:///Assets/10.jpg")},
                new Photo {Url = new Uri("ms-appx:///Assets/11.jpg")},
                new Photo {Url = new Uri("ms-appx:///Assets/12.jpg")},
                new Photo {Url = new Uri("ms-appx:///Assets/1.jpg")},
                new Photo {Url = new Uri("ms-appx:///Assets/2.jpg")},
                new Photo {Url = new Uri("ms-appx:///Assets/3.jpg")},
                new Photo {Url = new Uri("ms-appx:///Assets/4.jpg")},
                new Photo {Url = new Uri("ms-appx:///Assets/5.jpg")},
                new Photo {Url = new Uri("ms-appx:///Assets/6.jpg")},
                new Photo {Url = new Uri("ms-appx:///Assets/7.jpg")},
                new Photo {Url = new Uri("ms-appx:///Assets/8.jpg")},
                new Photo {Url = new Uri("ms-appx:///Assets/9.jpg")},
                new Photo {Url = new Uri("ms-appx:///Assets/10.jpg")},
                new Photo {Url = new Uri("ms-appx:///Assets/11.jpg")},
                new Photo {Url = new Uri("ms-appx:///Assets/12.jpg")},
                new Photo {Url = new Uri("ms-appx:///Assets/1.jpg")},
                new Photo {Url = new Uri("ms-appx:///Assets/2.jpg")},
                new Photo {Url = new Uri("ms-appx:///Assets/3.jpg")},
                new Photo {Url = new Uri("ms-appx:///Assets/4.jpg")},
                new Photo {Url = new Uri("ms-appx:///Assets/5.jpg")},
                new Photo {Url = new Uri("ms-appx:///Assets/6.jpg")},
                new Photo {Url = new Uri("ms-appx:///Assets/7.jpg")},
                new Photo {Url = new Uri("ms-appx:///Assets/8.jpg")},
                new Photo {Url = new Uri("ms-appx:///Assets/9.jpg")},
                new Photo {Url = new Uri("ms-appx:///Assets/10.jpg")},
                new Photo {Url = new Uri("ms-appx:///Assets/11.jpg")},
                new Photo {Url = new Uri("ms-appx:///Assets/12.jpg")},
                new Photo {Url = new Uri("ms-appx:///Assets/1.jpg")},
                new Photo {Url = new Uri("ms-appx:///Assets/2.jpg")},
                new Photo {Url = new Uri("ms-appx:///Assets/3.jpg")},
                new Photo {Url = new Uri("ms-appx:///Assets/4.jpg")},
                new Photo {Url = new Uri("ms-appx:///Assets/5.jpg")},
                new Photo {Url = new Uri("ms-appx:///Assets/6.jpg")},
                new Photo {Url = new Uri("ms-appx:///Assets/7.jpg")},
                new Photo {Url = new Uri("ms-appx:///Assets/8.jpg")},
                new Photo {Url = new Uri("ms-appx:///Assets/9.jpg")},
                new Photo {Url = new Uri("ms-appx:///Assets/10.jpg")},
                new Photo {Url = new Uri("ms-appx:///Assets/11.jpg")},
                new Photo {Url = new Uri("ms-appx:///Assets/12.jpg")},
                new Photo {Url = new Uri("ms-appx:///Assets/1.jpg")},
                new Photo {Url = new Uri("ms-appx:///Assets/2.jpg")},
                new Photo {Url = new Uri("ms-appx:///Assets/3.jpg")},
                new Photo {Url = new Uri("ms-appx:///Assets/4.jpg")},
                new Photo {Url = new Uri("ms-appx:///Assets/5.jpg")},
                new Photo {Url = new Uri("ms-appx:///Assets/6.jpg")},
                new Photo {Url = new Uri("ms-appx:///Assets/7.jpg")},
                new Photo {Url = new Uri("ms-appx:///Assets/8.jpg")},
                new Photo {Url = new Uri("ms-appx:///Assets/9.jpg")},
                new Photo {Url = new Uri("ms-appx:///Assets/10.jpg")},
                new Photo {Url = new Uri("ms-appx:///Assets/11.jpg")},
                new Photo {Url = new Uri("ms-appx:///Assets/12.jpg")},
                new Photo {Url = new Uri("ms-appx:///Assets/1.jpg")},
                new Photo {Url = new Uri("ms-appx:///Assets/2.jpg")},
                new Photo {Url = new Uri("ms-appx:///Assets/3.jpg")},
                new Photo {Url = new Uri("ms-appx:///Assets/4.jpg")},
                new Photo {Url = new Uri("ms-appx:///Assets/5.jpg")},
                new Photo {Url = new Uri("ms-appx:///Assets/6.jpg")},
                new Photo {Url = new Uri("ms-appx:///Assets/7.jpg")},
                new Photo {Url = new Uri("ms-appx:///Assets/8.jpg")},
                new Photo {Url = new Uri("ms-appx:///Assets/9.jpg")},
                new Photo {Url = new Uri("ms-appx:///Assets/10.jpg")},
                new Photo {Url = new Uri("ms-appx:///Assets/11.jpg")},
                new Photo {Url = new Uri("ms-appx:///Assets/12.jpg")},
                new Photo {Url = new Uri("ms-appx:///Assets/1.jpg")},
                new Photo {Url = new Uri("ms-appx:///Assets/2.jpg")},
                new Photo {Url = new Uri("ms-appx:///Assets/3.jpg")},
                new Photo {Url = new Uri("ms-appx:///Assets/4.jpg")},
                new Photo {Url = new Uri("ms-appx:///Assets/5.jpg")},
                new Photo {Url = new Uri("ms-appx:///Assets/6.jpg")},
                new Photo {Url = new Uri("ms-appx:///Assets/7.jpg")},
                new Photo {Url = new Uri("ms-appx:///Assets/8.jpg")},
                new Photo {Url = new Uri("ms-appx:///Assets/9.jpg")},
                new Photo {Url = new Uri("ms-appx:///Assets/10.jpg")},
                new Photo {Url = new Uri("ms-appx:///Assets/11.jpg")},
                new Photo {Url = new Uri("ms-appx:///Assets/12.jpg")},
                new Photo {Url = new Uri("ms-appx:///Assets/1.jpg")},
                new Photo {Url = new Uri("ms-appx:///Assets/2.jpg")},
                new Photo {Url = new Uri("ms-appx:///Assets/3.jpg")},
                new Photo {Url = new Uri("ms-appx:///Assets/4.jpg")},
                new Photo {Url = new Uri("ms-appx:///Assets/5.jpg")},
                new Photo {Url = new Uri("ms-appx:///Assets/6.jpg")},
                new Photo {Url = new Uri("ms-appx:///Assets/7.jpg")},
                new Photo {Url = new Uri("ms-appx:///Assets/8.jpg")},
                new Photo {Url = new Uri("ms-appx:///Assets/9.jpg")},
                new Photo {Url = new Uri("ms-appx:///Assets/10.jpg")},
                new Photo {Url = new Uri("ms-appx:///Assets/11.jpg")},
                new Photo {Url = new Uri("ms-appx:///Assets/12.jpg")},
                new Photo {Url = new Uri("ms-appx:///Assets/1.jpg")},
                new Photo {Url = new Uri("ms-appx:///Assets/2.jpg")},
                new Photo {Url = new Uri("ms-appx:///Assets/3.jpg")},
                new Photo {Url = new Uri("ms-appx:///Assets/4.jpg")},
                new Photo {Url = new Uri("ms-appx:///Assets/5.jpg")},
                new Photo {Url = new Uri("ms-appx:///Assets/6.jpg")},
                new Photo {Url = new Uri("ms-appx:///Assets/7.jpg")},
                new Photo {Url = new Uri("ms-appx:///Assets/8.jpg")},
                new Photo {Url = new Uri("ms-appx:///Assets/9.jpg")},
                new Photo {Url = new Uri("ms-appx:///Assets/10.jpg")},
                new Photo {Url = new Uri("ms-appx:///Assets/11.jpg")},
                new Photo {Url = new Uri("ms-appx:///Assets/12.jpg")},
                new Photo {Url = new Uri("ms-appx:///Assets/1.jpg")},
                new Photo {Url = new Uri("ms-appx:///Assets/2.jpg")},
                new Photo {Url = new Uri("ms-appx:///Assets/3.jpg")},
                new Photo {Url = new Uri("ms-appx:///Assets/4.jpg")},
                new Photo {Url = new Uri("ms-appx:///Assets/5.jpg")},
                new Photo {Url = new Uri("ms-appx:///Assets/6.jpg")},
                new Photo {Url = new Uri("ms-appx:///Assets/7.jpg")},
                new Photo {Url = new Uri("ms-appx:///Assets/8.jpg")},
                new Photo {Url = new Uri("ms-appx:///Assets/9.jpg")},
                new Photo {Url = new Uri("ms-appx:///Assets/10.jpg")},
                new Photo {Url = new Uri("ms-appx:///Assets/11.jpg")},
                new Photo {Url = new Uri("ms-appx:///Assets/12.jpg")},
                new Photo {Url = new Uri("ms-appx:///Assets/1.jpg")},
                new Photo {Url = new Uri("ms-appx:///Assets/2.jpg")},
                new Photo {Url = new Uri("ms-appx:///Assets/3.jpg")},
                new Photo {Url = new Uri("ms-appx:///Assets/4.jpg")},
                new Photo {Url = new Uri("ms-appx:///Assets/5.jpg")},
                new Photo {Url = new Uri("ms-appx:///Assets/6.jpg")},
                new Photo {Url = new Uri("ms-appx:///Assets/7.jpg")},
                new Photo {Url = new Uri("ms-appx:///Assets/8.jpg")},
                new Photo {Url = new Uri("ms-appx:///Assets/9.jpg")},
                new Photo {Url = new Uri("ms-appx:///Assets/10.jpg")},
                new Photo {Url = new Uri("ms-appx:///Assets/11.jpg")},
                new Photo {Url = new Uri("ms-appx:///Assets/12.jpg")},
                new Photo {Url = new Uri("ms-appx:///Assets/1.jpg")},
                new Photo {Url = new Uri("ms-appx:///Assets/2.jpg")},
                new Photo {Url = new Uri("ms-appx:///Assets/3.jpg")},
                new Photo {Url = new Uri("ms-appx:///Assets/4.jpg")},
                new Photo {Url = new Uri("ms-appx:///Assets/5.jpg")},
                new Photo {Url = new Uri("ms-appx:///Assets/6.jpg")},
                new Photo {Url = new Uri("ms-appx:///Assets/7.jpg")},
                new Photo {Url = new Uri("ms-appx:///Assets/8.jpg")},
                new Photo {Url = new Uri("ms-appx:///Assets/9.jpg")},
                new Photo {Url = new Uri("ms-appx:///Assets/10.jpg")},
                new Photo {Url = new Uri("ms-appx:///Assets/11.jpg")},
                new Photo {Url = new Uri("ms-appx:///Assets/12.jpg")},
                new Photo {Url = new Uri("ms-appx:///Assets/1.jpg")},
                new Photo {Url = new Uri("ms-appx:///Assets/2.jpg")},
                new Photo {Url = new Uri("ms-appx:///Assets/3.jpg")},
                new Photo {Url = new Uri("ms-appx:///Assets/4.jpg")},
                new Photo {Url = new Uri("ms-appx:///Assets/5.jpg")},
                new Photo {Url = new Uri("ms-appx:///Assets/6.jpg")},
                new Photo {Url = new Uri("ms-appx:///Assets/7.jpg")},
                new Photo {Url = new Uri("ms-appx:///Assets/8.jpg")},
                new Photo {Url = new Uri("ms-appx:///Assets/9.jpg")},
                new Photo {Url = new Uri("ms-appx:///Assets/10.jpg")},
                new Photo {Url = new Uri("ms-appx:///Assets/11.jpg")},
                new Photo {Url = new Uri("ms-appx:///Assets/12.jpg")},
                new Photo {Url = new Uri("ms-appx:///Assets/1.jpg")},
                new Photo {Url = new Uri("ms-appx:///Assets/2.jpg")},
                new Photo {Url = new Uri("ms-appx:///Assets/3.jpg")},
                new Photo {Url = new Uri("ms-appx:///Assets/4.jpg")},
                new Photo {Url = new Uri("ms-appx:///Assets/5.jpg")},
                new Photo {Url = new Uri("ms-appx:///Assets/6.jpg")},
                new Photo {Url = new Uri("ms-appx:///Assets/7.jpg")},
                new Photo {Url = new Uri("ms-appx:///Assets/8.jpg")},
                new Photo {Url = new Uri("ms-appx:///Assets/9.jpg")},
                new Photo {Url = new Uri("ms-appx:///Assets/10.jpg")},
                new Photo {Url = new Uri("ms-appx:///Assets/11.jpg")},
                new Photo {Url = new Uri("ms-appx:///Assets/12.jpg")}
            };
        }
    }
}