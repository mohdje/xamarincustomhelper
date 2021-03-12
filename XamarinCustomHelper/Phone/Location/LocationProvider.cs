using Android.App;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;

namespace XamarinCustomHelper.Phone.Location
{
    public class LocationProvider
    {
        private ILocationPermissionRequest _activity;

        public LocationProvider(ILocationPermissionRequest activity)
        {
            _activity = activity;
            _activity.RequestLocationPermission();

            while (!_activity.LocationPermissionReady)
            {
                System.Threading.Thread.Sleep(1000);
            }
        }

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
                if (ex.InnerException is FeatureNotEnabledException)
                    return new LocationResult(null, "La récupération de votre position a échoué. Vérifiez que la géolocalisation de votre appareil est activée");
                else if (ex.InnerException is PermissionException)
                    return new LocationResult(null, "La récupération de votre position a échoué. La permission d'accès à la position de votre appareil a été refusée.");
                else
                    return new LocationResult(null, "Une erreur est survenue lors de la récupération de votre position a échoué.");
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
                    return new LocationResult(null, "Les tentatives de récupération de votre position ont échoué.");
            }

            return new LocationResult(location, string.Empty);
        }
    }

    public class LocationResult
    {
        public Xamarin.Essentials.Location Location { get; }

        public string ExecptionMessage { get; }

        public LocationResult(Xamarin.Essentials.Location location, string exceptionMessage)
        {
            this.Location = location;
            this.ExecptionMessage = exceptionMessage;
        }
    }
}
