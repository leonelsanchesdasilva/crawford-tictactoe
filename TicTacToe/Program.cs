using System;
using System.Linq;

namespace TicTacToe.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            string attemptedPlayer = "0";
            string[] possibleOptions = new string[] { "1", "2" };

            do
            {
                Console.Write("Starting Player [1, 2, anything else to quit] >>> ");
                attemptedPlayer = Console.ReadLine();
                
                switch (attemptedPlayer.Trim())
                {
                    case "1":
                    case "2":
                        var game = new TicTacToe(byte.Parse(attemptedPlayer));
                        int position = 0;

                        do
                        {
                            game.PrintBoard();
                            Console.WriteLine($"Current Player: [ Player {game.CurrentPlayer} ]");

                            while (position < 1 || position > 9)
                            {
                                Console.Write("Position [1-9] >>> ");
                                if (!int.TryParse(Console.ReadLine(), out position))
                                {
                                    Console.WriteLine("Invalid position, try again.");
                                    position = 0;
                                    continue;
                                }

                                if (!game.Play(position))
                                {
                                    Console.WriteLine("Position has already taken, try again.");
                                }
                            }

                            position = 0;

                        } while (game.Winner == "");

                        game.PrintBoard();
                        Console.WriteLine(game.Winner);

                        break;
                    default:
                        break;
                }

            } while (possibleOptions.Contains(attemptedPlayer));
        }
    }
}
