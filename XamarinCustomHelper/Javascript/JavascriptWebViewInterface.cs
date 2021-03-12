using Android.App;
using System;
using System.Collections.Generic;
using System.Text;
using XamarinCustomHelper.Activities;

namespace XamarinCustomHelper.Javascript
{
    public abstract class JavascriptWebViewInterface : Java.Lang.Object
    {
        protected WebViewActivity Context { get; }

        public JavascriptWebViewInterface(WebViewActivity context)
        {
            Context = context;
        }

        public abstract string InterfaceName();
    }
}
