using System;
using System.Collections.Generic;

namespace RummyEnvironment
{
    public interface IOperationResult
    {
        StructureChanges StructureChanges { get; set; }

        List<IToken> Tokens { get; set; }
    }
}