using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RummyEnvironment;
using System.Linq;

namespace RummyEnvironmentTest
{
    [TestClass]
    public class BoardTest
    {
        [TestMethod]
        public void Board_NewEmptyBoard_TokensListIsEmpty()
        {
            Board board = new Board();
            Assert.IsNull(board.Structures);
        }

        [TestMethod]
        public void Board_NewBoardWithInitialStructures_StructuresAreValid()
        {
            // Pending.

            // Create 2 dummy structures .

            // Create a board with this structures.

            // Go over all the board structures and Assert are 2 and valid
            // (which internally asserts that its tokens are valid too).
        }
    }
}
