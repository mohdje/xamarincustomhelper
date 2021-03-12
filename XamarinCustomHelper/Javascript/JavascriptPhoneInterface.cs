using Android.App;
using Android.Webkit;
using Java.Interop;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;
using XamarinCustomHelper.Activities;
using XamarinCustomHelper.Phone.Location;

namespace XamarinCustomHelper.Javascript
{
    public class JavascriptPhoneInterface : JavascriptWebViewInterface
    {
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
                ShowToastMessage(phoneLocation.ExecptionMessage);

            return result;
        }

        [Export]
        [JavascriptInterface]
        public void OpenGoogleMaps(float lat, float lon)
        {
            var coordinates = lat.ToString().Replace(',', '.') + "," + lon.ToString().Replace(',', '.');

            Launcher.OpenAsync("http://maps.google.com/?daddr=" + coordinates);
        }

        [Export]
        [JavascriptInterface]
        public string CheckConnectivity()
        {
            var current = Connectivity.NetworkAccess;

            return current == NetworkAccess.Internet ? "ok" : string.Empty;
        }

        [Export]
        [JavascriptInterface]
        public void ShowToastMessage(string text)
        {
            var toast = Android.Widget.Toast.MakeText(Context, text, Android.Widget.ToastLength.Long);

            toast.Show();
        }
    }
}
