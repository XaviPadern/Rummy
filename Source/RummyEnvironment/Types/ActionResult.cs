using System.Collections.Generic;

namespace RummyEnvironment
{
    public class ActionResult : IActionResult
    {
        public bool ActionIsPossible { get; set; }

        public List<IToken> SpareTokens { get; set; }

        public ActionResult() : this(false, new List<IToken>())
        {
        }

        public ActionResult(bool additionIsPossible) : this(additionIsPossible, new List<IToken>())
        {
        }

        public ActionResult(bool additionIsPossible, List<IToken> spareTokens)
        {
            this.ActionIsPossible = additionIsPossible;
            this.SpareTokens = spareTokens;
        }
    }
}
