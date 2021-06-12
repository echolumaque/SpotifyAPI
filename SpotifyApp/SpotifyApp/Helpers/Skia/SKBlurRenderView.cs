using SkiaSharp;
using SkiaSharp.Views.Forms;
using Xamarin.Forms;

namespace SpotifyApp.Helpers.Skia
{
    public class SKBlurRenderView : SKGLView
    {

        public static readonly BindableProperty SourceProperty = BindableProperty.Create(
            "Source",
            typeof(string),
            typeof(SKBlurRenderView),
            null);

        public string Source
        {
            get => (string)GetValue(SourceProperty); 
            set => SetValue(SourceProperty, value); 
        }

        public static readonly BindableProperty SigmaXProperty = BindableProperty.Create(
            "SigmaX",
            typeof(float),
            typeof(SKBlurRenderView),
            null);

        public float SigmaX
        {
            get => (float)GetValue(SigmaXProperty);
            set => SetValue(SigmaXProperty, value);
        }

        public static readonly BindableProperty SigmaYProperty = BindableProperty.Create(
            "SigmaY",
            typeof(float),
            typeof(SKBlurRenderView),
            null);

        public float SigmaY
        {
            get => (float)GetValue(SigmaYProperty);
            set => SetValue(SigmaYProperty, value);
        }

        protected override void OnPaintSurface(SKPaintGLSurfaceEventArgs e)
        {
            base.OnPaintSurface(e);
            try
            {                
                using (var paint = new SKPaint())
                {
                    var bitmap = SKBitmap.Decode(App.Client().DownloadData(Source));

                    paint.ImageFilter = SKImageFilter.CreateBlur(SigmaX, SigmaY);
                    paint.IsAntialias = true;
                    paint.FilterQuality = SKFilterQuality.High;
                    e.Surface.Canvas.DrawBitmap(bitmap, new SKRect(0, 0, e.BackendRenderTarget.Width, e.BackendRenderTarget.Height), paint);
                }
                
                InvalidateSurface();
            }
            catch (System.Exception)
            {
                
            }
        }
    }
}
