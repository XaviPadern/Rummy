using System.Collections.Generic;

namespace RummyEnvironment
{
    public class Player
    {
        public string Name { get; }

        public List<Token> Tokens { get; }

        public Player(string name, List<Token> tokens)
        {
            this.Name = name;
            this.Tokens = tokens;
        }
    }
}
