using System.Collections.Generic;

namespace RummyEnvironment
{
    public interface ITokensLake
    {
        List<IToken> Tokens { get; }
        void CreateNew();
        void ShuffleTokensList();
        List<IToken> GrabToken();
        List<IToken> GrabTokens(int numberOfTokens);
    }
}