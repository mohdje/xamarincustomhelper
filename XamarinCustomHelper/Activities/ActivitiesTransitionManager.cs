using Android.App;
using Android.Content;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace XamarinCustomHelper.Activities
{
    /// <summary>
    /// A static class to manage transition between activities
    /// </summary>
    public static class ActivitiesTransitionManager
    {
        /// <summary>
        /// Finish the current activity and re-start previous activity
        /// </summary>
        /// <param name="context">the active activity</param>
        public static void GoToPreviousActivity(Activity context)
        {
            context.Finish();
        }
        /// <summary>
        /// Start a new activity with a transition
        /// </summary>
        /// <param name="context">the current activity</param>
        /// <param name="nextActivityType">the activity type to start</param>
        /// <param name="finishActivity">true if the current activity should be finished, false if it should be paused</param>
        /// <param name="bundleValues">values to pass to the activity to start</param>
        /// <param name="nextActivityStartingAnimation">animation id for the activity to start</param>
        /// <param name="currentActivityEndingAnimation">animation id for the activity to stop</param>
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

            //if (finishActivity)
            //    Task.Delay(1000).ContinueWith((t) => context.Finish());
            if (finishActivity)
                context.FinishAfterTransition();
        }
    }
}
