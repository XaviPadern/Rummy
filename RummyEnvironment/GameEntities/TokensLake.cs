using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using RummyEnvironment.Helpers;

namespace RummyEnvironment
{
    public class TokensLake
    {
        public List<Token> Tokens { get; private set; }

        public TokensLake()
        {
        }

        public TokensLake(List<Token> tokens)
        {
            this.Tokens = tokens;
        }

        public void CreateNew()
        {
            this.Tokens = new List<Token>();

            List<Color> colors = new List<Color> { Color.Blue, Color.Yellow, Color.Red, Color.Black };
            List<int> numbers = Enumerable.Range(1, 13).ToList();

            foreach (Color color in colors)
            {
                foreach (int number in numbers)
                {
                    this.Tokens.Add(new Token(color, number));
                }
            }
        }

        public void ShuffleTokensList()
        {
            if (this.Tokens == null || this.Tokens.Count < 2)
            {
                return;
            }
            this.Tokens.Shuffle();
        }

        public List<Token> GetToken()
        {
            return this.GrabTokens(1);
        }

        public List<Token> GrabTokens(int numberOfTokens)
        {
            List<Token> tokensToMove = this.Tokens.GetRange(0, numberOfTokens);
            this.Tokens.RemoveRange(0, numberOfTokens);
            return tokensToMove;
        }
    }
}
