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

        public IActionResult CanAdd(IToken tokenToInsert)
        {
            return this.CanAdd(new List<IToken> { tokenToInsert });
        }

        public abstract IActionResult CanAdd(List<IToken> tokensToInsert);

        public List<IOperationResult> Add(IToken tokenToInsert)
        {
            return this.Add(tokenToInsert, new List <IToken>());
        }

        public List<IOperationResult> Add(IToken tokenToInsert, List<IToken> extraTokens)
        {
            return this.Add(new List<IToken> { tokenToInsert }, extraTokens);
        }

        public List<IOperationResult> Add(List<IToken> tokensToInsert)
        {
            return this.Add(tokensToInsert, new List <IToken>());
        }

        public abstract List<IOperationResult> Add(List<IToken> tokensToInsert, List<IToken> extraTokens);

        public IActionResult CanGet(IToken tokenToGet)
        {
            return this.CanGet(new List<IToken> { tokenToGet });
        }

        public abstract IActionResult CanGet(List<IToken> tokensToGet);

        public List<IOperationResult> Get(IToken tokenToGet)
        {
            return this.Get(new List<IToken> { tokenToGet });
        }

        public abstract List<IOperationResult> Get(List<IToken> tokensToGet);

        protected void CheckTokenStructure(List<IToken> tokens)
        {
            if (!this.IsValidTokenStructure(tokens))
            {
                throw new RummyException("Invalid tokenToInsert structure.");
            }
        }
    }
}
