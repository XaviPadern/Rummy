using System;
using System.Collections.Generic;

namespace RummyEnvironment
{
    public abstract class RummyStructure : IRummyStructure
    {
        public Guid Id { get; protected set; }

        public List<IToken> Tokens { get; protected set; }

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

        //public IActionResult CanAdd(IToken tokenToInsert)
        //{
        //    return this.CanAdd(new List<IToken> { tokenToInsert });
        //}
        public abstract IActionResult CanAdd(IToken tokenToInsert);

        public abstract IActionResult CanAdd(List<IToken> tokensToInsert);

        //public List<IOperationResult> Add(IToken tokenToInsert)
        //{
        //    return this.Add(new List<IToken> { tokenToInsert });
        //}
        public abstract List<IOperationResult> Add(IToken tokenToInsert);

        public abstract List<IOperationResult> Add(List<IToken> tokensToInsert);

        //public IActionResult CanGet(IToken tokenToGet)
        //{
        //    return this.CanGet(new List<IToken> { tokenToGet });
        //}
        public abstract IActionResult CanGet(IToken tokenToGet);

        public abstract IActionResult CanGet(List<IToken> tokensToGet);

        //public List<IOperationResult> Get(IToken tokenToGet)
        //{
        //    return this.Get(new List<IToken> { tokenToGet });
        //}
        public abstract List<IOperationResult> Get(IToken tokenToGet);

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
