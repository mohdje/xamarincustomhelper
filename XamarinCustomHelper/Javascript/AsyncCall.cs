﻿using System;
using System.Collections.Generic;
using System.Text;
using XamarinCustomHelper.Activities;

namespace XamarinCustomHelper.Javascript
{
    /// <summary>
    /// A static class to manage async calls made from Javascript in the Web View
    /// </summary>
    public static class AsyncCall
    {
        /// <summary>
        /// Execute asynchronously a function and trigger a custom Javascript event in the UI Web View to notify the end of the execution
        /// </summary>
        /// <param name="context">Activity containing the Web View</param>
        /// <param name="function">The function to execute. This function retrieves an obect.</param>
        /// <param name="callback">The js callback function that will be called from the UI Web View</param>
        public static void ExecuteAsync(WebViewActivity context, Func<object> function, string callback)
        {
            System.Threading.Tasks.Task.Run(() =>
            {
                var result = function();

                var asyncCallResult = new
                {
                    Result = result
                };

                var script = $"var callback = {callback}";
                script += $";callback.call(window,{JsonHelper.ToJson(asyncCallResult)})";

                context.ExecuteJavaScript(script);
            });
        }
    }
}
