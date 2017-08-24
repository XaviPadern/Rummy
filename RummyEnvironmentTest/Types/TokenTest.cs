using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RummyEnvironment;
using System.Drawing;

namespace RummyEnvironmentTest
{
    [TestClass]
    public class TokenTest
    {
        [TestMethod]
        public void Token_ValidParameters_Created()
        {
            Token testToken = new Token(Color.Blue, 1);
            Guid outId;

            Assert.IsTrue(Guid.TryParse(testToken.Id.ToString(), out outId));
        }

        [TestMethod]
        public void Token_InvalidColor_ExceptionThrow()
        {
            TestHelpers.TestHelpers.AssertThrows<RummyException>(() => 
                new Token(Color.Green, 1));
        }

        [TestMethod]
        public void Token_InvalidNumber_ExceptionThrow()
        {
            TestHelpers.TestHelpers.AssertThrows<RummyException>(() =>
                new Token(Color.Blue, 14));
        }
    }
}
