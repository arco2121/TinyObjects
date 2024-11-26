using System;
using static System.Net.Mime.MediaTypeNames;

namespace TinyObjects
{
    public class TinyStack
    {
        private class TinyNode
        {
            public object ele;
            public TinyNode next;

            public TinyNode()
            {
                ele = null;
                next = null;
            }
            public TinyNode(object element)
            {
                ele = element;
                next = null;
            }
        }
        private TinyNode _first { set; get; }
        private TinyNode _last { set; get; }
        private int _count;
        public object this[int i]
        {
            get
            {
                var cu = _first;
                for (int j = 0; j < _count + 1; j++)
                {
                    if (j == (_count - 1 - i))
                    {
                        return cu.ele;
                    }
                    cu = cu.next;
                }
                return null;
            }
            set
            {
                var cu = _first;
                for (int j = 0; j < _count + 1; j++)
                {
                    if (j == (_count - 1 - i))
                    {
                        cu.ele = value;
                        return;
                    }
                    cu = cu.next;
                }
                throw new Exception("Not found");
            }
        }

        public TinyStack()
        {
             _first = null;
            _last = null;
            _count = 0;
        }

        public void Push(object ele)
        {

        }
    }
}
