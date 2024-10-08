using System;

namespace TinyObjects
{
    sealed class TinyArray
    {
        private object[] data { get; set; }
        private int _count { get; set; }
        private int _capx { get; set; }
        private object[] backup { get; set; }
        public int Count
        {
            get
            {
                int i = 0;
                foreach (var item in data)
                {
                    if (item != null)
                    { i++; }
                }
                return i;
            }
        }
        public object[] Back
        {
            get
            {
                if (backup != null)
                {
                    return backup;
                }
                else
                {
                    return null;
                }
            }
        }
        public int Lenght
        {
            get
            {
                return data.Length;
            }
        }
        public object this[int index]
        {
            get
            {
                return Value(index);
            }
            set
            {
                Add(value,index);
            }
        }

        public TinyArray()
        {
            _count = 0;
            _capx = 2;
            data = new object[_capx];
            data[_count] = null;
            data[_count + 1] = null;
        }
        public TinyArray(params object[] input)
        {
            _count = 0;
            _capx = 2;
            data = new object[_capx];
            data[_count] = null;
            data[_count + 1] = null;
            foreach(var i in input)
            {
                Add(i);
            }
        }

        private void _inc(int i)
        {
            _capx = i+2;
            object[] temp = new object[_capx];
            int k = 0;
            foreach(var l in data)
            {
                temp[k] = l;
                k++;
            }
            data = temp;
        }
        private void _dec(int i)
        {
            _capx = i+2;
            object[] temp = new object[_capx];
            for(int j = 0; j<_capx;j++)
            {
                temp[j] = data[j];
            }
            data = temp;
        }
        private void _shift(int j)
        {
            _capx += 1;
            object[] temp = new object[_capx];
            int cherry = 0;
            int k = 0;
            for(var i = 0; i< data.Length; i++)
            {
                if (i == j && cherry <= 0)
                {
                    temp[k] = null;
                    i--;
                    cherry++;
                }
                else
                {
                    temp[k + cherry] = data[i]; ;
                    k++;
                }
            }
            data = temp;
        }

        /*Aggiungi gli elementi*/
        public void Add(object ele)
        {
            if (_count + 1 > _capx)
            {
                _inc(_count+1);
                data[_count] = ele;
                _count++;
                return;
            }
            data[_count] = ele;
            _count++;
            return;
        }
        public void Add(object ele, int pos)
        {
            if (pos > _capx)
            {
                _inc(pos+1);
                data[pos] = ele;
                _count = pos;
                return;
            }
            data[pos] = ele;
            return;
        }
        public void Add(params object[] elems)
        {
            foreach(var t in elems)
            {
                Add(t);
            }
        }
        public void AddTo(object[] elems)
        {
            foreach (var t in elems)
            {
                Add(t);
            }
        }

        public object Value(int i)
        {
            try
            {
                if (data[i] == null)
                {
                    return null;
                }
                else
                {
                    return data[i];
                }
            }
            catch
            {
                return null;
            }
        }

        /*Rimuovi gli elementi*/
        public void Remove()
        {
            try
            {
                for (int i = data.Length - 1; i >= 0; i--)
                {
                    if (data[i] != null)
                    {
                        data[i] = null;
                        _count = i;
                        _dec(i);
                        return;
                    }
                }
            }
            catch
            {
                return;
            }
        }
        public void Remove(int i)
        {
            try
            {
                if (data[i] != null)
                {
                    data[i] = null;
                    _count = i;
                    return;
                }
                else
                {
                    new Exception("This Element does not exist");
                    return;
                }
            }
            catch
            { 
                return; 
            }
        }

        /*
         * 1) Restituisce l'index di un elemento
         * 2) Restituisce se un elemento esiste
         * 3) Restituisce quante istanze di un elemnto ci sono
        */
        public int IndexOf(object ele)
        {
            for (int i = 0; i < data.Length; i++)
            {
                if (ele.Equals(data[i]))
                {
                    return i;
                }
            }
            return -1;
        }
        public bool Contains(object ele)
        {
            for (int i = 0; i < data.Length; i++)
            {
                if (ele.Equals(data[i]))
                {
                    return true;
                }
            }
            return false;
        }
        public int Find(object ele)
        {
            int find = -1;
            for (int i = 0; i < data.Length; i++)
            {
                if (ele.Equals(data[i]))
                {
                    find++;
                }
            }
            return find < 0 - 1 ? -1 : find + 1;
        }

        public void Sort()
        {
            try
            {
                int n = data.Length;
                if (!IsSorted()) backup = data;
                for (int i = 0; i < n - 1; i++)
                {
                    int mi = i;
                    for (int j = i + 1; j < n; j++)
                    {
                        IComparable mi1 = data[j] as IComparable;
                        IComparable mi2 = data[mi] as IComparable;
                        if (mi1 != null && mi2 != null && mi2.CompareTo(mi1) > 0)
                        {
                            mi = j;
                        }
                    }
                    (data[i], data[mi]) = (data[mi], data[i]);
                }
            }
            catch
            {
                throw new Exception("Cannot Sort");
            }
        }
        public void ReverseSort()
        {
            try
            {
                int n = data.Length;
                if (!IsSorted()) backup = data;
                for (int i = 0; i < n - 1; i++)
                {
                    int mi = i;
                    for (int j = i + 1; j < n; j++)
                    {
                        IComparable mi1 = data[j] as IComparable;
                        IComparable mi2 = data[mi] as IComparable;
                        if (mi1 != null && mi2 != null && mi1.CompareTo(mi2) > 0)
                        {
                            mi = j;
                        }
                    }
                    (data[i], data[mi]) = (data[mi], data[i]);
                }
            }
            catch
            {
                throw new Exception("Cannot Sort");
            }
        }
        public void ConditionalSort(Comparison<object> rule)
        {
            try
            {
                int n = data.Length;
                if (!IsSorted()) backup = data;
                for (int i = 0; i < n - 1; i++)
                {
                    int mi = i;
                    for (int j = i + 1; j < n; j++)
                    {
                        if (rule(data[j], data[mi]) < 0)
                        {
                            mi = j;
                        }
                    }
                    (data[i], data[mi]) = (data[mi], data[i]);
                }
            }
            catch
            {
                throw new Exception("Cannot Sort");
            }
        }

        public bool IsSorted()
        {
            if (data.Length == 0) return true; 
            bool isAscending = true;
            bool isDescending = true;

            for (int i = 0; i < data.Length - 1; i++)
            {
                if (data[i] is IComparable current && data[i + 1] is IComparable next)
                {
                    try
                    {
                        if (current.CompareTo(next) > 0)
                        {
                            isAscending = false;
                        }
                        if (current.CompareTo(next) < 0)
                        {
                            isDescending = false;
                        }
                    }
                    catch
                    {
                        return false;
                    }
                }
            }

            return isAscending || isDescending;
        }

        public void Swap(int index,int index2)
        {
            try
            {
                (data[index], data[index2]) = (data[index2], data[index]);
            }
            catch
            {
                throw new Exception("Index not found");
            }
        }

        public void Reverse()
        {
            object[] array = new object[_capx];
            int j = 0; ;
            for (int i = data.Length;i >= 0;i--)
            {
                array[i] = data[i];
                j++;
            }
            backup = data;
            data = array;
            return;
        }

        public void GoBack()
        {
            try
            {
                if (backup != null)
                {
                    data = backup;
                    backup = null;
                }
                else
                {
                    throw new Exception("No Backup");
                }
            }
            catch
            {
                throw new Exception("No Backup");
            }
        }

        public void Clear()
        {
            _dec(0);
            object[] temp = new object[_capx];
            data = temp;
            _count = 0;
        }

        public void Insert(object ele, int pos)
        {
            _shift(pos);
            Add(ele,pos);
        }

        /*For che scorre tutti gli elementi non nulli*/
        public void For(Action<object,int> action)
        {
            foreach(var item in data)
            {
                if(item != null)
                {
                    int index = IndexOf(item);
                    action(item, index);
                }
            }
        }
        /*For che scorre tutti gli elementi, anche quelli nulli*/
        public void ForEach(Action<object,int> action)
        {
            foreach(var item in data)
            {
                int index = IndexOf(item);
                action(item, index);
            }
        }

        public override string ToString()
        {
            string k = "";
            foreach(var u in data)
            {
                if(u != null)
                {
                    k += u.ToString() + "\n";
                }
            }
            return k;
        }
        public object[] ToArray()
        {
            object[] temp = new object[Count];
            int i = 0;
            For((value,index) => {
                temp[i] = value;
                i++;
            });

            return temp;
        }
        public string ToStringAll()
        {
            string k = "";
            foreach (var u in data)
            {
                if (u != null)
                {
                    k += u.ToString() + " | " + u.GetType() + " | " + IndexOf(u) + "\n";
                }
            }
            return k;
        }
    }
}