using Android.Content;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using XamarinCustomHelper.IO.Serialization;

namespace XamarinCustomHelper.IO
{
    public class AndroidXmlSerializer : ISerializer
    {
        Context _context;
        public AndroidXmlSerializer(Context context)
        {
            _context = context;
        }

        public AndroidXmlSerializer()
        {

        }
        public void SaveB<T>(string fileName, T objectToSave)
        {
            try
            {
                var xs = new System.Xml.Serialization.XmlSerializer(typeof(T));
                using (StreamWriter fs = new StreamWriter(fileName))
                {
                    xs.Serialize(fs, objectToSave);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void Save<T>(string fileName, T objectToSave)
        {
            try
            {
                var xs = new System.Xml.Serialization.XmlSerializer(typeof(T));
                using (Stream fs = _context.OpenFileOutput(fileName, FileCreationMode.Private))
                {
                    xs.Serialize(fs, objectToSave);
                }
            }
            catch (Exception)
            {
                throw new Exception("Une erreur s'est produite lors de l'enregistrement");
            }
        }
        public T Load<T>(string fileName) where T : class
        {
            var xs = new System.Xml.Serialization.XmlSerializer(typeof(T));
            T element = null;
          
            try
            {
                using (Stream fs = _context.OpenFileInput(fileName))
                {
                    element = xs.Deserialize(fs) as T;
                }
            }
            catch (Exception ex)
            {

            }

            return element;
        }
    }
}
