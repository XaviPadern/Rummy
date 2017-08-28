using System;
using System.Collections.Generic;

namespace RummyEnvironment
{
    public class RummyStructure : IRummyStructure
    {
        public Guid Id { get; private set; }

        public List<IToken> Tokens { get; private set; }

        public RummyStructure(List<IToken> tokens)
        {
            this.CheckTokenStructure(tokens);

            while (this.Id.Equals(Guid.Empty))
            {
                this.Id = Guid.NewGuid();
            }
            this.Tokens = tokens;
        }

        public void Modify(List<IToken> tokens)
        {
            this.CheckTokenStructure(tokens);

            this.Tokens = tokens;
        }

        public virtual bool IsValidTokenStructure(List<IToken> tokens)
        {
            return true;
        }

        protected void CheckTokenStructure(List<IToken> tokens)
        {
            if (!this.IsValidTokenStructure(tokens))
            {
                throw new RummyException("Invalid token structure.");
            }
        }
    }
}
