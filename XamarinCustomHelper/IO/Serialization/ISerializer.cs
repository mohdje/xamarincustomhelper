using System;
using System.Collections.Generic;
using System.Text;

namespace XamarinCustomHelper.IO.Serialization
{
    public interface ISerializer 
    {
        void Save<T>(string fileName, T objectToSave);

        T Load<T>(string fileName) where T : class;

        T LoadStaticResource<T>(string fileName) where T : class;
    }
}
