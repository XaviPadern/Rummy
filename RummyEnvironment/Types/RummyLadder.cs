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

        private enum ExtremeWhereFits
        {
            None,
            Initial,
            Final
        }

        public RummyLadder(List<IToken> tokens) : base(tokens)
        {
        }

        public override IActionResult CanAdd(List<IToken> tokensToInsert)
        {
            IActionResult result = new ActionResult();

            if (!IsTokensToInsertSequenceValid(tokensToInsert, 1, RummyLadderMaximumSize - this.Tokens.Count))
            {
                return result;
            }
            
            ExtremeWhereFits fitting = ExtremeWhereFits.None;

            if (tokensToInsert.Last().Number < this.Tokens.First().Number)
            {
                fitting = ExtremeWhereFits.Initial;
            }
            if (tokensToInsert.First().Number > this.Tokens.Last().Number)
            {
                fitting = ExtremeWhereFits.Final;
            }

            if (fitting != ExtremeWhereFits.None)
            {
                // Source can be located in the extremes.
                result.ActionIsPossible = true;
                result.NeededExtraTokens = this.GetExtraTokens(tokensToInsert, fitting);
                return result;
            }

            if (tokensToInsert.Count > 2)
            {
                return result;
            }

            int indexOnDestiny = this.Tokens.FindIndex(token => token.Number == tokensToInsert.First().Number);

            if (indexOnDestiny > 2 - tokensToInsert.Count && indexOnDestiny < this.Tokens.Count - 2)
            {
                // Source can be located inside the destiny (splitting).
                result.ActionIsPossible = true;
                result.NeededExtraTokens = new List<IToken>();
            }
            return result;
        }

        public override List<IOperationResult> Add(List<IToken> tokensToInsert, List<IToken> extraTokens)
        {
            IActionResult result = this.CanAdd(tokensToInsert);

            if (!result.ActionIsPossible)
            {
                // Pending: Refine message.
                throw new RummyException("Validation exception while adding tokens to structure: " + tokensToInsert);
            }

            if (!TokenListsHelper.AreTokenListsEquivalentWithSameOrder(result.NeededExtraTokens, extraTokens))
            {
                // Pending: Refine message.
                throw new RummyException("Provided extra tokens are not the needed ones: " + tokensToInsert);
            }

            // Not in the extremes
            if (extraTokens.Count > 0)
            {
                // Add the desired tokens + extra tokens.
                if (extraTokens.First().Number < tokensToInsert.First().Number)
                {
                    tokensToInsert.InsertRange(0, extraTokens);
                }
                else
                {
                    tokensToInsert.AddRange(extraTokens);
                }
                
            }
            return this.PerformAddInStructure(tokensToInsert);
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
            bool tokensAreInTheExtreme = this.Tokens.First().IsEquivalent(tokensToGet.First()) ||
                                         this.Tokens.Last().IsEquivalent(tokensToGet.Last());

            if (tokensAreInTheExtreme && tokensToGet.Count <= this.Tokens.Count - 3)
            {
                result.ActionIsPossible = true;
                return result;
            }

            List<List<IToken>> resultingLadderLists = this.GetResultingLadderLists(tokensToGet);
            List<IToken> tempLeftLadder = resultingLadderLists.First();
            List<IToken> tempRightLadder = resultingLadderLists.Last();

            // Calculate if there's any space for the extra tokens.
            if (tempLeftLadder.First().Number + tempLeftLadder.Count < 4 || tempRightLadder.Last().Number - tempRightLadder.Count > 10)
            {
                return result;
            }

            result.NeededExtraTokens = new List<IToken>();

            int numberToAdd = tempLeftLadder.First().Number - 1;

            // Structure modified: Ladder left.
            for (int i = tempLeftLadder.Count; i < 3; i++)
            {
                result.NeededExtraTokens.Add(new Token(this.Tokens.First().Color, numberToAdd--));
            }

            numberToAdd = tempRightLadder.Last().Number + 1;

            for (int i = tempRightLadder.Count; i < 3; i++)
            {
                result.NeededExtraTokens.Add(new Token(this.Tokens.First().Color, numberToAdd++));
            }

            result.ActionIsPossible = true;
            return result;
        }

        public override List<IOperationResult> Get(List<IToken> tokensToGet, List<IToken> extraTokens)
        {
            List<IOperationResult> operationResults = new List<IOperationResult>();

            IActionResult result = this.CanGet(tokensToGet);

            if (!result.ActionIsPossible)
            {
                // Pending: Refine message.
                throw new RummyException("Validation exception while getting tokens from structure: " + tokensToGet);
            }

            if (!TokenListsHelper.AreTokenListsEquivalentWithSameOrder(result.NeededExtraTokens, tokensToGet))
            {
                // Pending: Refine message.
                throw new RummyException("Provided extra tokens are not the needed ones: " + tokensToGet);
            }

            List<List<IToken>> resultingLadderLists = this.GetResultingLadderLists(tokensToGet);
            List<List<IToken>> extraTokenLists = this.SplitExtraTokensInLists(extraTokens);


            OperationResult modificationOperation = new OperationResult(StructureChanges.Modified, this.Id, this.Tokens.Clone().ToList());

            foreach (List<IToken> extraTokenList in extraTokenLists)
            {
                if ()
            }

            return null;
        }

        public override bool IsValidTokenStructure(List<IToken> tokens)
        {
            return IsTokenSequenceValid(tokens, RummyLadderMinimumSize, RummyLadderMaximumSize);
        }

        private List<IOperationResult> PerformAddInStructure(List<IToken> tokensToInsert)
        {
            List<IOperationResult> operationResults = new List<IOperationResult>();

            // Two actions are the outcome of adding a structure:
            // - A modification of the current structure (always).
            // - A creation of a new structure (only if new tokens are added in the middle of the current structure).

            OperationResult modificationOperation = new OperationResult(StructureChanges.Modified, this.Id, this.Tokens.Clone().ToList());
            OperationResult creationOperation = null;

            if (tokensToInsert.First().Number < this.Tokens.First().Number)
            {
                // Extreme left.
                modificationOperation.Tokens.InsertRange(0, tokensToInsert);
            }
            else if (tokensToInsert.First().Number > this.Tokens.Last().Number)
            {
                // Extreme right.
                modificationOperation.Tokens.AddRange(tokensToInsert);
            }
            else
            {
                // Middle. The tokensToInsert can have a length of 1 or 2.
                int indexInTokens = this.Tokens.FindIndex(token => token.Number == tokensToInsert.First().Number);
                int lengthToGet = this.Tokens.Count - indexInTokens;

                List<IToken> tokensForNewStructure = this.Tokens.GetRange(indexInTokens, lengthToGet);
                creationOperation = new OperationResult(StructureChanges.Created, Guid.NewGuid(), tokensForNewStructure);
                
                modificationOperation.Tokens.RemoveRange(indexInTokens, lengthToGet);
                modificationOperation.Tokens.AddRange(tokensToInsert);
            }

            // Add OperationResults.
            operationResults.Add(modificationOperation);
            if (creationOperation != null)
            {
                operationResults.Add(creationOperation);
            }
            return operationResults;
        }

        private List<List<IToken>> GetResultingLadderLists(List<IToken> tokensToGet)
        {
            int indexRightLadder = this.Tokens.FindIndex(token => token.IsEquivalent(tokensToGet.Last()));

            return new List<List<IToken>>()
            {
                this.Tokens.GetRange(0, this.Tokens.FindIndex(token => token.IsEquivalent(tokensToGet.First()))),
                this.Tokens.GetRange(indexRightLadder + 1, this.Tokens.Count - indexRightLadder - 1)
            };
        }
        
        private List<List<IToken>> SplitExtraTokensInLists(List<IToken> extraTokens)
        {
            List<List<IToken>> splittedTokens = new List<List<IToken>>();

            int lastNumber = 0;
            List<IToken> currentList = new List<IToken>();

            foreach (IToken token in extraTokens)
            {
                if (token.Number < lastNumber)
                {
                    splittedTokens.Add((List<IToken>)currentList.Clone());
                    currentList = new List<IToken>();
                }
                currentList.Add(token);
                lastNumber++;
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
            // At least, 2 tokens are needed to perform a ladder.
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
        
        private List<IToken> GetExtraTokens(List<IToken> tokensToFit, ExtremeWhereFits fitting)
        {
            List <IToken> extraTokens = new List<IToken>();
            Color extraTokensColor = this.Tokens.First().Color;
            int numberOfTokensToBuild;

            if (fitting == ExtremeWhereFits.Initial)
            {
                numberOfTokensToBuild = this.Tokens.First().Number - tokensToFit.Last().Number - 1;

                for (int i = 1; i <= numberOfTokensToBuild; i++)
                {
                    extraTokens.Add(
                        new Token(extraTokensColor, tokensToFit.Last().Number + i));
                }
            }
            else
            {
                numberOfTokensToBuild = tokensToFit.First().Number - this.Tokens.Last().Number - 1;

                for (int i = 1; i <= numberOfTokensToBuild; i++)
                {
                    extraTokens.Add(
                        new Token(extraTokensColor, this.Tokens.Last().Number + i));
                }
            }
            return extraTokens;
        }
    }
}
