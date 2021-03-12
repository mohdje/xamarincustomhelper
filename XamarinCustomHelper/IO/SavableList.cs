using Android.Content;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Serialization;
using XamarinCustomHelper.IO.Serialization;

namespace XamarinCustomHelper.IO
{
    public abstract class SavableList<T> : SerializerWrapper where T : class, new()
    {
        protected List<T> _list;
        protected abstract bool CheckBeforeAddElement(T element, out string errorMessage);
        protected abstract bool CheckBeforeAddRange(T[] elements, out string errorMessage);
        public SavableList(ISerializer serializer) : base(serializer)
        {
            _list = new List<T>();

            var list = _serializer.Load<T[]>(GetSerializationFileName());

            if (list != null)
                _list.AddRange(list);
        }

        public void Add(T element)
        {
            string errorMessage;

            if (!CheckBeforeAddElement(element, out errorMessage))
                throw new Exception(errorMessage);
            else
                _list.Add(element);

            _serializer.Save(GetSerializationFileName(), _list.ToArray());
        }

        public void AddRange(T[] elements)
        {
            string errorMessage;

            if (!CheckBeforeAddRange(elements, out errorMessage))
                throw new Exception(errorMessage);
            else
                _list.AddRange(elements);

            _serializer.Save(GetSerializationFileName(), _list.ToArray());
        }

        public void Replace(Predicate<T> predicate, T newElement)
        {
            var index = _list.FindIndex(predicate);

            if (index > -1)
                _list[index] = newElement;
            else
                throw new Exception("Modification failed. The element is not in the list.");

            _serializer.Save(GetSerializationFileName(), _list.ToArray());
        }

        public void Remove(Predicate<T> predicate)
        {
            int index = _list.FindIndex(predicate);

            if (index > -1)
                _list.RemoveAt(index);
            else
                throw new Exception("Removing failed. The element is not in the list.");

            _serializer.Save(GetSerializationFileName(), _list.ToArray());
        }

        public T[] GetElements()
        {
            return _list.ToArray();
        }
    }
}
