using System.Collections.Generic;

namespace RummyEnvironment
{
    public class TokensLake
    {
        public List<Token> Tokens { get; }

        public TokensLake(List<Token> tokens)
        {
            this.Tokens = tokens;
        }
    }
}
