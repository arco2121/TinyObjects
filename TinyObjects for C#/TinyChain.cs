using System;

namespace TinyObjects
{
    sealed class TinyChain<Tiny>
    {
        /* Node Class */
        internal sealed class TinyNode<TinyIn>
        {
            public TinyIn Element { get; set; }
            public TinyNode<TinyIn> Next { get; set; }

            public TinyNode(TinyIn element)
            {
                Element = element;
                Next = null;
            }
        }


        /* Propieties */
        private TinyNode<Tiny> _first { get; set; }
        private TinyNode<Tiny> _last { get; set; }
        public TinyNode<Tiny> First
        {
            get
            {
                return _first;
            }
        }
        public TinyNode<Tiny> Last 
        { 
            get
            {
                return _last;
            }
        }
        private int _count { get; set; }
        public int Count { 
            get 
            {
                return _count; 
            } 
        }
        public Tiny this[int index]
        {
            get
            {
                var current = _first;
                for(var i = 0; i < _count; i++)
                {
                    if(i == index)
                    {
                        return current.Element;
                    }
                    current = current.Next;
                }
                return default(Tiny);
            }
            set
            {
                var current = _first;
                for (var i = 0; i < _count; i++)
                {
                    if (i == index)
                    {
                        current.Element = value;
                        return;
                    }
                    current = current.Next;
                }
                return;
            }
        }


        /* Constructors */
        public TinyChain()
        {
            _first = null;
            _last = null;
            _count = 0;
        }
        public TinyChain(params Tiny[] elems)
        {
            for(int i = 0; i < elems.Length; i++)
            {
                if(i == 0)
                {
                    TinyNode<Tiny> temp = new TinyNode<Tiny>(elems[i]);
                    _first = temp;
                    _last = temp;
                    _count++;
                }
                else
                {
                    AddLast(elems[i]);
                }
            }
        }


        /* Adding */
        public void AddAfter(TinyNode<Tiny> where, Tiny ele)
        {
            try
            {
                if (where.Equals(_last))
                {
                    AddLast(ele);
                    return;
                }
                var node = new TinyNode<Tiny>(ele);
                TinyNode<Tiny> temp = where.Next;
                where.Next = node;
                node.Next = temp;
                _count++;
            }
            catch
            {
                throw new Exception("Not found");
            }
        }
        public void AddAfter(Tiny where, Tiny ele)
        {
            try
            {
                if (where.Equals(_last))
                {
                    AddLast(ele);
                    return;
                }
                var node = new TinyNode<Tiny>(ele);
                TinyNode<Tiny> whereNode = Find(where);
                TinyNode<Tiny> temp = whereNode.Next;
                whereNode.Next = node;
                node.Next = temp;
                _count++;
            }
            catch
            {
                throw new Exception("Not found");
            }
        }
        public void AddBefore(TinyNode<Tiny> where, Tiny ele)
        {
            try
            {
                TinyNode<Tiny> node = new TinyNode<Tiny>(ele);
                if (where.Equals(_first))
                {
                    node.Next = _first;
                    _first = node;
                    _count++;
                    return;
                }
                TinyNode<Tiny> current = _first;
                while (current != null && current.Next != where)
                {
                    current = current.Next;
                }
                node.Next = where;
                current.Next = node;
                _count++;
            }
            catch
            {
                throw new Exception("Not found");
            }
        }
        public void AddBefore(Tiny where, Tiny ele)
        {
            try
            {
                TinyNode<Tiny> node = new TinyNode<Tiny>(ele);
                TinyNode<Tiny> whereNode = Find(where);
                if (whereNode.Equals(_first))
                {
                    node.Next = _first;
                    _first = node;
                    _count++;
                    return;
                }
                TinyNode<Tiny> current = _first;
                while (current != null && current.Next != whereNode)
                {
                    current = current.Next;
                }
                node.Next = whereNode;
                current.Next = node;
                _count++;
            }
            catch
            {
                throw new Exception("Not found");
            }
        }
        public void AddFirst(Tiny ele)
        {
            if(_first == null)
            {
                var temp1 = new TinyNode<Tiny>(ele);
                _first = temp1;
                _last = temp1;
                _count++;
                return;
            }
            var node = new TinyNode<Tiny>(ele);
            TinyNode<Tiny> temp = _first;
            _first = node;
            node.Next = temp;
            _count++;
        }
        public void AddLast(Tiny ele)
        {
            if(_last == null)
            {
                var temp = new TinyNode<Tiny>(ele);
                _first = temp;
                _last = temp;
                _count++;
                return;
            }
            var node = new TinyNode<Tiny>(ele);
            _last.Next = node;
            node.Next = null;
            _last = node;
            _count++;
        }


        /* Remove */
        public void Remove(TinyNode<Tiny> node)
        {
            TinyNode<Tiny> current = _first;
            for (int i = 0; i < _count; i++)
            {
                if (current.Next == node)
                {
                    current.Next = node.Next;
                    node = null;
                    _count--;
                    return;
                }
                current = current.Next;
            }
            throw new Exception("Not found");
        }
        public void Remove(Tiny nodes)
        {
            TinyNode<Tiny> node = Find(nodes);
            TinyNode<Tiny> current = _first;
            for (int i = 0; i < _count; i++)
            {
                if (current.Next == node)
                {
                    current.Next = node.Next;
                    node = null;
                    _count--;
                    return;
                }
                current = current.Next;
            }
            throw new Exception("Not found");
        }
        public void RemoveFirst()
        {
            if (_first == null)
            {
                return;
            }
            _first = _first.Next;
            _count--;
        }
        public void RemoveLast()
        {
            TinyNode<Tiny> current = _first;
            for (int i = 0; i < _count; i++)
            {
                if (current.Next == _last)
                {
                    _last = current;
                    _count--;
                    return;
                }
                current = current.Next;
            }
        }
        public void Clear()
        {
            _first = null;
            _last = null;
            _count = 0;
        }


        /* Access */
        public TinyNode<Tiny> Find(Tiny Element)
        {
            TinyNode<Tiny> current = _first;
            for (int i = 0; i < _count; i++)
            {
                if (current.Element.Equals(Element))
                {
                    return current;
                }
                current = current.Next;
            }
            return default;
        }
        public int IndexOf(Tiny Element)
        {
            TinyNode<Tiny> current = _first;
            for (int i = 0; i < _count; i++)
            {
                if (current.Element.Equals(Element))
                {
                    return i;
                }
                current = current.Next;
            }
            return -1;
        }
        public bool Contains(Tiny Element)
        {
            TinyNode<Tiny> current = _first;
            for (int i = 0; i < _count; i++)
            {
                if (current.Element.Equals(Element))
                {
                    return true;
                }
                current = current.Next;
            }
            return false;
        }
        public TinyNode<Tiny> FindLast(Tiny Element)
        {
            TinyNode<Tiny> temp = _first;
            TinyNode<Tiny> current = _first;
            for (int i = 0; i < _count; i++)
            {
                if (current.Element.Equals(Element))
                {
                     temp = current;
                }
                current = current.Next;
            }

            return temp;
        }
        public override string ToString()
        {
            string temp = "";
            TinyNode<Tiny> current = _first;
            for (int i = 0; i < _count; i++)
            {
                temp += current.Element.ToString() + "\n";
                if(current.Next != null)
                {
                    current = current.Next;
                }
                else
                {
                    break;
                }
            }
            return temp;
        }
    }
}