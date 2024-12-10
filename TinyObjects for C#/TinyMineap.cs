using System;

namespace TinyObjects
{
    public class TinyMineap<Tiny> where Tiny : IComparable<Tiny>
    {
        private Tiny[] _data;
        private int _cap;
        private int _count;
        public Tiny this[int index]
        {
            get
            {
                return _data[index];
            }
        }
        public int Count
        {
            get
            {
                return _count - 1; 
            }
        }

        public TinyMineap(int capacity)
        {
            _cap = capacity;
            _count = 1; 
            _data = new Tiny[capacity + 1];
        }

        public TinyMineap() : this(4) { }

        private void _inc()
        {
            _cap *= 2;
            var st = new Tiny[_cap + 1];
            for (int i = 1; i < _count; i++)
            {
                st[i] = _data[i];
            }
            _data = st;
        }

        private void _checkcap()
        {
            if (_count > _cap)
            {
                _inc();
            }
        }

        public void Add(Tiny value)
        {
            _checkcap();
            _data[_count] = value;
            _upHeap(_count);
            _count++;
        }

        public Tiny RemoveMin()
        {
            if (_count == 1) throw new InvalidOperationException("Heap Empty.");

            Tiny min = _data[1];
            _data[1] = _data[_count - 1];
            _count--; 
            _downHeap(1); 
            return min;
        }

        public Tiny PeekMin()
        {
            if (_count == 1) throw new InvalidOperationException("Heap Empty.");
            return _data[1];
        }

        private void _upHeap(int i)
        {
            while (i > 1)
            {
                int parent = i / 2;
                if (_data[i].CompareTo(_data[parent]) >= 0) 
                    break; 
                _swap(i, parent);
                i = parent;
            }
        }

        private void _downHeap(int i)
        {
            while (2 * i < _count)
            {
                int left = 2 * i;
                int right = left + 1;
                int smallest = left;

                if (right < _count && _data[right].CompareTo(_data[left]) < 0)
                {
                    smallest = right;
                }

                if (_data[i].CompareTo(_data[smallest]) <= 0) break;
                _swap(i, smallest);
                i = smallest;
            }
        }

        private void _swap(int i, int j)
        {
            Tiny temp = _data[i];
            _data[i] = _data[j];
            _data[j] = temp;
        }

        public override string ToString()
        {
            var result = "";
            for (int i = 1; i < _count; i++)
            {
                result += _data[i] + (i < _count - 1 ? ", " : "");
            }
            return result;
        }
    }
}
