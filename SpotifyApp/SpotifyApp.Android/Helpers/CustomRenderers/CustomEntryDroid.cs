using Android.Content;
using Android.Graphics;
using Android.OS;
using SpotifyApp.Droid.Helpers.CustomRenderers;
using SpotifyApp.Helpers.CustomRenderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Color = Xamarin.Forms.Color;

[assembly: ExportRenderer(typeof(CustomEntry), typeof(CustomEntryDroid))]
namespace SpotifyApp.Droid.Helpers.CustomRenderers
{
    public class CustomEntryDroid : EntryRenderer
    {
        public CustomEntryDroid(Context context) : base(context)
        {

        }

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (Control == null || e.NewElement == null) return;

            if (Build.VERSION.SdkInt >= BuildVersionCodes.Q)
            {
                Control.Background.SetColorFilter(new BlendModeColorFilter(Color.FromHex("727272").ToAndroid(), BlendMode.SrcAtop));
            }
            else
            {
                Control.Background.SetColorFilter(Color.FromHex("727272").ToAndroid(), PorterDuff.Mode.SrcAtop);
            }
        }
    }
}
