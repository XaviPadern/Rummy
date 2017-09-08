using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using RummyEnvironment.Helpers;

namespace RummyEnvironment
{
    public class TokensLake : ITokensLake
    {
        public List<IToken> Tokens { get; private set; }

        public TokensLake()
        {
        }

        public TokensLake(List<IToken> tokens)
        {
            this.Tokens = tokens;
        }

        public void CreateNew()
        {
            this.Tokens = new List<IToken>();

            List<Color> colors = new List<Color> { Color.Blue, Color.Yellow, Color.Red, Color.Black };
            List<int> numbers = Enumerable.Range(1, 13).ToList();

            // 2 sets of tokens.
            foreach (Color color in colors)
            {
                foreach (int number in numbers)
                {
                    this.Tokens.Add(new Token(color, number));
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

        public List<IToken> GrabToken()
        {
            return this.GrabTokens(1);
        }

        public List<IToken> GrabTokens(int numberOfTokens)
        {
            List<IToken> tokensToMove = this.Tokens.GetRange(0, numberOfTokens);
            this.Tokens.RemoveRange(0, numberOfTokens);
            return tokensToMove;
        }
    }
}
