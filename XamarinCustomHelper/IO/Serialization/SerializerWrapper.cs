using System;
using System.Collections.Generic;
using System.Text;

namespace XamarinCustomHelper.IO.Serialization
{
    public abstract class SerializerWrapper
    {
        protected ISerializer _serializer;
        protected abstract string GetSerializationFileName();

        public SerializerWrapper(ISerializer serializer)
        {
            _serializer = serializer;
        }
    }
}
