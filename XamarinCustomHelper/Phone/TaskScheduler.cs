using Android.App;
using Android.Content;
using Java.Util;
using System;
using System.Collections.Generic;
using System.Text;

namespace XamarinCustomHelper.Phone
{
    public class AndroidTaskScheduler
    {
        AlarmManager alarmManager;
        public AndroidTaskScheduler(Context context)
        {
            alarmManager = (AlarmManager)context.GetSystemService(Context.AlarmService);
        }

        public void ScheduleTask(DateTime executingTime, PendingIntent pendingIntent)
        {
            TimeSpan timeSpan = executingTime - DateTime.Now;
            ScheduleTask(timeSpan, pendingIntent);
        }

        public void ScheduleTask(TimeSpan timeSpan, PendingIntent pendingIntent)
        {
            var triggerAtMillis = Java.Lang.JavaSystem.CurrentTimeMillis() + (long)timeSpan.TotalMilliseconds;

            alarmManager.Cancel(pendingIntent);
            alarmManager.SetExactAndAllowWhileIdle(AlarmType.RtcWakeup, triggerAtMillis, pendingIntent);
        }

        public void CancelTask(PendingIntent pendingIntent)
        {
            alarmManager.Cancel(pendingIntent);
        }
    }
}
