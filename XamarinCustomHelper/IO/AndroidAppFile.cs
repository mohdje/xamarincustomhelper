using Android.Content;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace XamarinCustomHelper.IO
{
    public class AndroidAppFile
    {
        private string fileName;
        private Context context;
        public AndroidAppFile(Context context, string fileName)
        {
            this.fileName = fileName;
            this.context = context;
        }

        public byte[] Read()
        {            
            using (Stream fs = context.OpenFileInput(fileName))
            {
                using(MemoryStream ms = new MemoryStream())
                {
                    fs.CopyTo(ms);
                    return ms.ToArray();
                }
            }
        }

        public void Write(byte[] data)
        {
            using (Stream fs = context.OpenFileOutput(fileName, FileCreationMode.Private))
            {
                fs.Write(data, 0, data.Length);
            }
        }
    }
}
