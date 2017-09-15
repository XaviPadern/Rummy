using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RummyEnvironment;
using System.Drawing;
using System.Linq;
using TestHelpers;

namespace RummyEnvironmentTest
{
    [TestClass]
    public class Combination1_Test
    {
        [TestMethod]
        public void RummyIntegration_Combination1_Succeed()
        {
            // Define player.
            Player player = new Player("Coco", new List<IToken>()
            {
                new Token(Color.Black, 2),
                new Token(Color.Black, 3)
            });

            // Define structures.
            
            // Define board.
        }
    }
}
