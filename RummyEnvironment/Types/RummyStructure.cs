using System;
using System.Collections.Generic;

namespace RummyEnvironment
{
    public abstract class RummyStructure : IRummyStructure
    {
        public Guid Id { get; private set; }

        public List<IToken> Tokens { get; private set; }

        protected RummyStructure(List<IToken> tokens)
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

        public IAddingResult CanAddInStructure(IToken tokenToInsert)
        {
            return this.CanAddInStructure(new List<IToken> { tokenToInsert });
        }

        public abstract IAddingResult CanAddInStructure(List<IToken> tokensToInsert);

        public List<IOperationResult> AddInStructure(IToken tokenToInsert)
        {
            return this.AddInStructure(tokenToInsert, new List <IToken>());
        }

        public List<IOperationResult> AddInStructure(IToken tokenToInsert, List<IToken> extraTokens)
        {
            return this.AddInStructure(new List<IToken> { tokenToInsert }, extraTokens);
        }

        public List<IOperationResult> AddInStructure(List<IToken> tokensToInsert)
        {
            return this.AddInStructure(tokensToInsert, new List <IToken>());
        }

        public abstract List<IOperationResult> AddInStructure(List<IToken> tokensToInsert, List<IToken> extraTokens);

        protected void CheckTokenStructure(List<IToken> tokens)
        {
            if (!this.IsValidTokenStructure(tokens))
            {
                throw new RummyException("Invalid tokenToInsert structure.");
            }
        }
    }
}
