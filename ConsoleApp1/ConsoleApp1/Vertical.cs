namespace ConsoleApp1
{
    public class Vertical
    {
        public int top;
        public int bottom;
        public char e;
         Blok blok = new Blok();

        public Vertical()
        {
            top = 17;
            bottom = 24;
            e = 'E';
            blok.blockBoard[top] = e;
            blok.blockBoard[bottom] = e;
        }

        public void setVpiece(int temp)
        {
            top = temp;
            bottom = temp + 7;
            blok.blockBoard[top] = e;
            blok.blockBoard[bottom] = e;
        }
    }
}