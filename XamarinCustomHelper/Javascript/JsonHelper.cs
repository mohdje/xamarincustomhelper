using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace XamarinCustomHelper.Javascript
{
    /// <summary>
    /// A static class to serialize and deserialize JSON objects
    /// </summary>
    public static class JsonHelper
    {
        /// <summary>
        /// Convert an object to a JSON string 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToJson(object value)
        {
            DefaultContractResolver contractResolver = new DefaultContractResolver
            {
                NamingStrategy = new CamelCaseNamingStrategy()
            };

            return JsonConvert.SerializeObject(value, new JsonSerializerSettings { ContractResolver = contractResolver });
        }
        /// <summary>
        /// Convert a JSON string to an object
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="json"></param>
        /// <returns></returns>
        public static T ToObject<T>(string json) where T : class
        {
            return JsonConvert.DeserializeObject(json, typeof(T)) as T;
        }

        //public static T Deserialize<T>(Stream fileStream) where T : class
        //{
        //    JsonSerializer serializer = new JsonSerializer();

        //    T result = default(T);
        //    try
        //    {
        //        using (TextReader sr = new StreamReader(fileStream))
        //        {
        //            result = serializer.Deserialize(sr, typeof(T)) as T;
        //        }
        //    }
        //    catch (System.Exception) { }

        //    return result;
        //}
    }
}
