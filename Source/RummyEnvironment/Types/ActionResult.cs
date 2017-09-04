using System.Collections.Generic;

namespace RummyEnvironment
{
    public class ActionResult : IActionResult
    {
        public bool ActionIsPossible { get; set; }

        public List<IToken> NeededExtraTokens { get; set; }

        public ActionResult()
        {
            this.ActionIsPossible = false;
            this.NeededExtraTokens = new List<IToken>();
        }

        public ActionResult(bool additionIsPossible, List<IToken> neededExtraTokens)
        {
            this.ActionIsPossible = additionIsPossible;
            this.NeededExtraTokens = neededExtraTokens;
        }
    }
}
