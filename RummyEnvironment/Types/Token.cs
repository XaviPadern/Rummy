using System;
using System.Drawing;

namespace RummyEnvironment
{
    public class Token
    {
        public Guid TokenId { get; }

        public Color Color { get; }

        public int Number { get; }

        public Token(Color color, int number)
        {
            this.TokenId = Guid.NewGuid();
            this.Color = color;
            this.Number = number;
        }
    }
}
