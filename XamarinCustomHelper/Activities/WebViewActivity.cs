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

        protected abstract string GetUrl();

        protected abstract Javascript.JavascriptWebViewInterface[] GetJavascriptInterfaces();

        protected abstract int GetLayoutResource();

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
