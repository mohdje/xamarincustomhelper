using Android.Content;
using System;
using System.Collections.Generic;
using System.Text;
using XamarinCustomHelper.IO.Serialization;

namespace XamarinCustomHelper.IO
{
    public abstract class SavableObject<T>: SerializerWrapper where T : class, new()
    {
        private static T _object;

        public SavableObject(ISerializer serializer) : base(serializer)
        {
            if (_object == null)
                _object = _serializer.Load<T>(GetSerializationFileName());
        }
        public T GetItem()
        {
            return _object;
        }
        public void SetItem(T item)
        {
            _object = item;
            _serializer.Save(GetSerializationFileName(), _object);
        }
    }
}
