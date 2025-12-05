using System.Text.RegularExpressions;

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

        static bool isValidMove(int player, int[] pieceCoords, int[] moveCoords, bool isCapture)
        {
            switch(player)
            {
                //biały
                case 1:

                    if (!((pieceCoords[1] == moveCoords[1] + 1 || pieceCoords[1] == moveCoords[1] - 1) && pieceCoords[0] == moveCoords[0] + 1))
                    {
                        Console.WriteLine("Niepoprawny ruch");
                        return false;
                    }
                    break;
                //czarny
                case 2:
                    if (!((pieceCoords[1] == moveCoords[1] + 1 || pieceCoords[1] == moveCoords[1] - 1) && pieceCoords[0] == moveCoords[0] - 1))
                    {
                        Console.WriteLine("Niepoprawny ruch");
                        return false;
                    }
                    break;
            }
            if(!isCapture)
            {
                if (board[pieceCoords[0], pieceCoords[1]] != player)
                {
                    Console.WriteLine("Niepoprawny ruch (nie twój pionek)");
                    return false;
                }
            }
            return true;
        }

        static bool isValidCapture(int player, int[] pieceCoords, int[] moveCoords)
        {
            int enemyPlayer = (player == 1) ? 2 : 1;
            switch (player)
            {
                case 1:
                    //Console.WriteLine((moveCoords[0] + 1) + "" + (moveCoords[1] + 1));
                    //Console.WriteLine((moveCoords[0] + 1) + "" + (moveCoords[1] - 1));
                    //return false;
                    if (board[moveCoords[0] + 1, moveCoords[1] + 1] == enemyPlayer || board[moveCoords[0] + 1, moveCoords[1] - 1] == enemyPlayer)
                    {
                        return true;
                    }
                    break;
                case 2:
                    Console.WriteLine("asd");
                    Console.WriteLine((moveCoords[0] - 1) + " " + (moveCoords[1] + 1));
                    Console.WriteLine((moveCoords[0] - 1) + " " + (moveCoords[1] - 1));
                    //return false;
                    bool enemyOnRight = board[moveCoords[0] - 1, moveCoords[1] + 1] == enemyPlayer;
                    bool enemyOnLeft = board[moveCoords[0] - 1, moveCoords[1] - 1] == enemyPlayer;
                    if (enemyOnRight || enemyOnLeft)
                    {
                        int[] tempPieceCoords = { pieceCoords[0] - 1, pieceCoords[1] + 1 };
                        Console.WriteLine(tempPieceCoords[0] + " " + tempPieceCoords[1]);
                        return false;
                    }
                    break;
            }
            return false;
        }

        static void capturePiece(int player, int[] pieceCoords, int[] moveCoords)
        {
            int enemyPlayer = (player == 1) ? 2 : 1;
            switch (player)
            {
                case 1:
                    bool enemyOnLeft = board[moveCoords[0] - 1, moveCoords[1] + 1] == enemyPlayer;
                    Console.WriteLine(moveCoords[0] - 1 + " " + moveCoords[1] + 1);
                    //if (isValidCapture(player, pieceCoords, moveCoords))
                    //{
                        int[] tempPieceCoords = { pieceCoords[0] - 1, pieceCoords[1] + 1 };
                        Console.WriteLine("Bicie aaa");
                        Console.WriteLine($"TempPiece: {tempPieceCoords[0]} {tempPieceCoords[1]}");
                        Console.WriteLine(isValidMove(player, tempPieceCoords, moveCoords, true));
                        if (isValidMove(player, tempPieceCoords, moveCoords, true))
                        {
                            board[tempPieceCoords[0], tempPieceCoords[1]] = 0;
                        }
                    //}
                    //Console.WriteLine("Bicie eeee");
                    break;
                case 2:
                    //if (isValidCapture(player, pieceCoords, moveCoords))
                    //{
                        int[] tempPieceCoords2 = { pieceCoords[0] + 1, pieceCoords[1] + 1 };
                        if(isValidMove(player, tempPieceCoords2, moveCoords, true))
                        {
                            board[tempPieceCoords2[0], tempPieceCoords2[1]] = 0;
                        }
                    //}
                    break;
            }
            //return true;
        }

        static void playerTurn(int player, int[] pieceCoords, int[] moveCoords)
        {
            //int enemyPlayer = (player == 1) ? 2 : 1;
            //if (board[moveCoords[0] - 1, moveCoords[1] + 1] == enemyPlayer || board[moveCoords[0] - 1, moveCoords[1] - 1] == enemyPlayer)
            //{
            //    isValidCapture(player, pieceCoords, moveCoords);
            //
            if(isValidCapture(player, pieceCoords, moveCoords))
                capturePiece(player, pieceCoords, moveCoords);
            else
            {
                Console.WriteLine("Brak bicia");
                if (!isValidMove(player, pieceCoords, moveCoords, false)) return;
            }

            board[pieceCoords[0], pieceCoords[1]] = 0;
            board[moveCoords[0], moveCoords[1]] = player;
            Console.Clear();
            displayBoard();
        }


        static void Main(string[] args)
        {
            int player = 1; // 1 - biały 2 - czarny
            bool gameOver = false;
            Regex inputPattern = new Regex("([0-7]),([0-7]) na ([0-7]),([0-7])$");
            initializeBoard();
            displayBoard();


            while(!gameOver)
            {
                if (player == 1)
                    Console.WriteLine("Tura: **BIAŁY** (Pionek: B)");
                else if (player == 2)
                    Console.WriteLine("Tura: **CZARNY** (Pionek: C)");

                Console.Write("Wpisz ruch (np. 5,0 na 4,1): ");
                string input = Console.ReadLine().Trim();
                while (!inputPattern.IsMatch(input))
                {
                    Console.WriteLine("Niepoprawny ruch");
                    Console.Write("Wpisz ruch (np. 5,0 na 4,1): ");
                    input = Console.ReadLine().Trim();
                }
                int[] pieceCoords = { input[0] - '0', input[2] - '0' };
                int[] moveCoords = { input[7] - '0', input[9] - '0' };

                playerTurn(player, pieceCoords, moveCoords);

                player = (player == 1) ? 2 : 1;
            }
        }
    }
}
