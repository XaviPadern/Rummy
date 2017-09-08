using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using RummyEnvironment.Helpers;

namespace RummyEnvironment
{
    public class RummyLadder : RummyStructure
    {
        private const int RummyLadderMinimumSize = 3;
        private const int RummyLadderMaximumSize = 13;

        public RummyLadder(List<IToken> tokens) : base(tokens)
        {
        }

        public override IActionResult CanAdd(IToken tokenToInsert)
        {
            return this.CanAdd(new List<IToken> { tokenToInsert });
        }

        public override IActionResult CanAdd(List<IToken> tokensToInsert)
        {
            if (!IsTokensToInsertSequenceValid(tokensToInsert, 1, RummyLadderMaximumSize - this.Tokens.Count))
            {
                return new ActionResult();
            }

            bool isAddingInTheExtremes = this.AreTokensToBeAddedFitInTheInitialExtreme(tokensToInsert) ||
                                         this.AreTokensToBeAddedFitInTheFinalExtreme(tokensToInsert);

            return new ActionResult(isAddingInTheExtremes, new List<IToken>());
        }

        public override List<IOperationResult> Add(IToken tokenToInsert)
        {
            return this.Add(new List<IToken> { tokenToInsert });
        }

        public override List<IOperationResult> Add(List<IToken> tokensToInsert)
        {
            IActionResult result = this.CanAdd(tokensToInsert);

            if (!result.ActionIsPossible)
            {
                // Pending: Refine message.
                throw new RummyException("Validation exception while adding tokens to structure: " + tokensToInsert);
            }

            if (this.AreTokensToBeAddedFitInTheInitialExtreme(tokensToInsert))
            {
                this.Tokens.InsertRange(0, tokensToInsert);
            }
            else
            {
                this.Tokens.AddRange(tokensToInsert);
            }
            return new List<IOperationResult>();
        }

        public override IActionResult CanGet(IToken tokenToGet)
        {
            return this.CanGet(new List<IToken> { tokenToGet });
        }

        public override IActionResult CanGet(List<IToken> tokensToGet)
        {
            IActionResult result = new ActionResult();

            if (!IsTokensToInsertSequenceValid(tokensToGet, 1, this.Tokens.Count - 1))
            {
                return result;
            }

            if (tokensToGet.First().Number < this.Tokens.First().Number || tokensToGet.Last().Number > this.Tokens.Last().Number)
            {
                return result;
            }
            
            //Extremes.
            if (this.Tokens.First().IsEquivalent(tokensToGet.First()) ||
                this.Tokens.Last().IsEquivalent(tokensToGet.Last()))
            {
                if (tokensToGet.Count <= this.Tokens.Count - 3)
                {
                    result.ActionIsPossible = true;
                }
                return result;
            }

            int firstTokenIndex = this.Tokens.FindIndex(token => token.IsEquivalent(tokensToGet.First()));
            int lastTokenIndex = this.Tokens.FindIndex(token => token.IsEquivalent(tokensToGet.First()));

            if (firstTokenIndex < 3 && (this.Tokens.Count - lastTokenIndex) > 3)
            {
                // Spare tokens in both sides.
                return result;
            }

            result.ActionIsPossible = true;
            result.NeededExtraTokens = this.Tokens.Clone().ToList();
            result.NeededExtraTokens = result.NeededExtraTokens.Except(
                this.Tokens.GetRange(lastTokenIndex + 1, this.Tokens.Count - lastTokenIndex - 1)).ToList();

            return result;
        }
        
        public override List<IOperationResult> Get(IToken tokenToGet)
        {
            return this.Get(new List<IToken> { tokenToGet });
        }

        public override List<IOperationResult> Get(List<IToken> tokensToGet)
        {
            List<IOperationResult> operationResults = new List<IOperationResult>();

            IActionResult result = this.CanGet(tokensToGet);

            if (!result.ActionIsPossible)
            {
                // Pending: Refine message.
                throw new RummyException("Validation exception while getting tokens from structure: " + tokensToGet);
            }

            // Create the retrieving operation:
            List<IToken> retrievedTokens = new List<IToken>();

            foreach (IToken token in tokensToGet)
            {
                retrievedTokens.Add((IToken)this.Tokens.First(tok => token.IsEquivalent(tok)).Clone());
            }

            OperationResult retrievingOperation = new OperationResult(StructureChanges.Retrieving, retrievedTokens);
            operationResults.Add(retrievingOperation);

            //this.Tokens.AddRange(extraTokens);

            List<IToken> remainingTokens = this.Tokens.Except(tokensToGet).ToList();
            List<List<IToken>> resultingLists = this.SplitTokensInLists(remainingTokens);

            if (resultingLists.Count == 1)
            {
                this.Tokens = resultingLists.First();
                return operationResults;
            }

            if (resultingLists.First().Count >= resultingLists.Last().Count)
            {
                this.Tokens = resultingLists.First();
                operationResults.Add(new OperationResult(StructureChanges.SpareTokens, resultingLists.Last()));
                return operationResults;
            }

            this.Tokens = resultingLists.Last();
            operationResults.Add(new OperationResult(StructureChanges.SpareTokens, resultingLists.First()));
            return operationResults;
        }

        public override bool IsValidTokenStructure(List<IToken> tokens)
        {
            return IsTokenSequenceValid(tokens, RummyLadderMinimumSize, RummyLadderMaximumSize);
        }

        private bool AreTokensToBeAddedFitInTheInitialExtreme(List<IToken> tokensToInsert)
        {
            return tokensToInsert.Last().Number == this.Tokens.First().Number - 1;
        }

        private bool AreTokensToBeAddedFitInTheFinalExtreme(List<IToken> tokensToInsert)
        {
            return tokensToInsert.First().Number == this.Tokens.Last().Number + 1;
        }

        private List<List<IToken>> SplitTokensInLists(List<IToken> tokens)
        {
            List<List<IToken>> splittedTokens = new List<List<IToken>>();

            if (tokens.Count == 0)
            {
                return splittedTokens;
            }

            int lastNumber = tokens.First().Number - 1;
            List<IToken> currentList = new List<IToken>();

            foreach (IToken token in tokens)
            {
                if (token.Number != lastNumber + 1)
                {
                    splittedTokens.Add((List<IToken>)currentList.Clone());
                    currentList = new List<IToken>();
                }
                currentList.Add(token);
                lastNumber = token.Number;
            }
            splittedTokens.Add((List<IToken>)currentList.Clone());
            return splittedTokens;
        }

        private bool IsTokensToInsertSequenceValid(List<IToken> tokensToInsert, int minimumSequenceSize, int maximumSequenceSize)
        {
            if (!IsTokenSequenceValid(tokensToInsert, minimumSequenceSize, maximumSequenceSize))
            {
                // Pending: refine message.
                throw new RummyException("Not valid token sequence to add in RummyLadder structure: " + tokensToInsert);
            }

            if (!this.Tokens.First().Color.Equals(tokensToInsert.First().Color))
            {
                return false;
            }
            return true;
        }

        private static bool IsTokenSequenceValid(List<IToken> tokens, int minimumSequenceSize, int maximumSequenceSize)
        {
            // At least, 3 tokens are needed to perform a ladder.
            if (tokens.Count < minimumSequenceSize || tokens.Count > maximumSequenceSize)
            {
                return false;
            }

            // All the elements have the same color.
            Color color = tokens.First().Color;
            if (!tokens.All(token => token.Color.Equals(color)))
            {
                return false;
            }

            // All the elements are correlative.
            int number = tokens.First().Number;
            if (tokens.Any(token => token.Number != number++))
            {
                return false;
            }

            return true;
        }
    }
}
