using System.Collections.Generic;

namespace RummyEnvironment
{
    public class Player : IPlayer
    {
        public string Name { get; }

        public List<IToken> Tokens { get; }

        public Player(string name, List<IToken> tokens)
        {
            this.Name = name;
            this.Tokens = tokens;
        }
    }
}
