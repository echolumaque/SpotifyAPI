using Android.App;
using Android.Content.PM;
using Android.Content.Res;
using Android.Graphics;
using Android.OS;
using Android.Util;
using Android.Views;
using Plugin.CurrentActivity;
using Prism;
using Prism.Ioc;
using SpotifyApp.Droid.Helpers.Dependencies;
using SpotifyApp.Helpers.Dependencies;

namespace SpotifyApp.Droid
{
    [Activity(Theme = "@style/MainTheme",
              ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);

            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            Android.Glide.Forms.Init(this);
            global::Rg.Plugins.Popup.Popup.Init(this);

            if (Build.VERSION.SdkInt <= BuildVersionCodes.Q)
            {
                Window.SetStatusBarColor(Color.Transparent);
                Window.Attributes.LayoutInDisplayCutoutMode = LayoutInDisplayCutoutMode.ShortEdges;
                Window.AddFlags(WindowManagerFlags.LayoutInOverscan);
                Window.AddFlags(WindowManagerFlags.ForceNotFullscreen);
            }
            else if (Build.VERSION.SdkInt == BuildVersionCodes.R || Build.VERSION.SdkInt != BuildVersionCodes.R) //sakop yung navbar pero walang batt, wifi icons
            {
                Window.SetStatusBarColor(Color.Transparent);
                Window.Attributes.LayoutInDisplayCutoutMode = LayoutInDisplayCutoutMode.ShortEdges;
                Window.AddFlags(WindowManagerFlags.DrawsSystemBarBackgrounds);
                Window.AddFlags(WindowManagerFlags.LayoutInOverscan);
                Window.AddFlags(WindowManagerFlags.ForceNotFullscreen);
                Window.SetDecorFitsSystemWindows(false);
            }
            else
            {
                Window.SetStatusBarColor(Color.Transparent);
                Window.Attributes.LayoutInDisplayCutoutMode = LayoutInDisplayCutoutMode.Never;
                Window.AddFlags(WindowManagerFlags.DrawsSystemBarBackgrounds);
                Window.SetFlags(WindowManagerFlags.LayoutNoLimits, WindowManagerFlags.LayoutNoLimits);
                Window.AddFlags(WindowManagerFlags.LayoutInScreen);
                Window.ClearFlags(WindowManagerFlags.Fullscreen);
            }

            InitFontScale();
            LoadApplication(new App(new AndroidInitializer()));
        }

        public override Resources Resources
        {
            get
            {
                Configuration config = new Configuration();
                config.SetToDefaults();

                return CreateConfigurationContext(config).Resources;
            }
        }

        private void InitFontScale()
        {
            Configuration configuration = Resources.Configuration;
            configuration.FontScale = (float)0.85;
            DisplayMetrics metrics = new DisplayMetrics();
            WindowManager.DefaultDisplay.GetMetrics(metrics);

            metrics.ScaledDensity = configuration.FontScale * metrics.Density;

            BaseContext.ApplicationContext.CreateConfigurationContext(configuration);
            BaseContext.Resources.DisplayMetrics.SetTo(metrics);
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        public override void OnBackPressed() => Rg.Plugins.Popup.Popup.SendBackPressed(base.OnBackPressed);
    }

    public class AndroidInitializer : IPlatformInitializer
    {
        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.Register<IToast, ToastDroid>();
        }
    }
}

