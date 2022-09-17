using Android.App;
using Android.Webkit;
using Java.Interop;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Xamarin.Essentials;
using XamarinCustomHelper.Activities;
using XamarinCustomHelper.Phone.Location;

namespace XamarinCustomHelper.Javascript
{
    /// <summary>
    /// A Javascript interface to use native features of the device
    /// </summary>
    public class JavascriptPhoneInterface : JavascriptWebViewInterface
    {
        private CancellationTokenSource cancellationTokenSource;
        public JavascriptPhoneInterface(WebViewActivity context) : base(context) 
        {

        }

        public override string InterfaceName()
        {
            return "Phone";
        }

        [Export]
        [JavascriptInterface]
        public string GetGpsLocation()
        {
            var location = new LocationProvider(Context as ILocationPermissionRequest);

            var phoneLocation = location.GetLocation(2);

            string result = string.Empty;

            if (phoneLocation.Location != null)
                result = JsonHelper.ToJson(new { Lat = phoneLocation.Location.Latitude, Lng = phoneLocation.Location.Longitude });
            else
                ShowToastMessage(phoneLocation.ExceptionMessage);

            return result;
        }

        [Export]
        [JavascriptInterface]
        public void GetGpsLocationAsync(string callback)
        {
            if (cancellationTokenSource != null)
                cancellationTokenSource.Cancel();

            cancellationTokenSource = new CancellationTokenSource();

            AsyncCall.ExecuteAsync(this.Context, () =>
            {
                var location = new LocationProvider(Context as ILocationPermissionRequest);

                var phoneLocation = location.GetLocation(2);

                string result = string.Empty;

                if (phoneLocation.Location != null)
                    return new
                    {
                        Location = phoneLocation.Location
                    };
                else
                    return new
                    {
                        exception = phoneLocation.ExceptionMessage
                    };

            }, callback, cancellationTokenSource);          
        }

        [Export]
        [JavascriptInterface]
        public void OpenGoogleMaps(float lat, float lon)
        {
            var coordinates = lat.ToString().Replace(',', '.') + "," + lon.ToString().Replace(',', '.');

            Launcher.OpenAsync("http://maps.google.com/?daddr=" + coordinates);
        }

        /// <summary>
        /// Check if the phone is connected to network
        /// </summary>
        /// <returns>0 if no connectivity, 1 if phone is connected</returns>
        [Export]
        [JavascriptInterface]
        public int CheckConnectivity()
        {
            var current = Connectivity.NetworkAccess;

            return current == NetworkAccess.Internet ? 1 : 0;
        }

        [Export]
        [JavascriptInterface]
        public void ShowToastMessage(string text)
        {
            var toast = Android.Widget.Toast.MakeText(Context, text, Android.Widget.ToastLength.Long);

            toast.Show();
        }

        [Export]
        [JavascriptInterface]
        public void OpenAppSettings()
        {
            AppInfo.ShowSettingsUI();
        }

        [Export]
        [JavascriptInterface]
        public void GoToPreviousActivity()
        {
            ActivitiesTransitionManager.GoToPreviousActivity(this.Context);
        }
    }
}
