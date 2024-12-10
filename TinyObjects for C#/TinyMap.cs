using System;

namespace TinyObjects
{
    public class TinyMap
    {
        private sealed class TinyUnity
        {
            public object _data { get; set; }
            public object _key { get; set; }

            public TinyUnity _next { get; set; }

            public TinyUnity(object key, object data)
            {
                _key = key;
                _data = data;
                _next = null;
            }
        }
        public int Count
        {
            get
            {
                return _data.Count;
            }
        }
        private TinyList<TinyUnity> _data { get; set; }
        public object this[object key]
        {
            get
            {
                object temp = null;
                _data.For((ogg, i) =>
                {
                    if (ogg._key == key)
                    {
                        temp = ogg._data;
                        return;
                    }
                });
                return temp;
            }
            set
            {
                bool check = false;
                _data.For((ogg, i) =>
                {
                    if (ogg._key.Equals(key))
                    {
                        _data[i]._data = value;
                        check = true;
                        return;
                    }
                });
                if(!check)
                {
                    _data.Add(new TinyUnity(key,value));
                }
            }
        }

        public TinyMap(params (object key, object value)[] data) 
        {
            _data = new TinyList<TinyUnity>();
            foreach(var item in data)
            {
                _data.Add(new TinyUnity(item.key, item.value));
            }
        } 
        public TinyMap()
        {
            _data = new TinyList<TinyUnity>();
        }

        public bool Contains(object key)
        {
            bool found = false;
            _data.For((value, i) =>
            {
                if (value._key == key)
                {
                    found = true;
                    return;
                }
            });
            return found;
        }

        public void Add(object key, object value)
        {
            bool check = false;
            _data.For((ogg, i) =>
            {
                if (ogg._key.Equals(key))
                {
                    _data[i]._data = value;
                    check = true;
                    return;
                }
            });
            if (!check)
            {
                _data.Add(new TinyUnity(key, value));
            }
        }

        public object Value(object key)
        {
            return this[key];
        }

        public void Remove(object key)
        {
            _data.For((ogg, i) =>
            {
                if (ogg._key.Equals(key))
                {
                    _data.Remove(i);
                    return;
                }
            });
            return;
        }

        public void Clear()
        {
            _data.Clear();
        }
    }
}
