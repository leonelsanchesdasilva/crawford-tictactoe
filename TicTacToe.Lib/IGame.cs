using System;

namespace TicTacToe
{
    internal interface IGame
    {
        String Winner { get; }

        Boolean Play(Int32 Position);

        String this[Int32 position] { get; }
    }
}
