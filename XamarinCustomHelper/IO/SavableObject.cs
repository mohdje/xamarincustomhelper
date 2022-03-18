using Android.Content;
using System;
using System.Collections.Generic;
using System.Text;
using XamarinCustomHelper.IO.Serialization;

namespace XamarinCustomHelper.IO
{
    /// <summary>
    /// A base class to build an encapsulated serializer of an object.
    /// Inherit from it to build a class that will save an object in the device
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class SavableObject<T>: SerializerWrapper where T : class, new()
    {
        private static T _object;

        public T Object => _object;

        public SavableObject(ISerializer serializer) : base(serializer)
        {
            if (_object == null)
                _object = _serializer.Load<T>(GetSerializationFileName());
        }
        /// <summary>
        /// Set the object and save it
        /// </summary>
        /// <param name="item"></param>
        public void SetObject(T obj)
        {
            _object = obj;
            _serializer.Save(GetSerializationFileName(), _object);
        }
    }
}
