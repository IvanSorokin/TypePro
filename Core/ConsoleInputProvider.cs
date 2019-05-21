using System;

namespace TypePro.Core
{
    public class ConsoleInputProvider : IInputProvider
    {
        public char GetKey() => Console.ReadKey(true).KeyChar;
    }
}