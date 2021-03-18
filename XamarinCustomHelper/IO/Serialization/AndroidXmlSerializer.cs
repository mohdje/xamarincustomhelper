using Android.Content;
using System;
using System.Collections.Generic;
using System.Linq;

using System.IO;
using System.Text;
using XamarinCustomHelper.IO.Serialization;

namespace XamarinCustomHelper.IO
{
    /// <summary>
    /// An XmlSerializer to use in an Android context
    /// </summary>
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
      
        /// <summary>
        /// Serialize an object an save it in a Xml file on the Android device
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fileName">the Xml file the object will be serialized in</param>
        /// <param name="objectToSave">the object to save</param>
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
                throw new Exception("An error occured during serialization");
            }
        }
        /// <summary>
        /// Deserialize an object from a Xml file
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fileName">the Xml file containing the object to deserialize</param>
        /// <returns></returns>
        public T Load<T>(string fileName) where T : class
        {
            var xs = new System.Xml.Serialization.XmlSerializer(typeof(T));
            T element = null;

            var files = _context.FileList();

            if (!files.Contains(fileName))
                return null;

            try
            {
                using (Stream fs = _context.OpenFileInput(fileName))
                {
                    element = xs.Deserialize(fs) as T;
                }
            }
            catch(Exception ex)
            {
                throw new Exception("An error occured during deserialization");
            }

            return element;
        }
    }
}
