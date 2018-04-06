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
            _atm.Deposit(250);
            _atm.Withdraw(50);
            _atm.Deposit(150);

            _atm.PrintStatement();

            _outPutMock.Verify(o => o.PrintHeader(), Times.Once);
            _outPutMock.Verify(o => o.PrintLine("06/03/2016 || 250.00 || || 250.00"), Times.Once);
            _outPutMock.Verify(o => o.PrintLine("06/03/2016 || || 50.00 || 200.00"), Times.Once);
            _outPutMock.Verify(o => o.PrintLine("06/03/2016 || 150.00 || || 350.00"), Times.Once);
        }
    }
}