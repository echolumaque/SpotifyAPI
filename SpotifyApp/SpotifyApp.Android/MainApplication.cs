using System;
using Android.App;
using Android.Runtime;
using Plugin.CurrentActivity;

namespace SpotifyApp.Droid
{
    [Application(
        Theme = "@style/MainTheme"
        )]
    public class MainApplication : Application
    {
        public MainApplication(IntPtr javaReference, JniHandleOwnership transfer)
            : base(javaReference, transfer)
        {
        }

        public override void OnCreate()
        {
            base.OnCreate();
            CrossCurrentActivity.Current.Init(this);
            Xamarin.Essentials.Platform.Init(this);
        }
    }
}
