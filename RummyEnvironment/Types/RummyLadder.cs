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

        public override IAddingResult CanAddInStructure(List<IToken> tokensToInsert)
        {
            IAddingResult result = new AddingResult();

            if (!IsTokenSequenceValid(tokensToInsert, 1, RummyLadderMaximumSize - this.Tokens.Count))
            {
                // Pending: refine message.
                throw new RummyException("Not valid token sequence to add in RummyLadder structure: " + tokensToInsert);
            }

            if (!this.Tokens.First().Color.Equals(tokensToInsert.First().Color))
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
                result.AdditionIsPossible = true;
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
                result.AdditionIsPossible = true;
                result.NeededExtraTokens = new List<IToken>();
            }
            return result;
        }

        public override List<IOperationResult> AddInStructure(List<IToken> tokensToInsert, List<IToken> extraTokens)
        {
            IAddingResult result = this.CanAddInStructure(tokensToInsert);

            if (!result.AdditionIsPossible)
            {
                // Pending: Refine message.
                throw new RummyException("Validation exception while adding tokens to structure: " + tokensToInsert);
            }

            if (!TokenListsHelper.AreTokenListsEquivalentWithSameOrder(result.NeededExtraTokens, extraTokens))
            {
                // Pending: Refine message.
                throw new RummyException("Provided extra tokens are not the needed ones: " + tokensToInsert);
            }

            // For extremes
            if (extraTokens.Count < 1)
            {
                return this.PerformAddInStructure(tokensToInsert);
            }

            // Add the desired tokens + extra tokens.
            if (extraTokens.First().Number < tokensToInsert.First().Number)
            {
                tokensToInsert.InsertRange(0, extraTokens);
            }
            else
            {
                tokensToInsert.AddRange(extraTokens);
            }
            return this.PerformAddInStructure(tokensToInsert);
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
                //int indexInTokens = this.Tokens.IndexOf(tokensToInsert.First()) + 1;
                int indexInTokens = this.Tokens.FindIndex(Token => Token.Number == tokensToInsert.First().Number) + 1;

                int lengthToGet = this.Tokens.Count - indexInTokens;

                modificationOperation.Tokens.RemoveRange(indexInTokens, lengthToGet);
                
                List<IToken> tokensForNewStructure = new List<IToken> { tokensToInsert.First() };
                tokensForNewStructure.AddRange(this.Tokens.GetRange(indexInTokens, lengthToGet));

                creationOperation = new OperationResult(StructureChanges.Created, Guid.NewGuid(), tokensForNewStructure);

                if (tokensToInsert.Count == 2)
                {
                    modificationOperation.Tokens.Add(tokensToInsert.Last());
                }
            }

            // Add OperationResults.
            operationResults.Add(modificationOperation);
            if (creationOperation != null)
            {
                operationResults.Add(creationOperation);
            }
            return operationResults;
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
