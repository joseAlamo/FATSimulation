using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FATSimulation
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Node> Memory = new List<Node>(){
                new Node(false),new Node(),new Node(false),new Node(),new Node()
            };
            List<int> FAT = new List<int>()
            {
                new int(),new int(),new int(),new int(),new int()
            };

            Console.WriteLine("Insert the size of the Data you want to allocate in memory: ");
            int Size;
            int.TryParse(Console.ReadLine(), out Size);//returns 0 if parsing fails
            int UnitSize = 100;
            int BlocksNeeded = Size / UnitSize;
            BlocksNeeded = (Size % UnitSize > 0) ? BlocksNeeded + 1 : BlocksNeeded;

            int CurrentBlock = 0;
            for (int i = 0; i < Memory.Count; i++)
            {
                if (Memory[i].Available && Size > 0)
                {
                    Memory[i].Available = false;
                    Size -= UnitSize;
                    CurrentBlock = i;
                    Memory[i].Next = -1;
                    i = Memory.Count;
                    BlocksNeeded--;
                }
            }


            if (BlocksNeeded >= 1)
            {
                for (int i = 0; i < Memory.Count; i++)
                {
                    if (Memory[i].Available && Size > 0)
                    {
                        FAT[CurrentBlock] = i;
                        Memory[CurrentBlock].Next = i;
                        Memory[i].Available = false;
                        Size -= UnitSize;
                        CurrentBlock = i;
                        Memory[i].Next = -1;
                        FAT[CurrentBlock] = -1;
                    }
                }
            }

            for (int i = 0; i < FAT.Count; i++)
            {
                Console.WriteLine("{0}: Read {1}", i, FAT[i]);
            }
            Console.Read();
        }
    }

    class Node
    {
        public string Data { get { if (Available) { return "Do What U Want With Me"; } else { return "Sorry, I'm taken"; };} }
        private int? _next;
        public int Next { get { return _next ?? 0; } set { _next = value; } }
        public bool Available { get; set; }
        public Node()
        {
            Available = true;
        }
        public Node(bool available)
        {
            Available = available;
        }
    }
}
