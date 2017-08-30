using System;
using System.Drawing;

namespace RummyEnvironment
{
    public interface IToken : ICloneable
    {
        Guid Id { get; }
        Color Color { get; }
        int Number { get; }
        bool IsEqual(IToken token);
        bool IsEquivalent(IToken token);
    }
}