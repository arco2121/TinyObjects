using System;

namespace TinyObjects
{
    public class TinyList<Tiny>
    {
        private Tiny[] data { get; set; }
        private int _count { get; set; }
        private int _capx { get; set; }
        public int Count
        {
            get
            {
                int i = 0;
                foreach (var item in data)
                {
                    if (item.Equals(default(Tiny)))
                    { i++; }
                }
                return i;
            }
        }
        public int Lenght
        {
            get
            {
                return data.Length;
            }
        }
        public Tiny this[int index]
        {
            get
            {
                return Value(index);
            }
            set
            {
                Add(value, index);
            }
        }

        public TinyList()
        {
            _count = 0;
            _capx = 0;
            data = new Tiny[_capx];
        }
        public TinyList(params Tiny[] input)
        {
            _count = 0;
            _capx = 0;
            data = new Tiny[_capx];
            foreach (var i in input)
            {
                Add(i);
            }
        }

        private void _inc(int i)
        {
            _capx = i;
            Tiny[] temp = new Tiny[_capx];
            int k = 0;
            foreach (var l in data)
            {
                temp[k] = l;
                k++;
            }
            data = temp;
        }
        private void _dec(int i)
        {
            _capx = i;
            Tiny[] temp = new Tiny[_capx];
            for (int j = 0; j < _capx; j++)
            {
                temp[j] = data[j];
            }
            data = temp;
        }

        private void _shift(int j)
        {
            _capx += 1;
            Tiny[] temp = new Tiny[_capx];
            int cherry = 0;
            int k = 0;
            for (var i = 0; i < data.Length; i++)
            {
                if (i == j && cherry <= 0)
                {
                    temp[k] = default(Tiny);
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
        public void Add(Tiny ele)
        {
            if (_count + 1 > _capx)
            {
                _inc(_count + 1);
                data[_count] = ele;
                _count++;
                return;
            }
            data[_count] = ele;
            _count++;
            return;
        }
        public void Add(Tiny ele, int pos)
        {
            if (pos > _capx)
            {
                _inc(pos + 1);
                data[pos] = ele;
                _count = pos;
                return;
            }
            data[pos] = ele;
            _count = pos;
            return;
        }
        public void AddTo(Tiny[] elems)
        {
            foreach (var t in elems)
            {
                Add(t);
            }
        }

        public Tiny Value(int i)
        {
            try
            {
                if (data[i].Equals(default(Tiny)))
                    {
                    return default;
                }
                else
                {
                    return data[i];
                }
            }
            catch
            {
                return default;
            }
        }

        /*Rimuovi gli elementi*/
        public void Remove()
        {
            try
            {
                for (int i = data.Length - 1; i >= 0; i--)
                {
                    if (data.Equals(default(Tiny)))
                    {
                        data[i] = default;
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
                if (data[i].Equals(default(Tiny)))
                {
                    data[i] = default;
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
        public int IndexOf(Tiny ele)
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
        public bool Contains(Tiny ele)
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
        public int Find(Tiny ele)
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
                for (int i = 0; i < n - 1; i++)
                {
                    int mi = i;
                    for (int j = i + 1; j < n; j++)
                    {
                        IComparable mi1 = data[j] as IComparable;
                        IComparable mi2 = data[mi] as IComparable;
                        if (!(mi1.Equals(default(Tiny))) && !(mi2.Equals(default(Tiny))) && mi2.CompareTo(mi1) > 0)
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
                for (int i = 0; i < n - 1; i++)
                {
                    int mi = i;
                    for (int j = i + 1; j < n; j++)
                    {
                        IComparable mi1 = data[j] as IComparable;
                        IComparable mi2 = data[mi] as IComparable;
                        if (!(mi1.Equals(default(Tiny))) && !(mi2.Equals(default(Tiny))) && mi1.CompareTo(mi2) > 0)
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

        public void Swap(int index, int index2)
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

        public void Insert(Tiny ele, int pos)
        {
            _shift(pos);
            Add(ele, pos);
        }

        public void Clear()
        {
            _dec(0);
            Tiny[] temp = new Tiny[_capx];
            data = temp;
            _count = 0;
        }

        /*For che scorre tutti gli elementi*/
        public void For(Action<Tiny, int> action)
        {
            foreach (var item in data)
            {
                int index = IndexOf(item);
                action(item, index);
            }
        }

        public override string ToString()
        {
            string k = "";
            foreach (var u in data)
            {
                if (!u.Equals(default(Tiny)))
                {
                    k += u.ToString() + "\n";
                }
            }
            return k;
        }
        public Tiny[] ToArray()
        {
            Tiny[] temp = new Tiny[Count];
            int i = 0;
            For((value, index) => {
                if (!value.Equals(default(Tiny)))
                {
                    temp[i] = value;
                    i++;
                }
            });

            return temp;
        }
    }
}