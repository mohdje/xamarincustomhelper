using Android.App;
using Android.Content;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using XamarinCustomHelper.Activities;

namespace XamarinCustomHelper.IO
{
    public class FilePicker
    {
        private static FilePicker instance;

        private Activity Context;

        public event EventHandler<FilePickerEventArgs> OnFileChosen;
        private FilePicker(Activity context)
        {
            Context = context;
        }

        public static FilePicker GetInstance(Activity context)
        {           
            instance = new FilePicker(context);

            return instance;
        }
        public void Open()
        { 
           Context.StartActivity(new Intent(Application.Context, typeof(FilePickerActivity)));
        }

        internal static void NotifyFileChosen(string fileName, string fileUri)
        {
            if (instance.OnFileChosen != null)
                instance.OnFileChosen.Invoke(instance, new FilePickerEventArgs() { FileName = fileName, FileUri = fileUri });
        }
    }

    public class FilePickerEventArgs
    {
        public string FileName { get; set; }

        public string FileUri { get; set; }
    }
}
