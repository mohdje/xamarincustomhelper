using Android.App;
using Android.OS;
using Android.Views;
using Android.Webkit;
using System;
using System.Collections.Generic;
using System.Text;
using XamarinCustomHelper.Javascript;

namespace XamarinCustomHelper.Activities
{
    /// <summary>
    /// A base class to build WebView based activity
    /// </summary>
    public abstract class WebViewActivity : Activity
    {
        private WebView _webView;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            //pour que le clavier virtuel ne compresse pas la fenêtre
            this.Window.SetSoftInputMode(SoftInput.AdjustNothing);
            // Set our view from the "main" layout resource
            SetContentView(GetLayoutResource());

            //set webview 
            _webView = FindViewById<WebView>(GetWebViewId());
            _webView.Settings.JavaScriptEnabled = true;

            _webView.SetWebChromeClient(new WebChromeClient());
            _webView.SetWebViewClient(new WebViewClient());

            foreach (var jsInterface in this.GetJavascriptInterfaces())
            {
                _webView.AddJavascriptInterface(jsInterface, jsInterface.InterfaceName());
            }

            _webView.LoadUrl(this.GetUrl());
        }
        /// <summary>
        /// Url of the web view page
        /// </summary>
        /// <returns></returns>
        protected abstract string GetUrl();
        /// <summary>
        /// Array of Javascript interfaces to bind with the WebView's activity
        /// </summary>
        /// <returns></returns>
        protected abstract Javascript.JavascriptWebViewInterface[] GetJavascriptInterfaces();
        /// <summary>
        /// The xaml resource to bind to the activity
        /// </summary>
        /// <returns></returns>
        protected abstract int GetLayoutResource();
        /// <summary>
        /// The WebView resource id 
        /// </summary>
        /// <returns></returns>
        protected abstract int GetWebViewId();

        public JavascriptResult ExecuteJavaScript(string script)
        {
            JavascriptResult result = new JavascriptResult();

            this.RunOnUiThread(() => {
                _webView.EvaluateJavascript(script, result);
            });

            return result;
        }
    }
}
