using System;

public class RummyException : Exception
{
    public RummyException()
    {
    }

    public RummyException(string message)
        : base(message)
    {
    }

    public RummyException(string message, Exception inner)
        : base(message, inner)
    {
    }
}