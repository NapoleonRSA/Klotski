using System;
using System.Linq;
using System.Linq.Expressions;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            Blok blok = new Blok();
            int[] a = {1,1};
            int[] f = {0,0};
            int[] j = {0,1};
            int[] e = {1, 0, 0};
            int[] w = {1, 0, 1};

            blok.hastableCode.Add('A', a);
            blok.hastableCode.Add('F',f);
            blok.hastableCode.Add('J',j);
            blok.hastableCode.Add('E',e);
            blok.hastableCode.Add('W',w);

            setStartBoard(blok.blockBoard);
            blok.queue.CopyTo(new[] {(blok.blockBoard)},41);
            blok.piece.Enqueue(-1);
            blok.toShowTree();
        }

        private static void setStartBoard(char[] blockBoard)
        {
            startWide(blockBoard);
            startSmall(blockBoard);
            startBlank(blockBoard);
        } 

        private static void startBlank(char[] blockBoard)
        {
            int location1 = 19;
            int location2 = 26;
            char w = 'F';
            blockBoard[location1] = w;
            blockBoard[location2] = w;
        }

        private static void startSmall(char[] blockBoard)
        {
            int location1 = 12;
            int location2 = 18;
            int location3 = 25;
            int location4 = 33;
            char f = 'F';
            blockBoard[location1] = f;
            blockBoard[location2] = f;
            blockBoard[location3] = f;
            blockBoard[location4] = f;
        }

        private static void startWide(char[] blockBoard)
        {
            int location1 = 8;
            int location2 = 9;
            int location3 = 10;
            int location4 = 11;
            int location5 = 29;
            int location6 = 30;
            int location7 = 31;
            int location8 = 32;
            char a = 'A';

            blockBoard[location1] = a;
            blockBoard[location2] = a;
            blockBoard[location3] = a;
            blockBoard[location4] = a;
            blockBoard[location5] = a;
            blockBoard[location6] = a;
            blockBoard[location7] = a;
            blockBoard[location8] = a;
        }
    }
}
