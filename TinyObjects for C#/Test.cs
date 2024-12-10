using System;

namespace TinyObjects
{
    class Program
    {
        static void Main(string[] args)
        {
            /*TestAddAfter();
            TestAddBefore();
            TestAddFirst();
            TestAddAfterBroken();
            TestAddLast();
            TestRemove();
            TestFind();
            TestIndexOf();
            TestContains();
            TestRemoveFirst();
            TestRemoveLast();
            TestClear();
            TestToString();
            Console.ReadLine();*/
            TinyMineap<int> o = new TinyMineap<int>(4);
            o.Add(12);
            Console.ReadLine();
        }


        /* Adding After */
            static void TestAddAfter()
        {
            Console.WriteLine("TestAddAfter:");

            TinyChain<int> chain = new TinyChain<int>(0, 10, 20);
            chain.AddAfter(10, 15);
            Check(chain[2] == 15, $"Errore: Elemento non aggiunto correttamente");

            Console.WriteLine("TestAddAfter completato.\n");
        }

        /* Adding After Broken */
        static void TestAddAfterBroken()
        {
            Console.WriteLine("TestAddAfterBroken:");

            TinyChain<int> chain = new TinyChain<int>(0, 10, 20);
            chain.AddAfter(10, 15);
            Check(chain[2] == 15, $"Errore: Elemento aggiunto correttamente");

            Console.WriteLine("TestAddAfter completato.\n");
        }

        /* Adding Before */
        static void TestAddBefore()
        {
            Console.WriteLine("TestAddBefore:");

            TinyChain<int> chain = new TinyChain<int>(0, 10, 20);
            chain.AddBefore(10, 15);
            Check(chain[2] != 15, $"Errore: Elemento aggiunto correttamente");

            Console.WriteLine("TestAddBefore fallito correttamente.\n");
        }


        /* Adding First */
        static void TestAddFirst()
        {
            Console.WriteLine("TestAddFirst:");

            TinyChain<int> chain = new TinyChain<int>();
            chain.AddFirst(10);
            Check(chain.First.Element == 10, "Errore: Elemento non aggiunto correttamente all'inizio");

            chain.AddFirst(20);
            Check(chain.First.Element == 20, "Errore: Il nuovo elemento non è in testa");

            Console.WriteLine("TestAddFirst completato.\n");
        }


        /* Adding Last */
        static void TestAddLast()
        {
            Console.WriteLine("TestAddLast:");

            TinyChain<int> chain = new TinyChain<int>();
            chain.AddLast(10);
            Check(chain.Last.Element == 10, "Errore: Elemento non aggiunto correttamente alla fine");

            chain.AddLast(20);
            Check(chain.Last.Element == 20, "Errore: Il nuovo elemento non è in fondo");

            Console.WriteLine("TestAddLast completato.\n");
        }


        /* Removing Element */
        static void TestRemove()
        {
            Console.WriteLine("TestRemove:");

            TinyChain<int> chain = new TinyChain<int>(10, 20, 30);
            TinyChain<int>.TinyNode<int> nodeToRemove = chain.Find(20);

            chain.Remove(nodeToRemove);
            Check(chain.Contains(20) == false, "Errore: Elemento non rimosso correttamente");

            Console.WriteLine("TestRemove completato.\n");
        }


        /* Removing First */
        static void TestRemoveFirst()
        {
            Console.WriteLine("TestRemoveFirst:");

            TinyChain<int> chain = new TinyChain<int>(10, 20, 30);
            chain.RemoveFirst();
            Check(chain.First.Element == 20, "Errore: Il primo elemento non è stato rimosso correttamente");

            Console.WriteLine("TestRemoveFirst completato.\n");
        }


        /* Removing Last */
        static void TestRemoveLast()
        {
            Console.WriteLine("TestRemoveLast:");

            TinyChain<int> chain = new TinyChain<int>(10, 20, 30);
            chain.RemoveLast();
            Check(chain.Last.Element == 20, "Errore: L'ultimo elemento non è stato rimosso correttamente");

            Console.WriteLine("TestRemoveLast completato.\n");
        }


        /* Clearing */
        static void TestClear()
        {
            Console.WriteLine("TestClear:");

            TinyChain<int> chain = new TinyChain<int>(10, 20, 30);
            chain.Clear();
            Check(chain.Count == 0, "Errore: La lista non è stata cancellata correttamente");

            Console.WriteLine("TestClear completato.\n");
        }


        /* Finding Element */
        static void TestFind()
        {
            Console.WriteLine("TestFind:");

            TinyChain<int> chain = new TinyChain<int>(10, 20, 30);
            TinyChain<int>.TinyNode<int> node = chain.Find(20);
            Check(node != null && node.Element == 20, "Errore: Nodo non trovato correttamente");

            Console.WriteLine("TestFind completato.\n");
        }


        /* Get Index Of */
        static void TestIndexOf()
        {
            Console.WriteLine("TestIndexOf:");

            TinyChain<int> chain = new TinyChain<int>(10, 20, 30);
            int index = chain.IndexOf(20);
            Check(index == 1, "Errore: Indice non trovato correttamente");

            index = chain.IndexOf(40);
            Check(index == -1, "Errore: Indice di un elemento non presente dovrebbe essere -1");

            Console.WriteLine("TestIndexOf completato.\n");
        }

         
        /* Conatins */
        static void TestContains()
        {
            Console.WriteLine("TestContains:");

            TinyChain<int> chain = new TinyChain<int>(10, 20, 30);
            Check(chain.Contains(20) == true, "Errore: Elemento dovrebbe essere contenuto nella lista");
            Check(chain.Contains(40) == false, "Errore: Elemento non dovrebbe essere contenuto nella lista");

            Console.WriteLine("TestContains completato.\n");
        }


        /* To Stringing */
        static void TestToString()
        {
            Console.WriteLine("TestToString:");

            TinyChain<int> chain = new TinyChain<int>(10, 20, 30);
            string result = chain.ToString();
            Check(result == "10\n20\n30\n", "Errore: Il risultato di ToString non è corretto");

            Console.WriteLine("TestToString completato.\n");
        }


        /* Last Check */
        static void Check(bool condition, string message)
        {
            if (!condition)
            {
                Console.WriteLine("Fail: " + message);
            }
            else
            {
                Console.WriteLine("Pass");
            }
        }
    }
}
