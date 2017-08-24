using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestHelpers
{
    public class TestHelpers
    {
        public static TException AssertThrows<TException>(Action action) where TException : Exception
        {
            try
            {
                action();
            }
            catch (TException ex)
            {
                return ex;
            }
            Assert.Fail("Expected exception was not thrown");

            return null;
        }
    }
}
