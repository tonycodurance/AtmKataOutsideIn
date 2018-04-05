using System;
using BankAccountKataOI;
using Moq;
using NUnit.Framework;

namespace BankAccountKataOITests
{
    [TestFixture]
    public class PrintStatementFeature
    {
        private Mock<Output> _outPutMock;
        private Mock<Clock> _clockMock;
        private Atm _atm;

        [SetUp]
        public void Init()
        {
            _outPutMock = new Mock<Output>();
            _clockMock = new Mock<Clock>();

            _clockMock.Setup(c => c.GetCurrentDate()).Returns(new DateTime(2016, 03, 06));
            
            _atm = new Atm(_outPutMock.Object, new TransactionRepository(), _clockMock.Object, new TransactionFormatter());
        }
        
        [Test]
        public void PrintStatement()
        {
            //Arrange
            _atm.Deposit(250);
            _atm.Withdraw(50);
            _atm.Deposit(150);

            //Act
            _atm.PrintStatement();

            //Assert
            _outPutMock.Verify(o => o.PrintHeader(), Times.Once);
            _outPutMock.Verify(o => o.PrintLine("06/03/16 || 250 || || 250"), Times.Once);
            _outPutMock.Verify(o => o.PrintLine("06/03/16 || || 50 || 200"), Times.Once);
            _outPutMock.Verify(o => o.PrintLine("06/03/16 || 150 || || 350"), Times.Once);
        }
    }
}