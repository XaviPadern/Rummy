using System;
using System.Drawing;

namespace RummyEnvironment
{
    public class Token : IToken
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

            while (this.Id.Equals(Guid.Empty))
            {
                this.Id = Guid.NewGuid();
            }
            this.Color = color;
            this.Number = number;
        }

        public static bool IsTokenValid(IToken token)
        {
            Guid outId;
            
            bool isValidId = !token.Id.Equals(Guid.Empty) && Guid.TryParse(token.Id.ToString(), out outId);

            return isValidId && IsValidColor(token.Color) && IsValidNumber(token.Number);
        }

        public bool IsEqual(IToken token)
        {
            return this.Id.Equals(token.Id) && this.IsEquivalent(token);
        }

        public bool IsEquivalent(IToken token)
        {
            return this.Color.Equals(token.Color) && this.Number == token.Number;
        }
        
        public object Clone()
        {
            return this.MemberwiseClone();
        }

        private static bool IsValidColor(Color color)
        {
            return (color == Color.Blue || color == Color.Yellow || color == Color.Red || color == Color.Black);
        }

        private static bool IsValidNumber(int number)
        {
            return (number >=1 && number <=13);
        }
    }
}
