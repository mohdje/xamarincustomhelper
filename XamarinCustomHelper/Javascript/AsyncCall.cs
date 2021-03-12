using System;
using System.Collections.Generic;
using System.Text;
using XamarinCustomHelper.Activities;

namespace XamarinCustomHelper.Javascript
{
    public static class AsyncCall
    {
        public static void ExecuteAsync(WebViewActivity context, Func<string> function, string callbackEventId)
        {
            System.Threading.Tasks.Task.Run(() =>
            {
                var result = function();

                var asyncCallResult = new
                {
                    CallId = callbackEventId,
                    Result = result
                };

                var script = "document.dispatchEvent(new CustomEvent('asyncCallfinished',{ 'detail': " + JsonHelper.ToJson(asyncCallResult) + " }));";
                context.ExecuteJavaScript(script);
            });
        }
    }
}
