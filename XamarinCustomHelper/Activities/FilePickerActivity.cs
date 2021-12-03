using Android;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Provider;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XamarinCustomHelper.IO;

namespace XamarinCustomHelper.Activities
{
    [Activity]
    internal class FilePickerActivity : Activity
    {
        private const int FilePickerRequestCode = 579985;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            OpenFilePicker();
        }


        private void OpenFilePicker()
        {
            Intent intent = new Intent(Intent.ActionOpenDocument);
            intent.SetType("*/*");
            intent.AddCategory(Intent.CategoryOpenable);
            StartActivityForResult(intent, FilePickerRequestCode);
        }

        protected override void OnActivityResult(int requestCode, [GeneratedEnum] Result resultCode, Intent data)
        {
            string fileUri = null;
            string fileName = null;
            if (requestCode == FilePickerRequestCode)
            {
                if (data?.Data != null)
                {
                    Android.Net.Uri uri = data.Data;
                    this.ContentResolver.TakePersistableUriPermission(uri, ActivityFlags.GrantReadUriPermission);
                    fileUri = uri.ToString();
                    fileName = GetFileName(uri);
                }
            }

            FilePicker.NotifyFileChosen(fileName, fileUri);

            Finish();
        }

        private string GetFileName(Android.Net.Uri uri)
        {
            var cursor = ContentResolver.Query(uri, new string[] { OpenableColumns.DisplayName }, null, null, null);
            if (cursor != null && cursor.MoveToFirst())
            {
                int index = cursor.GetColumnIndexOrThrow(OpenableColumns.DisplayName);
                return cursor.GetString(index);
            }

            return null;
        }

    }


}
