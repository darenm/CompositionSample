using System;
using System.Numerics;
using System.Threading.Tasks;
using Windows.UI.Composition;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Hosting;
using Robmikh.CompositionSurfaceFactory;
using Microsoft.Graphics.Canvas.Effects;
using Windows.UI;

namespace Pixels.Controls
{
    public class VisualImage : UserControl
    {
        public static readonly DependencyProperty ImageUriProperty = DependencyProperty.Register(
            "ImageUri", typeof(Uri), typeof(VisualImage), new PropertyMetadata(default(Uri), ImageUriChanged));

        private SurfaceFactory _surfaceFactoryInstance;

        private Compositor _compositor;
        private CompositionEffectBrush _blurBrush;

        private UriSurface _uriSurface;
        private Visual _rootVisual;
        private Uri _lastImageUri;
        private CompositionSurfaceBrush _backgroundBrush;
        private CompositionSurfaceBrush _foregroundBrush;

        public VisualImage()
        {
            _compositor = ElementCompositionPreview.GetElementVisual(this).Compositor;
            _surfaceFactoryInstance = SurfaceFactory.CreateFromCompositor(_compositor);

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
                    BackgroundVisual.Size = new Vector2((float)args.NewSize.Width, (float)args.NewSize.Height);
                    ForegroundVisual.Size = new Vector2((float)args.NewSize.Width - 48, (float)args.NewSize.Height - 48);
                };

            _blurBrush = BuildBlurBrush();

            Loaded += VisualImage_Loaded;

            Unloaded += VisualImage_Unloaded;
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
            }
            catch
            {
                // ignored
            }
        }


        public Uri ImageUri
        {
            get { return (Uri)GetValue(ImageUriProperty); }
            set { SetValue(ImageUriProperty, value); }
        }

        public SpriteVisual BackgroundVisual { get; private set; }
        public SpriteVisual ForegroundVisual { get; private set; }
        public ContainerVisual ContainerVisual { get; private set; }

        private static void ImageUriChanged(DependencyObject dependencyObject,
            DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            var instance = (VisualImage)dependencyObject;
            var unused = instance.LoadImageAsync(dependencyPropertyChangedEventArgs.NewValue as Uri);
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
                BackgroundVisual.Size = new Vector2((float)ActualWidth, (float)ActualHeight);

                _foregroundBrush = _compositor.CreateSurfaceBrush(_uriSurface.Surface);
                _foregroundBrush.Stretch = CompositionStretch.Uniform;
                _foregroundBrush.HorizontalAlignmentRatio = 0.5f;
                _foregroundBrush.VerticalAlignmentRatio = 0.5f;

                ForegroundVisual.Brush = _foregroundBrush;

                // creating a margin around the image
                // TODO: Make dep prop for Margin 
                ForegroundVisual.Offset = new Vector3(24f, 24f, 0);
                ForegroundVisual.Size = new Vector2((float)ActualWidth-48, (float)ActualHeight-48);

                var dropShadow = _compositor.CreateDropShadow();
                dropShadow.Color = Color.FromArgb(255, 75, 75, 80);
                dropShadow.BlurRadius = 15.0f;
                dropShadow.Offset = new Vector3(2.5f, 2.5f, 0.0f);
                dropShadow.Mask = _foregroundBrush;
                ForegroundVisual.Shadow = dropShadow;

                var opacityAnimation = _compositor.CreateScalarKeyFrameAnimation();
                opacityAnimation.InsertKeyFrame(0f, 0f);
                opacityAnimation.InsertKeyFrame(1f, 1f);
                opacityAnimation.Duration = TimeSpan.FromMilliseconds(400);
                ContainerVisual.StartAnimation(nameof(Visual.Opacity), opacityAnimation);
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
                Foreground = new ColorSourceEffect { Name = "Color", Color = Color.FromArgb(64, 255, 255, 255) },
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
    }
}