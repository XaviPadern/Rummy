using System;

namespace RummyEnvironment
{
    public interface IRummyStructure
    {
        Guid Id { get; }
        IToken[] Tokens { get; }
    }
}