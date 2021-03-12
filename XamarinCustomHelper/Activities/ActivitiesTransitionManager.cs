using Android.App;
using Android.Content;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace XamarinCustomHelper.Activities
{
    public static class ActivitiesTransitionManager
    {
        public static void GoToPreviousActivity(Activity context)
        {
            context.Finish();
        }

        public static void SwitchToActivity(
            Activity context,
            Type nextActivityType,
            bool finishActivity,
            Dictionary<string, string> bundleValues,
            int nextActivityStartingAnimation,
            int currentActivityEndingAnimation)
        {
            var options = ActivityOptions.MakeCustomAnimation(context, nextActivityStartingAnimation, currentActivityEndingAnimation);
            StartActivity(context, nextActivityType, options, finishActivity, bundleValues);
        }
        private static void StartActivity(
            Activity context,
            Type nextActivityType,
            ActivityOptions options,
            bool finishActivity,
            Dictionary<string, string> bundleValues)
        {
            Intent intent = new Intent(context, nextActivityType);

            if (bundleValues != null)
            {
                foreach (var val in bundleValues)
                    intent.PutExtra(val.Key, val.Value);
            }

            context.StartActivity(intent, options.ToBundle());

            if (finishActivity)
                Task.Delay(1000).ContinueWith((t) => context.Finish());
        }
    }
}
