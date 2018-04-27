using System.Collections;
using System.Collections.Generic;

namespace ConsoleApp1
{
    internal class Blok
    {
        public Hashtable hastableCode;
        public char[] blockBoard = new char[41];
        public Queue<char[]> queue = new Queue<char[]>(new LinkedList<char[]>());



    }
}