using System.Collections.Generic;

namespace RummyEnvironment
{
    public interface ITokensLake
    {
        List<IToken> Tokens { get; }
        void CreateNew();
        void ShuffleTokensList();
        List<IToken> GetToken();
        List<IToken> GrabTokens(int numberOfTokens);
    }
}