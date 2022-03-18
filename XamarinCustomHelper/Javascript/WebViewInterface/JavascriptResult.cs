using Android.Webkit;
using System;
using System.Collections.Generic;
using System.Text;

namespace XamarinCustomHelper.Javascript
{
    public class JavascriptResult : Java.Lang.Object, IValueCallback
    {
        public event EventHandler<JavascriptResultEventArgs> ResultReceived;

        public void OnReceiveValue(Java.Lang.Object result)
        {
            Java.Lang.String json = (Java.Lang.String)result;

            NotifyResultReceived(json.ToString());
        }

        protected void NotifyResultReceived(string result)
        {
            if (this.ResultReceived != null)
                this.ResultReceived(this, new JavascriptResultEventArgs() { Result = result });
        }
    }

    public class JavascriptResultEventArgs
    {
        public string Result { get; set; }
    }
}
