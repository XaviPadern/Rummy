using System.Collections.Generic;

namespace RummyEnvironment
{
    public interface IBoard
    {
        List<IRummyStructure> Structures { get; }
    }
}