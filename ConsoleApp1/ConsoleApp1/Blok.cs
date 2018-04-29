using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp1
{
    internal class Blok
    {
        public Dictionary<char,int[]> hastableCode;
        public char[] blockBoard = new char[41];
        public Queue<char[]> queue = new Queue<char[]>(new LinkedList<char[]>());
        public Queue <int> piece = new Queue<int>(new LinkedList<int>());
        public LinkedList<int[]> node_tree = new LinkedList<int[]>();

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
                                    foreach (int i in hastableCode.Keys)
                                    {
                                        n[countN] = i;
                                        charCount++;
                                    }
                                }
                            }
                        }
                    }

                    int[] nodeNew = new int[] { };
                    Array.Copy(n, nodeNew, 45);
                    bool duplicate = false;
                    if (parent_InitialIndex > 0)
                    {
                        duplicate = isDuplicate(n, parent_InitialIndex, iterator);
                    }

                    if (!duplicate)
                    {
                        storeChild(nodeNew);
                        isSolution(otherBoard);
                        int index = node_tree.Select((item, inx) => new {item, inx}).First(x => x.item == nodeNew).inx;
                        indexofParent.ToArray().ElementAt(index);
                    }
                }
            }
            findChild(indexofParent);
            toShowTree();
        }

        private void isSolution(char[] otherBoard)
        {
            if (otherBoard[18] =='J' && otherBoard[19] == 'J' && otherBoard[25]=='J')
            {
                winList(node_tree.Last.Value);
            }
        }
        public int[] copyOfRange(int[] src, int start, int end)
        {
            int len = end - start;
            int[] dest = new int[len];
            Array.Copy(src, start, dest, 0, len);
            return dest;
        }
        private void winList(int[] last)
        {
            PrintSolution pr = new PrintSolution();
            LinkedList<int[]> list = new LinkedList<int[]>();
            int parentIndex = last[0];
            list.CopyTo(new []{last},1);
            while (parentIndex >-1)
            {
                int[] parentNode = node_tree.ElementAt(parentIndex);
                list.AddFirst(copyOfRange(parentNode,1,parentNode.Length));
            }
        }

        private void storeChild(int[] nodeNew)
        {
            node_tree = new LinkedList<int[]>(new[] {nodeNew});
        }

        private bool isDuplicate(int[] node, int parentInitialIndex, IEnumerator<char[]> iterator)
        {
            int[] child = new int[] { }; 
            Array.Copy(node, child, 45);
            bool dupFound = false;
        }

        private bool testInBounds(int i)
        {
            if (i<8)
            {
                return false;
            }
            
            if (i > 35)
            {
                return false;
            }

            if (i%7 == 0)
            {
                return false;
            }

            if ((i + 1) % 7 == 0)
            {
                return false;
            }
            return true;
        }
    }
}