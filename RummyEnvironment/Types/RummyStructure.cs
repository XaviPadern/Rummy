using System;

namespace RummyEnvironment
{
    public abstract class RummyStructure : IRummyStructure
    {
        public Guid Id { get; }

        public IToken[] Tokens { get; }

        public RummyStructure(Guid id, IToken[] tokens)
        {
            this.Id = id;
            this.Tokens = tokens;
        }
    }
}
