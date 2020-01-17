using System;
using System.Collections.Generic;
using System.Linq;

namespace TicTacToe
{
    /// <summary>
    /// The Tic Tac Toe Game implementation.
    /// </summary>
    /// <remarks>
    /// To make the calculations easier, each value in the matrix is the player number
    /// raised to the second power. Therefore, if a row, column or diagonal has sum 3, player 1 won
    /// (1 + 1 + 1). If a row, column or diagonal has sum 12, player 2 won (4 + 4 + 4).
    /// A Cat's Game in general is 21 or 22, depending on what player started the game.
    /// </remarks>
    public class TicTacToe : IGame
    {
        private byte _currentPlayer;
        private string _winner;
        private int[,] _board = new int[3, 3];
        private readonly Dictionary<int, string> _symbols = new Dictionary<int, string>
        {
            { 0, "   " },
            { 1, " X " },
            { 4, " 0 " }
        };

        public TicTacToe(byte startingPlayer)
        {
            _winner = "";
            _currentPlayer = startingPlayer;
        }

        public byte CurrentPlayer => _currentPlayer;

        public string this[int position] => _symbols[_board[position / 3, position % 3]];

        public string Winner => _winner;

        /// <summary>
        /// Executes a play by the current player.
        /// </summary>
        /// <param name="Position">Number from 1 to 9, corresponding to the position chosen.</param>
        /// <returns>true if the position is not already taken, false otherwise.</returns>
        public bool Play(int Position)
        {
            int effectivePosition = Position - 1;

            if (_board[effectivePosition / 3, effectivePosition % 3] != 0)
            {
                return false;
            }

            _board[effectivePosition / 3, effectivePosition % 3] = (int)Math.Pow(_currentPlayer, 2);
            CalculateWinConditions();
            _currentPlayer = (byte)(_currentPlayer == 1 ? 2 : 1);
            return true;
        }

        /// <summary>
        /// Prints the actual state of the board, pretty style.
        /// </summary>
        public void PrintBoard()
        {
            Console.WriteLine();

            for (int i = 2; i >= 0; i--)
            {
                Console.Write(" ");

                for (int j = 0; j < 3; j++)
                {
                    Console.Write(_symbols[_board[i, j]]);
                    Console.Write(j != 2 ? "|" : " ");
                }

                Console.WriteLine();

                if (i > 0)
                    PrintSeparator();
            }

            Console.WriteLine();
        }

        private int SumX(int row)
        {
            return _board[row, 0] + _board[row, 1] + _board[row, 2];
        }

        private int SumY(int column)
        {
            return _board[0, column] + _board[1, column] + _board[2, column];
        }

        private int SumDiagonal1()
        {
            return _board[0, 0] + _board[1, 1] + _board[2, 2];
        }

        private int SumDiagonal2()
        {
            return _board[2, 0] + _board[1, 1] + _board[0, 2];
        }

        private void CalculateWinConditions()
        {
            if (new int[] {
                SumX(0), SumX(1), SumX(2), SumY(0), SumY(1), SumY(2), SumDiagonal1(), SumDiagonal2()
            }.Contains(3))
            {
                _winner = "Player X Won!";
            }

            if (new int[] {
                SumX(0), SumX(1), SumX(2), SumY(0), SumY(1), SumY(2), SumDiagonal1(), SumDiagonal2()
            }.Contains(12))
            {
                _winner = "Player O Won!";
            }

            if (_board.Cast<int>().Sum() >= 21)
            {
                _winner = "Cat's Game!";
            }
        }

        private void PrintSeparator()
        {
            Console.WriteLine("-------------");
        }
    }
}
