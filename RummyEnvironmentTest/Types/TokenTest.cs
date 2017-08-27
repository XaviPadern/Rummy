using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RummyEnvironment;
using System.Drawing;
using Moq;
using TestHelpers;

namespace RummyEnvironmentTest
{
    [TestClass]
    public class TokenTest
    {
        private MockRepository mockRepository;
        private Mock<IToken> tokenMock;

        [TestInitialize]
        public void TestInitialize()
        {
            this.mockRepository = new MockRepository(MockBehavior.Strict);
            this.tokenMock = this.mockRepository.Create<IToken>();
        }

        [TestMethod]
        public void Token_ValidParameters_Created()
        {
            Token testToken = new Token(Color.Blue, 1);
            Guid outId;

            Assert.IsTrue(Guid.TryParse(testToken.Id.ToString(), out outId));
            Assert.IsTrue(testToken.Color.Equals(Color.Blue));
            Assert.AreEqual(1, testToken.Number);
        }

        [TestMethod]
        public void Token_InvalidColor_ExceptionThrow()
        {
            AssertHelpers.AssertThrows<RummyException>(() => 
                new Token(Color.Green, 1));
        }

        [TestMethod]
        public void Token_InvalidNumber_ExceptionThrow()
        {
            AssertHelpers.AssertThrows<RummyException>(() =>
                new Token(Color.Blue, 14));
        }

        [TestMethod]
        public void Token_IsTokenValidValidData_ReturnsTrue()
        {
            this.SetupValidToken();

            Assert.IsTrue(Token.IsTokenValid(this.tokenMock.Object));
        }

        [TestMethod]
        public void Token_IsTokenValidInvalidId_ReturnsFalse()
        {
            this.SetupValidToken();
            this.tokenMock.SetupGet(token => token.Id).Returns(Guid.Empty);

            Assert.IsFalse(Token.IsTokenValid(tokenMock.Object));
        }

        [TestMethod]
        public void Token_IsTokenValidInvalidColor_ReturnsFalse()
        {
            this.SetupValidToken();
            this.tokenMock.Setup(token => token.Color).Returns(Color.Green);

            Assert.IsFalse(Token.IsTokenValid(tokenMock.Object));
        }

        [TestMethod]
        public void Token_IsTokenValidInvalidNumber_ReturnsFalse()
        {
            this.SetupValidToken();
            this.tokenMock.Setup(token => token.Number).Returns(14);

            Assert.IsFalse(Token.IsTokenValid(tokenMock.Object));
        }

        private void SetupValidToken()
        {
            this.tokenMock.SetupGet(token => token.Id).Returns(Guid.NewGuid());
            this.tokenMock.SetupGet(token => token.Color).Returns(Color.Blue);
            this.tokenMock.SetupGet(token => token.Number).Returns(13);
        }
    }
}
