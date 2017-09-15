using System.Collections.Generic;
using System.Linq;

namespace RummyEnvironment
{
    public class RummySet : RummyStructure
    {
        private const int RummySetMinimumSize = 3;
        private const int RummySetMaximumSize = 4;

        public RummySet(List<IToken> tokens) : base(tokens)
        {
        }

        public override IActionResult CanAdd(List<IToken> tokensToInsert)
        {
            // Can add token list from a set.
            return new ActionResult();
        }

        public override IActionResult CanAdd(IToken tokenToInsert)
        {
            bool isSetNumber = tokenToInsert.Number == this.Tokens.First().Number;
            bool isAvailableColor = !this.Tokens.Any(token => token.IsEquivalent(tokenToInsert));

            if (isSetNumber && isAvailableColor)
            {
                return new ActionResult(true, new List<IToken>()); 
            }
            return new ActionResult();
        }

        public override List<IOperationResult> Add(List<IToken> tokensToInsert)
        {
            // Pending: Refine message.
            throw new RummyException("Cannot add multiple tokens to a set: " + tokensToInsert);
        }

        public override List<IOperationResult> Add(IToken tokenToInsert)
        {
            IActionResult result = this.CanAdd(tokenToInsert);

            if (!result.ActionIsPossible)
            {
                // Pending: Refine message.
                throw new RummyException("Validation exception while adding tokens to structure: " + tokenToInsert);
            }

            this.Tokens.Add(tokenToInsert);
            return new List<IOperationResult>();
        }

        public override IActionResult CanGet(List<IToken> tokensToInsert)
        {
            // Can get token list from a set.
            return new ActionResult();
        }

        public override IActionResult CanGet(IToken tokensToGet)
        {
            bool areAvailableTokens = this.Tokens.Count == 4;
            bool isSetNumber = tokensToGet.Number == this.Tokens.First().Number;
            bool tokenExists = this.Tokens.Count(token => token.IsEquivalent(tokensToGet)) == 1;

            if (isSetNumber && areAvailableTokens && tokenExists)
            {
                return new ActionResult(true, new List<IToken>()); 
            }
            return new ActionResult();
        }

        public override List<IOperationResult> Get(List<IToken> tokensToInsert)
        {
            // Pending: Refine message.
            throw new RummyException("Cannot get multiple tokens from a set: " + tokensToInsert);
        }

        public override List<IOperationResult> Get(IToken tokenToGet)
        {
            IActionResult result = this.CanGet(tokenToGet);

            if (!result.ActionIsPossible)
            {
                // Pending: Refine message.
                throw new RummyException("Validation exception while getting tokens to structure: " + tokenToGet);
            }

            IToken structureTokenToGet = this.Tokens.First(token => token.IsEquivalent(tokenToGet));

            List<IOperationResult> operationResults = new List<IOperationResult>();
            operationResults.Add(new OperationResult(StructureChanges.Retrieving, new List<IToken> { (IToken)structureTokenToGet.Clone() }));

            this.Tokens.Remove(structureTokenToGet);
            return operationResults;
        }

        public override bool IsValidTokenStructure(List<IToken> tokens)
        {
            return IsTokenSequenceValid(tokens, RummySetMinimumSize, RummySetMaximumSize);
        }
        
        private static bool IsTokenSequenceValid(List<IToken> tokens, int minimumSequenceSize, int maximumSequenceSize)
        {
            // At least, 3 tokens are needed to perform a set.
            if (tokens.Count < minimumSequenceSize || tokens.Count > maximumSequenceSize)
            {
                return false;
            }

            // All the elements have different color.
            if (!tokens.TrueForAll(token => tokens.Count(tok => tok.Color.Equals(token.Color)) == 1))
            {
                return false;
            }

            // All the elements have the same number.
            if (tokens.Any(token => token.Number != tokens.First().Number))
            {
                return false;
            }

            return true;
        }
    }
}
