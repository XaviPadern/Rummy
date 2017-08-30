using System.Collections.Generic;

namespace RummyEnvironment
{
    public interface IAddingResult
    {
        bool AdditionIsPossible { get; set; }

        List<IToken> NeededExtraTokens { get; set; }
    }
}