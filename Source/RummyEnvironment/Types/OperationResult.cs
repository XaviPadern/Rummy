using System;
using System.Collections.Generic;

namespace RummyEnvironment
{
    public enum StructureChanges
    {
        None,
        Retrieving,
        SpareTokens,
        Created
    }

    public class OperationResult : IOperationResult
    {
        public StructureChanges StructureChanges { get; set; }

        public List<IToken> Tokens { get; set; }

        public OperationResult()
        {
            this.StructureChanges = StructureChanges.None;
            this.Tokens = new List<IToken>();
        }

        public OperationResult(StructureChanges structureChanges, List<IToken> tokens)
        {
            this.StructureChanges = structureChanges;
            this.Tokens = tokens;
        }
    }
}
