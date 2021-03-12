using Android.App;
using System;
using System.Collections.Generic;
using System.Text;

namespace XamarinCustomHelper.Phone.Location
{
    /// <summary>
    /// Interface that should be implemented only by an Activity class 
    /// </summary>
    public interface ILocationPermissionRequest
    {
        bool LocationPermissionReady { get; set; }
        void RequestLocationPermission();
    }
}
