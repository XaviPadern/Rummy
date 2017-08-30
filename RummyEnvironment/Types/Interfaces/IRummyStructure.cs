using System;
using System.Collections.Generic;

namespace RummyEnvironment
{
    public interface IRummyStructure
    {
        Guid Id { get; }
        List<IToken> Tokens { get; }

        void Modify(List<IToken> tokens);

        bool IsValidTokenStructure(List<IToken> tokens);

        IAddingResult CanAddInStructure(IToken tokenToInsert);

        IAddingResult CanAddInStructure(List<IToken> tokensToInsert);

        List<IOperationResult> AddInStructure(IToken tokenToInsert);

        List<IOperationResult> AddInStructure(IToken tokenToInsert, List<IToken> extraTokens);

        List<IOperationResult> AddInStructure(List<IToken> tokensToInsert);

        List<IOperationResult> AddInStructure(List<IToken> tokensToInsert, List<IToken> extraTokens);
    }
}