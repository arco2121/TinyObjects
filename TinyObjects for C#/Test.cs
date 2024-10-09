using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyObjects;

class Program
{
    static void Main()
    {
        TinyChain<int> tiny = new TinyChain<int>(1,2,3,4,5,4,4,60);
        tiny.AddAfter(tiny.Find(60),2);
        tiny.AddLast(120);
        tiny.RemoveFirst();
        Console.WriteLine(tiny.ToString());
        Console.ReadLine();
    }
}
