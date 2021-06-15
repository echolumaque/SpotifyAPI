using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using SkiaSharp;
using SkiaSharp.Views.Forms;
using SpotifyApp.Helpers.Cache;
using SpotifyApp.Models;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace SpotifyApp.Helpers.Skia
{
    public class SKBlurRenderView : SKGLView
    {
        private readonly ImageCache imageCache = new ImageCache();

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

        private SKBitmap bitmap;

        protected override void OnPaintSurface(SKPaintGLSurfaceEventArgs e)
        {
            base.OnPaintSurface(e);

            bitmap = SKBitmap.Decode(GetCachedByteArray());

            using (var paint = new SKPaint())
            {
                paint.ImageFilter = SKImageFilter.CreateBlur(SigmaX, SigmaY);
                paint.IsAntialias = true;
                paint.FilterQuality = SKFilterQuality.High;
                e.Surface.Canvas.DrawBitmap(bitmap, new SKRect(0, 0, e.BackendRenderTarget.Width, e.BackendRenderTarget.Height), paint);
            }
            InvalidateSurface();
        }

        private byte[] GetCachedByteArray()
        {
            //var test = Preferences.Get("source", "");
            //return App.Client().DownloadData(test);
            return imageCache.GetCachedImageByteArray(Preferences.Get("source", ""));
        }//TODO: icache ko gamit key value pair where key is string and value is byte arrays
    }
}
