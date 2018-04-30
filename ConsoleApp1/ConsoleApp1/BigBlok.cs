namespace ConsoleApp1
{
    public class BigBlok
    {
        public int topLeft;
        public int topRight;
        public int bottomLeft;
        public int bottomRight;
        public char b = 'J';
        Blok blok = new Blok();

        public BigBlok()
        {
            topLeft = 15;
            topRight = 16;
            bottomLeft = 22;
            bottomRight = 23;
            blok.blockBoard[topLeft] = b;
            blok.blockBoard[topRight] = b;
            blok.blockBoard[bottomLeft] = b;
            blok.blockBoard[bottomRight] = b;
        }

        public void setBblok(int p0)
        {
            topLeft = p0;
            topRight = p0 + 1;
            bottomLeft = p0 + 7;
            bottomRight = p0 + 8;
            blok.blockBoard[topLeft] = b;
            blok.blockBoard[topRight] = b;
            blok.blockBoard[bottomRight] = b;
            blok.blockBoard[bottomLeft] = b;
        }
    }
}