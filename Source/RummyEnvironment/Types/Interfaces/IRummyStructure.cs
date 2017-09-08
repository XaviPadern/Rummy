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

        IActionResult CanAdd(IToken tokenToInsert);

        IActionResult CanAdd(List<IToken> tokensToInsert);

        List<IOperationResult> Add(IToken tokenToInsert);

        List<IOperationResult> Add(List<IToken> tokensToInsert);

        IActionResult CanGet(IToken tokenToGet);

        IActionResult CanGet(List<IToken> tokensToGet);

        List<IOperationResult> Get(IToken tokenToGet);

        List<IOperationResult> Get(List<IToken> tokensToGet);
    }
}