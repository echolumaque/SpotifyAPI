using Android.Widget;
using SpotifyApp.Helpers.Dependencies;

namespace SpotifyApp.Droid.Helpers.Dependencies
{
    public class ToastDroid : IToast
    {
        public void ShowToast(string message)
        {
            var currentContext = Android.App.Application.Context;

            Toast.MakeText(currentContext, message, ToastLength.Long).Show();
        }
    }
}