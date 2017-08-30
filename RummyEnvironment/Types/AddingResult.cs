using System.Collections.Generic;

namespace RummyEnvironment
{
    public class AddingResult : IAddingResult
    {
        public bool AdditionIsPossible { get; set; }

        public List<IToken> NeededExtraTokens { get; set; }

        public AddingResult()
        {
            this.AdditionIsPossible = false;
            this.NeededExtraTokens = new List<IToken>();
        }

        public AddingResult(bool additionIsPossible, List<IToken> neededExtraTokens)
        {
            this.AdditionIsPossible = additionIsPossible;
            this.NeededExtraTokens = neededExtraTokens;
        }
    }
}
