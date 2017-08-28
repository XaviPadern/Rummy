using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace RummyEnvironment
{
    public class RummyLadder : RummyStructure
    {
        public RummyLadder(List<IToken> tokens) : base(tokens)
        {
        }

        public override bool IsValidTokenStructure(List<IToken> tokens)
        {
            // At least, 2 tokens are needed to perform a ladder.
            if (tokens.Count < 2)
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
