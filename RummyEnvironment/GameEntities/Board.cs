using System.Collections.Generic;

namespace RummyEnvironment
{
    public class Board
    {
        public List<RummyStructure> Structures { get; }

        public Board(List<RummyStructure> structures)
        {
            this.Structures = structures;
        }
    }
}
