using Android.App;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;

namespace XamarinCustomHelper.Phone.Location
{
    /// <summary>
    /// A class to provide device location
    /// </summary>
    public class LocationProvider
    {
        private ILocationPermissionRequest _activity;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="activity">an Android Activity that implements ILocationPermissionRequest</param>
        public LocationProvider(ILocationPermissionRequest activity)
        {
            _activity = activity;
            _activity.RequestLocationPermission();

            while (!_activity.LocationPermissionReady)
            {
                System.Threading.Thread.Sleep(1000);
            }
        }
        /// <summary>
        /// Retrieve device location
        /// </summary>
        /// <param name="attempts">number of attempts to get device location</param>
        /// <returns></returns>
        public LocationResult GetLocation(int attempts)
        {
            var request = new GeolocationRequest(GeolocationAccuracy.Best, TimeSpan.FromSeconds(5));

            Xamarin.Essentials.Location location;

            try
            {
                location = Geolocation.GetLocationAsync(request).Result;
            }
            catch (Exception ex)
            {
                return new LocationResult(ex);
            }

            attempts--;

            if (location == null)
            {
                if (attempts > 0)
                {
                    System.Threading.Thread.Sleep(2000);
                    return GetLocation(attempts);
                }
                else
                    return new LocationResult(new Exception("Les tentatives de récupération de votre position ont échoué."));
            }

            return new LocationResult(location);
        }
    }

    public class LocationResult
    {
        public Xamarin.Essentials.Location Location { get; }

        public string ExceptionMessage { get; }

        public LocationResult(Xamarin.Essentials.Location location)
        {
            this.Location = location;
        }

        public LocationResult(Exception exception)
        {
            if (exception.InnerException is FeatureNotEnabledException)
                ExceptionMessage = "La récupération de votre position a échoué. Vérifiez que la géolocalisation de votre appareil est activée";
            else if (exception.InnerException is PermissionException)
                ExceptionMessage = "La récupération de votre position a échoué. La permission d'accès à la position de votre appareil a été refusée.";
            else
                ExceptionMessage = "La récupération de votre position a échoué.";
        }
    }
}
