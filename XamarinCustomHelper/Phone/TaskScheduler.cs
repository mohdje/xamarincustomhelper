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

        public void ScheduleTask(DateTime executingTime, PendingIntent pendingIntent, int executionDelayAllowed = 60000)
        {
            TimeSpan timeSpan = executingTime - DateTime.Now;
            ScheduleTask(timeSpan, pendingIntent, executionDelayAllowed);
        }

        public void ScheduleTask(TimeSpan timeSpan, PendingIntent pendingIntent, int executionDelayAllowed = 60000)
        {
            Calendar calendar = Calendar.Instance;
            calendar.TimeInMillis = Java.Lang.JavaSystem.CurrentTimeMillis();
            calendar.Add(CalendarField.Hour, timeSpan.Hours);
            calendar.Add(CalendarField.Minute, timeSpan.Minutes);
            calendar.Add(CalendarField.Second, timeSpan.Seconds);

            alarmManager.Cancel(pendingIntent);
            alarmManager.SetWindow(AlarmType.Rtc, calendar.TimeInMillis, executionDelayAllowed, pendingIntent);
        }

        public void CancelTask(PendingIntent pendingIntent)
        {
            alarmManager.Cancel(pendingIntent);
        }
    }
}
