using System;
using System.Collections.Generic;

namespace RummyEnvironment
{
    public interface IRummyStructure
    {
        Guid Id { get; }
        List<IToken> Tokens { get; }
    }
}