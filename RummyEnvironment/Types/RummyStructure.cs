using System;

namespace RummyEnvironment
{
    public abstract class RummyStructure
    {
        public Guid Id { get; }

        public Token[] Tokens { get; }

        public RummyStructure(Guid id, Token[] tokens)
        {
            this.Id = id;
            this.Tokens = tokens;
        }
    }
}
