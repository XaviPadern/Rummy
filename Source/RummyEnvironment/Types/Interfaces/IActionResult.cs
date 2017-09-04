using System.Collections.Generic;

namespace RummyEnvironment
{
    public interface IActionResult
    {
        bool ActionIsPossible { get; set; }

        List<IToken> NeededExtraTokens { get; set; }
    }
}