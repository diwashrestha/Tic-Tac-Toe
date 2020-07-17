using System;

namespace Tic_Tac_Toe
{
    class Program
    {
        public static void Main(string[] args)
        {
            int[] winCount = new int[] { 0, 0, 0 };
        //  for replaying the game
        GameStart:
            Console.WriteLine("The grid and coordinates for the game is:");

// showing grid of the program
            Console.WriteLine("\n------------------");
            Console.WriteLine("| (13) (23) (33) |");
            Console.WriteLine("| (12) (22) (32) |");
            Console.WriteLine("| (11) (21) (31) |");
            Console.WriteLine("------------------");


            Console.WriteLine("\n ***** Tic - Tac - Toe ! ***** : \n");
            // At the beginning of the game it is empty
            string userInput = "         ";
            char[] user1dInput = userInput.ToCharArray();

            // At the start of the game  X player initiate the game
            char player = 'X';

            // boolean to track the ending of the game and message to be printed at the end of game
            bool gameEnd = false;
            string gameMessage = "";

            // Changing the 1d array to 2d array
            char[,] user2dInput = new char[3,3];
            int index = 0;

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    user2dInput[i, j] = user1dInput[index];
                    index++;
                }
            }

            PrintUserArray(user2dInput);


            // this loop keeps running till the gameEnd value is not true
            while(!gameEnd)
            {
                user2dInput = TakeUserCoord(user2dInput,ref player);
                PrintUserArray(user2dInput);

                player = player == 'X' ? 'O' : 'X';

                CheckWin(user2dInput, ref gameEnd, ref gameMessage, ref winCount);
            }
            Console.WriteLine("{0}", gameMessage);
            Console.WriteLine(" ----------------------------");
            Console.WriteLine(" |         Point Table       |");
            Console.WriteLine(" ----------------------------");
            Console.WriteLine(" |    X    |    O   |  Draw  |");
            Console.WriteLine(" ----------------------------");
            Console.Write(" |");
            for (int i = 0; i < 3; i++)
            {
                Console.Write("    {0}    ", winCount[i]);
            }
            Console.Write("|");
            Console.Write("\n");
            Console.WriteLine(" ---------------------------");


            // Can be replay the game
            Console.WriteLine("Do you want play once more (yes|no) ?");
            string userVote = Console.ReadLine();
            if (userVote == "yes")
            {
                Console.Clear();
                Console.WriteLine("Lets play Again!\n");
                goto GameStart;
            }
            else
            {
                Console.WriteLine("*** Have a great day! See You Again ***");
            }


            Console.Read();
        }



        // Method for taking user input for the Coordinates
        private static char[,]  TakeUserCoord(char[,] grid2DArray, ref char player)
        {

            Console.WriteLine("[Player {0}] \n Enter the Coordinates: ",player);
            string coord = Console.ReadLine();
            char[] coordCharArray = coord.ToCharArray();
            char[,] gridInput = CheckUserCoord(coordCharArray, grid2DArray, ref player);
            return gridInput;
        }



        // Method for checking the  user input for the coordinates
        public static char[,]  CheckUserCoord(char[] userCharCoord, char[,] gridInput, ref char player)
        {
            int number;
            int[] userCoord = new int[2];
            if(Int32.TryParse(userCharCoord, out number) == false)
            {
                Console.WriteLine("Enter Number only!");
                TakeUserCoord(gridInput, ref player);
            }
            else
            {
                userCoord = Array.ConvertAll(userCharCoord, c => (int)Char.GetNumericValue(c));
                if (userCoord[0] > 3 || userCoord[0] < 1 || userCoord[1] > 3 || userCoord[1] < 1)
                {
                    Console.WriteLine("Coordinates should be from 1 to 3!");
                    TakeUserCoord(gridInput, ref player);
                }

                else if (gridInput[(3 - userCoord[1]), (userCoord[0] - 1)] == 'X' || gridInput[(3 - userCoord[1]), (userCoord[0] - 1)] == 'O')
                {
                    Console.WriteLine("This cell is occupied! Choose another one!");
                    TakeUserCoord(gridInput, ref player);
                }
                else
                {
                    gridInput[(3 - userCoord[1]), (userCoord[0] - 1)] = player;
                }
            }
            return gridInput;
        }

        // Method to print the 2d array of tic tac toe
        private static char PrintUserArray(char [,] userInput)
        {

            Console.WriteLine("-------------");
            Console.WriteLine();
            for (int i = 0; i < 3; i++)
            {
                Console.Write("|");
                Console.Write(" ");
                for (int j = 0; j < 3; j++)
                {
                    Console.Write(" ");
                    Console.Write(userInput[i, j]);
                    Console.Write(" ");
                }
                Console.Write(" ");
                Console.Write("|");
                Console.Write("\n");
                Console.WriteLine();
            }
            Console.WriteLine("-------------");
            return '0';
        }


        // method to check the different game situation
        private static bool CheckWin(char[,] input, ref bool gameEnd , ref string gameMessage, ref int[] winCount)
        {
            // var to check the difference between 'X' and 'O' count in grid
            int charCountDiff = 0;
            charCountDiff = CharCounter(input, 'X') - CharCounter(input, 'O');

            if ((IsWin(input,'X') && IsWin(input,'O'))|| Math.Abs(charCountDiff) >= 2)
            {
                gameEnd = true;
                gameMessage = "Impossible";
            }
            else if (IsWin(input,'X'))
            {
                gameEnd = true;
                gameMessage = "X Wins!";
                winCount[0]++;
            }
            else if (IsWin(input,'O'))
            {
                gameEnd = true;
                gameMessage = "O Wins!";
                winCount[1]++;
            }
            else if (IsDraw(input))
            {
                gameEnd = true;
                gameMessage = "Draw!";
                winCount[2]++;
            }
            else if (!IsWin(input, 'X') && !IsWin(input, 'O') && !IsDraw(input))
            {
                gameEnd = false;
                gameMessage = " Game not finished";
            }

            return gameEnd;
        }


        // method for finding winning condition
        static bool IsWin(char[,] userInput, char sign)
        {
            // row win case
            return (userInput[0, 0] == sign && userInput[0, 1] == sign && userInput[0, 2] == sign
                    || userInput[1, 0] == sign && userInput[1, 1] == sign && userInput[1, 2] == sign
                    || userInput[2, 0] == sign && userInput[2, 1] == sign && userInput[2, 2] == sign
                    // column win case
                    || userInput[0, 0] == sign && userInput[1, 0] == sign && userInput[2, 0] == sign
                    || userInput[0, 1] == sign && userInput[1, 1] == sign && userInput[2, 1] == sign
                    || userInput[0, 2] == sign && userInput[1, 2] == sign && userInput[2, 2] == sign
                    // diagonal win case
                    || userInput[0, 0] == sign && userInput[1, 1] == sign && userInput[2, 2] == sign
                    || userInput[0, 2] == sign && userInput[1, 1] == sign && userInput[2, 0] == sign);
        }


        // method to check the draw condition
        static bool IsDraw(char[,] userInput)
        {
            // var for the space in the grid
            bool isSpace = false;
            // var to find is there draw in the game or not
            bool isDraw = false;
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if ((userInput[i, j] == '_'|| userInput[i, j] == ' '))
                    {
                        isSpace = true;
                    }
                }
            }

            if (isSpace == false && !IsWin(userInput, 'X') && !IsWin(userInput, 'O'))
            {
                isDraw = true;
            }

            return isDraw;
        }

        // Method to find the impossible cases where
        // the field has a lot more X's than O's or vice versa
        static int CharCounter(char[,] userArray, char sign)
        {
            int count = 0;
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if ((userArray[i, j] == sign))
                    {
                        count++;
                    }
                }
            }
            return count;
        }
    }
}
