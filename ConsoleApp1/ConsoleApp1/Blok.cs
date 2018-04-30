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
        public Vertical vPiece = new Vertical();
        public BigBlok bBlok = new BigBlok();

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

        private void findChild(Queue<int> indexofParent)
        {
            int qSize = queue.Count;
            for (int i = 0; i < qSize; i++)
            {
                int parI = indexofParent.Dequeue();
                char[] c = queue.Dequeue();
                movePiece(c, parI);
            }
        }

        private void movePiece(char[] parent, int parI)
        {
            resetParent(parent);
            int index = 0;
            bool found = false;
            int piece1 = 0;
            int piece2 = 0;
            foreach (var c in parent)
            {
                if (testInBounds(index))
                {
                    if (!found && c == 'W')
                    {
                        piece1 = index;
                        found = true;
                    }

                    if (c == 'W')
                    {
                        piece2 = index;
                    }
                }

                index++;
            }

            int temp = 0;
            int countChild = 0;
            while (true)
            {
                if (parent[piece2 + 7] == 'F')
                {
                    temp = piece2;
                    blockBoard[temp + 7] = 'W';
                    blockBoard[temp] = 'F';
                    piece.Enqueue(parI);
                    queue.CopyTo(new[] {blockBoard}, 41);
                    resetParent(parent);
                    countChild++;
                }

                if (parent[piece1 + 7] == 'F')
                {
                    temp = piece2;
                    blockBoard[temp + 7] = 'W';
                    blockBoard[temp] = 'F';
                    piece.Enqueue(parI);
                    queue.CopyTo(new[] {blockBoard}, 41);
                    resetParent(parent);
                    countChild++;
                }

                if (parent[piece2 - 7] == 'F')
                {
                    temp = piece2;
                    blockBoard[temp - 7] = 'W';
                    blockBoard[temp] = 'F';
                    piece.Enqueue(parI);
                    queue.CopyTo(new[] {blockBoard}, 41);
                    resetParent(parent);
                    countChild++;
                }

                if (parent[piece1 - 7] == 'F')
                {
                    temp = piece2;
                    blockBoard[temp - 7] = 'W';
                    blockBoard[temp] = 'F';
                    piece.Enqueue(parI);
                    queue.CopyTo(new[] {blockBoard}, 41);
                    resetParent(parent);
                    countChild++;
                }

                if (parent[piece1 + 1] == 'F')
                {
                    temp = piece1;
                    blockBoard[temp + 1] = 'W';
                    blockBoard[temp] = 'F';
                    queue.CopyTo(new[] {blockBoard}, 41);
                    resetParent(parent);
                    countChild++;
                }

                if (parent[piece2 + 1] == 'F')
                {
                    temp = piece1;
                    blockBoard[temp + 1] = 'W';
                    blockBoard[temp] = 'F';
                    queue.CopyTo(new[] {blockBoard}, 41);
                    resetParent(parent);
                    countChild++;
                    if (countChild > 4)
                    {
                        break;
                    }
                }

                if (parent[piece1 - 1] == 'F')
                {
                    temp = piece1;
                    blockBoard[temp - 1] = 'W';
                    blockBoard[temp] = 'F';
                    queue.CopyTo(new[] {blockBoard}, 41);
                    resetParent(parent);
                    countChild++;
                    if (countChild > 4)
                    {
                        break;
                    }
                }

                if (parent[piece2 - 1] == 'F')
                {
                    temp = piece1;
                    blockBoard[temp - 1] = 'W';
                    blockBoard[temp] = 'F';
                    queue.CopyTo(new[] {blockBoard}, 41);
                    resetParent(parent);
                    countChild++;
                    if (countChild > 4)
                    {
                        break;
                    }
                }

                if (parent[piece1 + 1] == 'A')
                {
                    temp = piece1;
                    blockBoard[temp + 2] = 'W';
                    blockBoard[temp] = 'A';
                    blockBoard[temp + 1] = 'A';
                    queue.CopyTo(new[] {blockBoard}, 41);
                    resetParent(parent);
                    countChild++;
                    if (countChild > 4)
                    {
                        break;
                    }
                }

                if (parent[piece2 + 1] == 'A')
                {
                    temp = piece1;
                    blockBoard[temp + 2] = 'W';
                    blockBoard[temp] = 'A';
                    blockBoard[temp + 1] = 'A';
                    queue.CopyTo(new[] {blockBoard}, 41);
                    resetParent(parent);
                    countChild++;
                    if (countChild > 4)
                    {
                        break;
                    }
                }

                if (parent[piece1 - 1] == 'A')
                {
                    temp = piece1;
                    blockBoard[temp - 2] = 'W';
                    blockBoard[temp] = 'A';
                    blockBoard[temp + 1] = 'A';
                    queue.CopyTo(new[] {blockBoard}, 41);
                    resetParent(parent);
                    countChild++;
                    if (countChild > 4)
                    {
                        break;
                    }
                }

                if (parent[piece2 - 1] == 'A')
                {
                    temp = piece1;
                    blockBoard[temp - 2] = 'W';
                    blockBoard[temp] = 'A';
                    blockBoard[temp + 1] = 'A';
                    queue.CopyTo(new[] {blockBoard}, 41);
                    resetParent(parent);
                    countChild++;
                    if (countChild > 4)
                    {
                        break;
                    }
                }

                if (parent[piece2 - 7] == 'E')
                {
                    temp = piece2 - 7;
                    blockBoard[vPiece.top] = 'W';
                    vPiece.setVpiece(temp);
                    queue.CopyTo(new[] {blockBoard}, 41);
                    resetParent(parent);
                    countChild++;
                    if (countChild > 4)
                    {
                        break;
                    }
                }
                if (parent[piece1 - 7] == 'E')
                {
                    temp = piece1 - 7;
                    blockBoard[vPiece.top] = 'W';
                    vPiece.setVpiece(temp);
                    queue.CopyTo(new[] { blockBoard }, 41);
                    resetParent(parent);
                    countChild++;
                    if (countChild > 4)
                    {
                        break;
                    }
                }
                if (parent[piece2 + 7] == 'E')
                {
                    temp = piece2;
                    blockBoard[vPiece.bottom] = 'W';
                    vPiece.setVpiece(temp);
                    queue.CopyTo(new[] { blockBoard }, 41);
                    resetParent(parent);
                    countChild++;
                    if (countChild > 4)
                    {
                        break;
                    }
                }
                if (parent[piece1 + 7] == 'E')
                {
                    temp = piece1;
                    blockBoard[vPiece.bottom] = 'W';
                    vPiece.setVpiece(temp);
                    queue.CopyTo(new[] { blockBoard }, 41);
                    resetParent(parent);
                    countChild++;
                    if (countChild > 4)
                    {
                        break;
                    }
                }

                if (parent[piece1 + 1] =='W' && parent[piece1 +7] == 'A' && parent[piece1 + 8] == 'A' && parent[piece1 +9] != 'A' || parent[piece1 + 6] != 'A')
                {
                    temp = piece1;
                    blockBoard[temp + 7] = 'W';
                    blockBoard[temp + 8] = 'W';
                    blockBoard[temp] = 'A';
                    blockBoard[temp + 1] = 'A';
                    queue.CopyTo(new[] { blockBoard }, 41);
                    resetParent(parent);
                    countChild++;
                    if (countChild > 4)
                    {
                        break;
                    }
                }

                if (parent[piece1 +1] =='W' && parent[piece1 + 7]=='J' && parent[piece1 + 8 ] == 'J')
                {
                    temp = piece1;
                    blockBoard[bBlok.bottomLeft] = 'W';
                    blockBoard[bBlok.bottomRight] = 'W';
                    bBlok.setBblok(temp);
                    queue.CopyTo(new[] { blockBoard }, 41);
                    resetParent(parent);
                    countChild++;
                    if (countChild > 4)
                    {
                        break;
                    }
                }

                if (parent[piece1 + 1] == 'W' && parent[piece1 -1 ] == 'A' && parent[piece1 - 6] == 'A'
                    && parent[piece1 - 5] != 'A' || parent[piece1 - 8 ] != 'A')
                {
                    temp = piece1;
                    blockBoard[temp - 7] = 'W';
                    blockBoard[temp - 6] = 'W';
                    blockBoard[temp] = 'A';
                    blockBoard[temp + 1] = 'A';
                    queue.CopyTo(new[] { blockBoard }, 41);
                    resetParent(parent);
                    countChild++;
                    if (countChild > 4)
                    {
                        break;
                    }
                }

                if (parent[piece1+1]=='W' && parent[piece1-7 ] == 'J' && parent[piece1 -6 ] == 'J')
                {
                    temp = piece1;
                    blockBoard[bBlok.topLeft] = 'W';
                    blockBoard[bBlok.topRight] = 'W';
                    bBlok.setBblok(temp - 7);
                    queue.CopyTo(new[] { blockBoard }, 41);
                    resetParent(parent);
                    countChild++;
                    if (countChild > 4)
                    {
                        break;
                    }
                }

                if (parent[piece1 + 7] == 'W' && parent[piece1 + 1] == 'E' 
                    && parent[piece1 + 8] =='E')
                {
                    temp = piece1;
                    blockBoard[vPiece.top] = 'W';
                    blockBoard[vPiece.bottom] = 'W';
                    vPiece.setVpiece(temp);
                    queue.CopyTo(new[] { blockBoard }, 41);
                    resetParent(parent);
                    countChild++;
                    if (countChild > 4)
                    {
                        break;
                    }
                }

                if (parent[piece1 + 7] == 'W' && parent[piece1 -1] =='E' && parent[piece1 + 6] =='E')
                {
                    temp = piece1;
                    blockBoard[bBlok.topRight] = 'W';
                    blockBoard[bBlok.bottomRight] = 'W';
                    bBlok.setBblok(temp);
                    queue.CopyTo(new[] { blockBoard }, 41);
                    resetParent(parent);
                    countChild++;
                    if (countChild > 4)
                    {
                        break;
                    }
                }

                if (parent[piece1 + 7] == 'W' && parent[piece1 +1] == 'J' && parent[piece1 + 8 ] == 'J')
                {
                    temp = piece1;
                    blockBoard[bBlok.topRight] = 'W';
                    blockBoard[bBlok.bottomRight] = 'W';
                    bBlok.setBblok(temp);
                    queue.CopyTo(new[] { blockBoard }, 41);
                    resetParent(parent);
                    countChild++;
                    if (countChild > 4)
                    {
                        break;
                    }
                }

                if (parent[piece1+ 7] == 'W' && parent[piece1 -1] == 'J'
                    && parent[piece1 + 6] == 'J')
                {
                    temp = piece1;
                    blockBoard[bBlok.topLeft] = 'W';
                    blockBoard[bBlok.bottomLeft] = 'W';
                    bBlok.setBblok(temp - 1);
                    queue.CopyTo(new[] { blockBoard }, 41);
                    resetParent(parent);
                    countChild++;
                    if (countChild > 4)
                    {
                        break;
                    }
                }

                if (parent[piece2 + 1] == 'W' && parent[piece2 + 7] =='A' && parent[piece2+ 8] == 'A'
                    &&parent[piece2+9]!='A' || parent[piece2 + 6] !='A')
                {
                    temp = piece2;
                    blockBoard[temp - 7] = 'W';
                    blockBoard[temp + 8] = 'W';
                    blockBoard[temp] = 'A';
                    blockBoard[temp + 1] = 'A';
                    queue.CopyTo(new[] { blockBoard }, 41);
                    resetParent(parent);
                    countChild++;
                    if (countChild > 4)
                    {
                        break;
                    }
                }

                if (parent[piece2 + 1] == 'W' && parent[piece2 + 7 ] == 'J' && parent[piece2+8]=='J')
                {
                    temp = piece2;
                    blockBoard[bBlok.bottomLeft] = 'W';
                    blockBoard[bBlok.bottomRight] = 'W';
                    bBlok.setBblok(temp);
                    queue.CopyTo(new[] { blockBoard }, 41);
                    resetParent(parent);
                    countChild++;
                    if (countChild > 4)
                    {
                        break;
                    }
                }

                if (parent[piece2 + 1] == 'W' && parent[piece2 - 7] == 'A' && parent[piece2 - 6]== 'A'
                    && parent[piece2 -5] != 'A' || parent[piece2- 8] == 'A')
                {
                    temp = piece2;
                    blockBoard[temp - 7] = 'W';
                    blockBoard[temp - 6] = 'W';
                    blockBoard[temp] = 'A';
                    blockBoard[temp + 1] = 'A';
                    queue.CopyTo(new[] { blockBoard }, 41);
                    resetParent(parent);
                    countChild++;
                    if (countChild > 4)
                    {
                        break;
                    }
                }

                if (parent[piece2 + 1] == 'W' && parent[piece2 - 7] == 'J' && parent[piece2 - 6] == 'J')
                {
                    temp = piece2;
                    blockBoard[bBlok.topLeft] = 'W';
                    blockBoard[bBlok.topRight] = 'W';
                    bBlok.setBblok(temp - 7);
                    queue.CopyTo(new[] { blockBoard }, 41);
                    resetParent(parent);
                    countChild++;
                    if (countChild > 4)
                    {
                        break;
                    }
                }

                if (parent[piece2 +7] == 'W' && parent[piece2 + 1]=='E'&&parent[piece2 + 8] == 'E')
                {
                    temp = piece2;
                    blockBoard[vPiece.top] = 'W';
                    blockBoard[vPiece.bottom] = 'W';
                    vPiece.setVpiece(temp);
                    queue.CopyTo(new[] { blockBoard }, 41);
                    resetParent(parent);
                    countChild++;
                    if (countChild > 4)
                    {
                        break;
                    }
                }

                if (parent[piece2 + 7]=='W' && parent[piece2 - 1] == 'E' && parent[piece2 + 6] == 'E')
                {
                    temp = piece2;
                    blockBoard[vPiece.top] = 'W';
                    blockBoard[vPiece.bottom] = 'W';
                    vPiece.setVpiece(temp);
                    queue.CopyTo(new[] { blockBoard }, 41);
                    resetParent(parent);
                    countChild++;
                    if (countChild > 4)
                    {
                        break;
                    }
                }

                if (parent[piece2 + 7 ] == 'W' && parent[piece2 + 1] == 'J' && parent[piece2+8 ] == 'J')
                {
                    temp = piece2;
                    blockBoard[bBlok.topRight] = 'W';
                    blockBoard[bBlok.bottomRight] = 'W';
                    bBlok.setBblok(temp);
                    queue.CopyTo(new[] { blockBoard }, 41);
                    resetParent(parent);
                    countChild++;
                    if (countChild > 4)
                    {
                        break;
                    }
                }

                if (parent[piece2 + 7] == 'W' && parent[piece2 - 1] =='J' && parent[piece2+6] == 'J')
                {
                    temp = piece2;
                    blockBoard[bBlok.topLeft] = 'W';
                    blockBoard[bBlok.bottomLeft] = 'W';
                    bBlok.setBblok(temp - 1);
                    queue.CopyTo(new[] { blockBoard }, 41);
                    resetParent(parent);
                    countChild++;
                    if (countChild > 4)
                    {
                        break;
                    }
                }
                break;
            }
        }

        private void resetParent(char[] parent)
        {
            bool j = false;
            bool e = false;
            for (int i = 0; i < parent.Length; i++)
            {
                if (testInBounds(i))
                {
                    char c = parent[i];
                    switch (c)
                    {
                        case 'J':
                            if (!j)
                            {
                                bBlok.setBblok(i);
                                i += 1;
                                j = true;
                            }
                            break;
                        case 'A':
                            blockBoard[i] = 'A';
                            blockBoard[++i] = 'A';
                            break;
                        case 'E':
                            if (!e)
                            {
                                vPiece.setVpiece(i);
                                e = true;
                            }
                            break;
                        case 'F':
                            blockBoard[i] = 'F';
                            break;
                        case 'W':
                            blockBoard[i] = 'W';
                            break;
                        default:
                            break;
                    }
                }
            }
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
                parentIndex = parentNode[0];
            }

            pr.show(list);
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
            foreach (int[] prev in node_tree)
            {
                dupFound = false;
                var ofRange = prev;
                ofRange = copyOfRange(prev, 1, 45);
                int index = 0;
                foreach (var i in ofRange)
                {
                    if (child[index++] !=i)
                    {
                        dupFound = true;
                        break;
                    }
                }

                if (!dupFound)
                {
                    iterator.Dispose();
                    return true;
                }
            }

            return false;
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