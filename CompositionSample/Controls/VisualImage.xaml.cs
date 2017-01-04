﻿using System;
using System.Numerics;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Composition;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Hosting;
using Microsoft.Graphics.Canvas.Effects;
using Robmikh.CompositionSurfaceFactory;

namespace CompositionSample.Controls
{
    public partial class VisualImage : UserControl
    {
        public static readonly DependencyProperty ImageUriProperty = DependencyProperty.Register(
            "ImageUri", typeof(Uri), typeof(VisualImage), new PropertyMetadata(default(Uri), ImageUriChanged));

        public static readonly DependencyProperty ImageMarginProperty =
            DependencyProperty.Register("ImageMargin", typeof(Thickness), typeof(VisualImage),
                new PropertyMetadata(new Thickness()));

        public static readonly DependencyProperty ShowBlurredBackgroundProperty = DependencyProperty.Register(
            "ShowBlurredBackground", typeof(bool), typeof(VisualImage),
            new PropertyMetadata(true, ShowBlurredBackgroundChanged));

        public static readonly DependencyProperty ImageHorizontalAlignmentProperty = DependencyProperty.Register(
            "ImageHorizontalAlignment", typeof(HorizontalAlignment), typeof(VisualImage),
            new PropertyMetadata(default(HorizontalAlignment)));

        public static readonly DependencyProperty ImageVerticalAlignmentProperty = DependencyProperty.Register(
            "ImageVerticalAlignment", typeof(VerticalAlignment), typeof(VisualImage),
            new PropertyMetadata(default(VerticalAlignment)));

        private static SurfaceFactory _surfaceFactoryInstance;
        private static Compositor _compositor;
        private static ScalarKeyFrameAnimation _opacityAnimation;

        private readonly CompositionEffectBrush _blurBrush;
        private CompositionSurfaceBrush _backgroundBrush;
        private CompositionSurfaceBrush _foregroundBrush;
        private DropShadow _foregroundVisualShadow;
        private Uri _lastImageUri;
        private Visual _rootVisual;

        private UriSurface _uriSurface;

        public VisualImage()
        {
            InitializeComponent();
            EnsureCompositor();
            EnsureSurfaceFactory();

            _rootVisual = ElementCompositionPreview.GetElementVisual(this);
            BackgroundVisual = _compositor.CreateSpriteVisual();
            ForegroundVisual = _compositor.CreateSpriteVisual();
            ContainerVisual = _compositor.CreateContainerVisual();
            ContainerVisual.Children.InsertAtBottom(BackgroundVisual);
            ContainerVisual.Children.InsertAtTop(ForegroundVisual);
            ElementCompositionPreview.SetElementChildVisual(this, ContainerVisual);
            SizeChanged +=
                (s, args) =>
                {
                    BackgroundVisual.Size = new Vector2((float) args.NewSize.Width, (float) args.NewSize.Height);
                    ForegroundVisual.Size = new Vector2(
                        (float) args.NewSize.Width - (float) ImageMargin.Left - (float) ImageMargin.Right,
                        (float) args.NewSize.Height - (float) ImageMargin.Top - (float) ImageMargin.Bottom);
                };

            _blurBrush = BuildBlurBrush();

            Loaded += VisualImage_Loaded;
            Unloaded += VisualImage_Unloaded;
        }

        public SpriteVisual BackgroundVisual { get; }
        public ContainerVisual ContainerVisual { get; }
        public SpriteVisual ForegroundVisual { get; }

        public HorizontalAlignment ImageHorizontalAlignment
        {
            get { return (HorizontalAlignment) GetValue(ImageHorizontalAlignmentProperty); }
            set { SetValue(ImageHorizontalAlignmentProperty, value); }
        }

        public Thickness ImageMargin
        {
            get { return (Thickness) GetValue(ImageMarginProperty); }
            set { SetValue(ImageMarginProperty, value); }
        }

        public Uri ImageUri
        {
            get { return (Uri) GetValue(ImageUriProperty); }
            set { SetValue(ImageUriProperty, value); }
        }

        public VerticalAlignment ImageVerticalAlignment
        {
            get { return (VerticalAlignment) GetValue(ImageVerticalAlignmentProperty); }
            set { SetValue(ImageVerticalAlignmentProperty, value); }
        }

        public bool ShowBlurredBackground
        {
            get { return (bool) GetValue(ShowBlurredBackgroundProperty); }
            set { SetValue(ShowBlurredBackgroundProperty, value); }
        }

        public async Task LoadImageAsync(Uri imageUri)
        {
            try
            {
                if (_lastImageUri == imageUri)
                {
                    return;
                }
                _lastImageUri = imageUri;

                if (_uriSurface == null)
                {
                    _uriSurface = await _surfaceFactoryInstance.CreateUriSurfaceAsync(imageUri);
                }
                else
                {
                    await _uriSurface.RedrawSurfaceAsync(imageUri);
                }

                _backgroundBrush = _compositor.CreateSurfaceBrush(_uriSurface.Surface);
                _backgroundBrush.Stretch = CompositionStretch.Fill;
                _blurBrush.SetSourceParameter("source", _backgroundBrush);

                BackgroundVisual.Brush = _blurBrush;
                BackgroundVisual.Size = new Vector2((float) ActualWidth, (float) ActualHeight);
                BackgroundVisual.Opacity = ShowBlurredBackground ? 1.0f : 0;


                _foregroundBrush = _compositor.CreateSurfaceBrush(_uriSurface.Surface);
                _foregroundBrush.Stretch = CompositionStretch.Uniform;

                // center the image
                switch (ImageHorizontalAlignment)
                {
                    case HorizontalAlignment.Left:
                        _foregroundBrush.HorizontalAlignmentRatio = 0f;
                        break;
                    case HorizontalAlignment.Center:
                        _foregroundBrush.HorizontalAlignmentRatio = 0.5f;
                        break;
                    case HorizontalAlignment.Right:
                        _foregroundBrush.HorizontalAlignmentRatio = 1f;
                        break;
                    case HorizontalAlignment.Stretch:
                        _foregroundBrush.HorizontalAlignmentRatio = 0f;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

                switch (ImageVerticalAlignment)
                {
                    case VerticalAlignment.Top:
                        _foregroundBrush.VerticalAlignmentRatio = 0f;
                        break;
                    case VerticalAlignment.Center:
                        _foregroundBrush.VerticalAlignmentRatio = 0.5f;
                        break;
                    case VerticalAlignment.Bottom:
                        _foregroundBrush.VerticalAlignmentRatio = 1f;
                        break;
                    case VerticalAlignment.Stretch:
                        _foregroundBrush.HorizontalAlignmentRatio = 0f;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

                ForegroundVisual.Brush = _foregroundBrush;

                // creating a margin around the image
                ForegroundVisual.Offset = new Vector3((float) ImageMargin.Left, (float) ImageMargin.Top, 0);

                // ensure the image size accounts for the margin
                ForegroundVisual.Size = new Vector2(
                    (float) ActualWidth - (float) ImageMargin.Left - (float) ImageMargin.Right,
                    (float) ActualHeight - (float) ImageMargin.Top - (float) ImageMargin.Bottom);

                if (_foregroundVisualShadow == null)
                {
                    _foregroundVisualShadow = _compositor.CreateDropShadow();
                    _foregroundVisualShadow.Color = Color.FromArgb(255, 75, 75, 80);
                    _foregroundVisualShadow.BlurRadius = 15.0f;
                    _foregroundVisualShadow.Offset = new Vector3(2.5f, 2.5f, 0.0f);
                    ForegroundVisual.Shadow = _foregroundVisualShadow;
                }
                _foregroundVisualShadow.Mask = _foregroundBrush;

                EnsureOpacityAnimation();
                ContainerVisual.StartAnimation(nameof(Visual.Opacity), _opacityAnimation);
            }
            catch
            {
                // do nowt 
            }
        }

        private CompositionEffectBrush BuildBlurBrush()
        {
            var blurEffect = new GaussianBlurEffect
            {
                Name = "Blur",
                BlurAmount = 50.0f,
                BorderMode = EffectBorderMode.Hard,
                Optimization = EffectOptimization.Balanced,
                Source = new CompositionEffectSourceParameter("source")
            };

            var blendEffect = new BlendEffect
            {
                Background = blurEffect,
                Foreground = new ColorSourceEffect {Name = "Color", Color = Color.FromArgb(64, 255, 255, 255)},
                Mode = BlendEffectMode.SoftLight
            };

            var saturationEffect = new SaturationEffect
            {
                Source = blendEffect,
                Saturation = 1.75f
            };

            var factory = _compositor.CreateEffectFactory(saturationEffect);
            return factory.CreateBrush();
        }

        private void EnsureCompositor()
        {
            if (_compositor == null)
            {
                _compositor = ElementCompositionPreview.GetElementVisual(this).Compositor;
            }
        }

        private static void EnsureOpacityAnimation()
        {
            _opacityAnimation = _compositor.CreateScalarKeyFrameAnimation();
            _opacityAnimation.InsertKeyFrame(0f, 0f);
            _opacityAnimation.InsertKeyFrame(1f, 1f);
            _opacityAnimation.Duration = TimeSpan.FromMilliseconds(400);
        }

        private static void EnsureSurfaceFactory()
        {
            if (_surfaceFactoryInstance == null)
            {
                _surfaceFactoryInstance = SurfaceFactory.CreateFromCompositor(_compositor);
            }
        }

        private static void ImageUriChanged(DependencyObject dependencyObject,
            DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            var instance = (VisualImage) dependencyObject;
            var unused = instance.LoadImageAsync(dependencyPropertyChangedEventArgs.NewValue as Uri);
        }

        private void RefreshView()
        {
            BackgroundVisual.Opacity = ShowBlurredBackground ? 1.0f : 0;
        }

        private static void ShowBlurredBackgroundChanged(DependencyObject dependencyObject,
            DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            ((VisualImage) dependencyObject).RefreshView();
        }

        private void VisualImage_Loaded(object sender, RoutedEventArgs e)
        {
        }


        private void VisualImage_Unloaded(object sender, RoutedEventArgs e)
        {
            try
            {
                ElementCompositionPreview.SetElementChildVisual(this, null);
                _backgroundBrush?.Dispose();
                _foregroundBrush?.Dispose();
                BackgroundVisual?.Dispose();
                ForegroundVisual?.Dispose();
                ContainerVisual?.Dispose();
                _uriSurface?.Dispose();
                _opacityAnimation?.Dispose();
            }
            catch
            {
                // ignored
            }
        }
    }
}