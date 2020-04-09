using System;
using System.Collections;
using System.IO;
namespace Advance_Csharp
{
    class Program
    {
        class NodeList
        {
            public NodeList(int val)
            {
                value = val;
                next = prev = null;
            }
            public int value { get; set; }
            public NodeList next { get; set; }
            public NodeList prev { get; set; }
        }
        class Listme : IEnumerable
        {
            public Listme()
            {
                first = last = null;
                size = 0;
            }
            public bool empty()
            {
                return first == null && last == null ? true : false;
            }
            public void Add(int val)
            {
                var node = new NodeList(val);
                if (empty())
                {
                    first = last = node;
                    ++size;
                }
                else
                {
                    last.next = node;
                    node.prev = last;
                    last = node;
                    size++;
                }

            }
            public IEnumerator GetEnumerator()
            {
                var current = first;
                while (current != null)
                {
                    yield return current.value;
                    current = current.next;
                }
            }
            private int size;
            private NodeList first;
            private NodeList last;
        }

        static void Main(string[] args)
        {
            Listme listme=new Listme();
            for(int i = 0; i < 5; i++)
            {
                listme.Add(i);
            }
            foreach(var item in listme)
            {
                Console.WriteLine(item);
            }
        }
    }
}
