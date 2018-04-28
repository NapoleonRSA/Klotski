using System.Collections;
using System.Collections.Generic;

namespace ConsoleApp1
{
    internal class Blok
    {
        public Dictionary<char,int[]> hastableCode;
        public char[] blockBoard = new char[41];
        public Queue<char[]> queue = new Queue<char[]>(new LinkedList<char[]>());
        public Queue <int> piece = new Queue<int>(new LinkedList<int>());

        public void toShowTree()
        {
            Queue<int> indexofParent = new Queue<int>(new LinkedList<int>());
            int [] n = new int[45];
            int parent_InitialIndex = 0;
            using (IEnumerator<char[]> iterator = queue.GetEnumerator())
            {
                while (iterator.MoveNext())
                {
                    char[] otherBoard = iterator.Current;
                    if (piece.Count != 0)
                    {
                        parent_InitialIndex = piece.Dequeue();
                    }

                    n[0] = parent_InitialIndex;
                    int countN = 1;
                    int charCount = 0;

                    foreach (char c in otherBoard)
                    {
                        if (testInBounds(charCount++))
                        {
                            foreach ( char s in hastableCode.Keys)
                            {
                                if (s.Equals(c))
                                {
                                    foreach (int i in hastableCode.TryGetValue(s))
                                    {
                                        
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}