using System;

namespace TinyObjects
{
    sealed class TinyChain<Tiny>
    {
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
        private TinyNode<Tiny> _first { get; set; }
        private TinyNode<Tiny> _last { get; set; }
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

        /*Aggiungi*/
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

        public void AddFirst(Tiny ele)
        {
            var node = new TinyNode<Tiny>(ele);
            TinyNode<Tiny> temp = _first.Next;
            _first.Next = node;
            node.Next = temp;
            _count++;
        }
        public void AddLast(Tiny ele)
        {
            var node = new TinyNode<Tiny>(ele);
            _last.Next = node;
            node.Next = null;
            _last = node;
            _count++;
        }

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
            return default;
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