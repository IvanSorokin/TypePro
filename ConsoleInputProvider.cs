using System;

namespace TypePro
{
    public class ConsoleInputProvider : IInputProvider
    {
        public char GetKey() => Console.ReadKey(true).KeyChar;
    }
}