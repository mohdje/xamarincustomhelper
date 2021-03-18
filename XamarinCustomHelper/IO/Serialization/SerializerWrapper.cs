using System;
using System.Collections.Generic;
using System.Text;

namespace XamarinCustomHelper.IO.Serialization
{
    /// <summary>
    /// A base class to build a serializer wrapper
    /// </summary>
    public abstract class SerializerWrapper
    {
        protected ISerializer _serializer;
        /// <summary>
        /// The file used to serialize
        /// </summary>
        /// <returns></returns>
        protected abstract string GetSerializationFileName();

        public SerializerWrapper(ISerializer serializer)
        {
            _serializer = serializer;
        }
    }
}
