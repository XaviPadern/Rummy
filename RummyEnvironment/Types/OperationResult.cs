using System;
using System.Collections.Generic;

namespace RummyEnvironment
{
    public enum StructureChanges
    {
        None,
        Retrieved,
        Modified,
        Created
    }

    public class OperationResult : IOperationResult
    {
        public StructureChanges StructureChanges { get; set; }

        public Guid StructureId { get; set; }

        public List<IToken> Tokens { get; set; }

        public OperationResult()
        {
            this.StructureChanges = StructureChanges.None;
            this.StructureId = Guid.Empty;
            this.Tokens = new List<IToken>();
        }

        public OperationResult(StructureChanges structureChanges, Guid id, List<IToken> tokens)
        {
            this.StructureChanges = structureChanges;
            this.StructureId = id;
            this.Tokens = tokens;
        }
    }
}
