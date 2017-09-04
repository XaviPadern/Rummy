using System.Collections.Generic;

namespace RummyEnvironment
{
    public interface IPlayer
    {
        string Name { get; }
        List<IToken> Tokens { get; }
    }
}