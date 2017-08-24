using System;

namespace RummyEnvironment
{
    public abstract class RummyStructure
    {
        public Guid StructureId { get; }

        public Token[] Tokens { get; }

        public RummyStructure(Guid structureId, Token[] tokens)
        {
            this.StructureId = structureId;
            this.Tokens = tokens;
        }
    }
}
