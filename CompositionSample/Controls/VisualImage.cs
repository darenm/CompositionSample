using System;
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
    public class VisualImage : UserControl
    {
        public static readonly DependencyProperty ImageUriProperty = DependencyProperty.Register(
            "ImageUri", typeof(Uri), typeof(VisualImage), new PropertyMetadata(default(Uri), ImageUriChanged));

        public static readonly DependencyProperty ImageMarginProperty =
            DependencyProperty.Register("ImageMargin", typeof(Thickness), typeof(VisualImage),
                new PropertyMetadata(new Thickness()));

        private static SurfaceFactory _surfaceFactoryInstance;
        private static Compositor _compositor;
        private static ScalarKeyFrameAnimation _opacityAnimation;

        private readonly CompositionEffectBrush _blurBrush;
        private CompositionSurfaceBrush _backgroundBrush;
        private CompositionSurfaceBrush _foregroundBrush;
        private Uri _lastImageUri;
        private Visual _rootVisual;

        private UriSurface _uriSurface;

        public VisualImage()
        {
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

                _foregroundBrush = _compositor.CreateSurfaceBrush(_uriSurface.Surface);
                _foregroundBrush.Stretch = CompositionStretch.Uniform;
                _foregroundBrush.HorizontalAlignmentRatio = 0.5f;
                _foregroundBrush.VerticalAlignmentRatio = 0.5f;

                ForegroundVisual.Brush = _foregroundBrush;

                // creating a margin around the image
                ForegroundVisual.Offset = new Vector3((float) ImageMargin.Left, (float) ImageMargin.Top, 0);
                ForegroundVisual.Size = new Vector2(
                    (float) ActualWidth - (float) ImageMargin.Left - (float) ImageMargin.Right,
                    (float) ActualHeight - (float) ImageMargin.Top - (float) ImageMargin.Bottom);

                var dropShadow = _compositor.CreateDropShadow();
                dropShadow.Color = Color.FromArgb(255, 75, 75, 80);
                dropShadow.BlurRadius = 15.0f;
                dropShadow.Offset = new Vector3(2.5f, 2.5f, 0.0f);
                dropShadow.Mask = _foregroundBrush;
                ForegroundVisual.Shadow = dropShadow;

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