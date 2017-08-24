using System;
using System.Drawing;

namespace RummyEnvironment
{
    public class Token
    {
        public Guid Id { get; }

        public Color Color { get; }

        public int Number { get; }

        public Token(Color color, int number)
        {
            if (!IsValidColor(color) || !IsValidNumber(number))
            {
                throw new RummyException("Invalid token to be created: " + color + ", " + number);
            }
            this.Id = Guid.NewGuid();
            this.Color = color;
            this.Number = number;
        }

        private bool IsValidColor(Color color)
        {
            return (color == Color.Blue || color == Color.Yellow || color == Color.Red || color == Color.Black);
        }

        private bool IsValidNumber(int number)
        {
            return (number >=1 && number <=13);
        }
    }
}
