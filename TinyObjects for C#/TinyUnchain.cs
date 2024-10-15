using System;

namespace TinyObjects
{
    public class TinyUnchain
    {
        public sealed class TinyNode
        {
            public object Element { get; set; }
            public TinyNode Next { get; set; }

            public TinyNode(object element)
            {
                Element = element;
                Next = null;
            }
        }
        private TinyNode _first { get; set; }
        private TinyNode _last { get; set; }
        public TinyNode First
        {
            get
            {
                return _first;
            }
        }
        private TinyNode Last
        {
            get
            {
                return _last;
            }
        }
        private int _count { get; set; }
        public int Count
        {
            get
            {
                return _count;
            }
        }
        public object this[int index]
        {
            get
            {
                var current = _first;
                for (var i = 0; i < _count; i++)
                {
                    if (i == index)
                    {
                        return current.Element;
                    }
                    current = current.Next;
                }
                return null;
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

        public TinyUnchain()
        {
            _first = null;
            _last = null;
            _count = 0;
        }

        public TinyUnchain(params object[] elems)
        {
            for (int i = 0; i < elems.Length; i++)
            {
                if (i == 0)
                {
                    TinyNode temp = new TinyNode(elems[i]);
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

        //Aggiungi
        public void AddAfter(TinyNode where, object ele)
        {
            try
            {
                if (where.Equals(_last))
                {
                    AddLast(ele);
                    return;
                }
                var node = new TinyNode(ele);
                TinyNode temp = where.Next;
                where.Next = node;
                node.Next = temp;
                _count++;
            }
            catch
            {
                throw new Exception("Not found");
            }
        }

        public void AddBefore(TinyNode where, object ele)
        {
            try
            {
                TinyNode node = new TinyNode(ele);
                if (where.Equals(_first))
                {
                    node.Next = _first;
                    _first = node;
                    _count++;
                    return;
                }
                TinyNode current = _first;
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

        public void AddFirst(object ele)
        {
            if (_first == null)
            {
                var temp1 = new TinyNode(ele);
                _first = temp1;
                _last = temp1;
                _count++;
                return;
            }
            var node = new TinyNode(ele);
            TinyNode temp = _first;
            _first = node;
            node.Next = temp;
            _count++;
        }
        public void AddLast(object ele)
        {
            if (_last == null)
            {
                var temp = new TinyNode(ele);
                _first = temp;
                _last = temp;
                _count++;
                return;
            }
            var node = new TinyNode(ele);
            _last.Next = node;
            node.Next = null;
            _last = node;
            _count++;
        }

        public TinyNode Find(object Element)
        {
            TinyNode current = _first;
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

        public int IndexOf(object Element)
        {
            TinyNode current = _first;
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

        public bool Contains(object Element)
        {
            TinyNode current = _first;
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

        public TinyNode FindLast(object Element)
        {
            TinyNode cherry = null;
            TinyNode current = _first;
            for (int i = 0; i < _count; i++)
            {
                if (current.Element.Equals(Element))
                {
                    cherry = current;
                }
                current = current.Next;
            }

            return cherry;
        }

        public void Remove(TinyNode node)
        {
            TinyNode current = _first;
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
        public void Remove(object nodes)
        {
            TinyNode node = Find(nodes);
            TinyNode current = _first;
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
            TinyNode current = _first;
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

        public override string ToString()
        {
            string temp = "";
            TinyNode current = _first;
            for (int i = 0; i < _count; i++)
            {
                temp += current.Element.ToString() + "\n";
                if (current.Next != null)
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
