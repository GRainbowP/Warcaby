namespace Warcaby
{
    internal class Program
    {
        // 0 - puste pole 1 - biały pionek 2 - czarny pionek
        static int[,] board = new int[8, 8];
        static void initializeBoard()
        {
            for(int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    board[i, j] = 0;
                    if(i >=5 && i <= 7)
                    {
                        if((i + j) % 2 != 0)
                        {
                            board[i, j] = 1;
                        }
                    }

                    if(i >= 0 && i <= 2)
                    {
                        if((i + j) % 2 != 0)
                        {
                            board[i, j] = 2;
                        }
                    }

                }
            }
        }

        static void displayBoard()
        {
            Console.WriteLine("    0 1 2 3 4 5 6 7");
            Console.WriteLine("    ---------------");
            for(int i = 0; i < 8; i++)
            {
                Console.Write(i + " | ");
                for(int j = 0; j < 8; j++)
                {
                    switch(board[i,j])
                    {
                        case 0:
                            Console.Write("* ");
                            break;
                        case 1:
                            Console.Write("B ");
                            break;
                        case 2:
                            Console.Write("C ");
                            break;
                    }
                }
                Console.WriteLine("|");
            }
            Console.WriteLine("    ---------------");
        }


        static void playerTurn(int turn)
        {
            if (turn == 1)
                Console.WriteLine("Tura: **BIAŁY** (Pionek: B)");
            else if (turn == 2)
                Console.WriteLine("Tura: **CZARNY** (Pionek: C)");

            Console.WriteLine("Wpisz ruch (np. 5,0 na 4,1): ");

        }

        static void gameLoop()
        {
            int turn = 1; // 1 - biały 2 - czarny
            initializeBoard();
            displayBoard();


        }
        static void Main(string[] args)
        {
            //initializeBoard();
            //displayBoard();
            gameLoop();
        }
    }
}
