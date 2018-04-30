using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp1
{
    internal class PrintSolution
    {
        Dictionary<int[],string> dictionaryT = new Dictionary<int[], string>();
        private int[] a = {1, 1};
        private int[] f = {0, 0};
        private int[] j = {0, 1};
        private int[] e = {1, 0, 0};
        private int[] w = {1, 0, 1};
        private int i = 0;

        string[] start = {"A","A","B","B","F","J","J","E","G",".",
            "J","J","E","H",".","C","C","D","D","I"};

        public void show(LinkedList<int[]> list)
        {
            dictionaryT.Add(a,'A'.ToString());
            dictionaryT.Add(f,'F'.ToString());
            dictionaryT.Add(j,'J'.ToString());
            dictionaryT.Add(e,'E'.ToString());
            dictionaryT.Add(w,'W'.ToString());
            string[] curString = list.Select(x =>x.ToString()).ToArray();
            string[] nextArray = list.Select(x =>x.ToString()).ToArray();
            Compare(curString, nextArray, list);

        }

        private void Compare(string[] curString, string[] nextArray, LinkedList<int[]> list)
        {
            List<string> strList = new List<string>();
            List<int> ints = new List<int>();
            int index = 0;
            foreach (var s in nextArray)
            {
                if (s != curString[index++])
                {
                    int chanIndex = index - 1;
                    ints.Add(chanIndex);
                    strList.Add(s);
                }
            }

            nextMove(ints, strList);
            ints.Clear();
            strList.Clear();
        }

        private void nextMove(List<int> ints, List<string> strList)
        {
            int index = 0;
            int blank = 0;
            int blankPos = 0;
            int piecePos = 0;
            bool foundBlank = false;
            bool fondPiece = false;
            bool blankVertical = false;
            bool blankHor = false;
            foreach (var str in strList)
            {
                if (str != "W")
                {
                    if (!fondPiece)
                    {
                        piecePos = ints.ElementAt(index);
                        fondPiece = true;
                    }
                }
                else
                {
                    if (!foundBlank)
                    {
                        blank = index;
                        blankPos = ints.ElementAt(index);
                        foundBlank = true;
                    }
                }

                index++;
            }

            if (strList.Count() > 2)
            {
                if (strList.ElementAt(blank +1) == "W")
                {
                    blankVertical = true;
                }

                if (strList.ElementAt(blank+2) == "W")
                {
                    blankHor = true;
                }
            }

            int blankDir = blankPos - piecePos;
            string dir = "";
            if (blankDir < 0 )
            {
                dir = "right";
                if (blankDir < -2)
                {
                    dir = "down";
                }
            }
            else
            {
                dir = "left";
                if (blankDir > 2)
                {
                    dir = "up";
                }
            }

            Console.WriteLine("Move Piece " + start[blankPos] + dir +" one space" );
            start[piecePos] = start[blankPos];
            if (blankVertical)
            {
                start[piecePos + 1] = start[blankPos];
                start[blankPos + 1] = ".";
            }

            if (blankHor)
            {
                start[piecePos + 5] = start[blankPos];
                start[blankPos + 5] = ".";
            }

            start[blankPos] = ".";
        }
    }
}