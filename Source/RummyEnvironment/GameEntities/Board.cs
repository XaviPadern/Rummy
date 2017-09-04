using System.Collections.Generic;

namespace RummyEnvironment
{
    public class Board : IBoard
    {
        public List<IRummyStructure> Structures { get; }

        public Board(List<IRummyStructure> structures)
        {
            this.Structures = structures;
        }
        public Board()
        {
        }
    }
}
