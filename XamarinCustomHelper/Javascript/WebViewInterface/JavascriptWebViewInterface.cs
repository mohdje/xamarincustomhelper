using Android.App;
using System;
using System.Collections.Generic;
using System.Text;
using XamarinCustomHelper.Activities;

namespace XamarinCustomHelper.Javascript
{
    /// <summary>
    /// A base class to build an interface to call C# code from Web View's Javascript code
    /// </summary>
    public abstract class JavascriptWebViewInterface : Java.Lang.Object
    {
        protected WebViewActivity Context { get; }

        public JavascriptWebViewInterface(WebViewActivity context)
        {
            Context = context;
        }
        /// <summary>
        /// The interface name to use in Web View's Javascript code
        /// </summary>
        /// <returns></returns>
        public abstract string InterfaceName();

        protected string ResultJSON(object obj)
        {
            return JsonHelper.ToJson(new { Result = obj });
        }
    }
}
