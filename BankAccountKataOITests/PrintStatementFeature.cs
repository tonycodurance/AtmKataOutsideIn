using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using BankAccountKataOI;
using Moq;
using NUnit.Framework;

namespace BankAccountKataOITests
{
    [TestFixture]
    public class PrintStatementFeature
    {
        private Mock<StatementPrinter> _statementPrinterMock;
        private Mock<Clock> _clockMock;
        private Atm _atm;

        [SetUp]
        public void Init()
        {
            _statementPrinterMock = new Mock<StatementPrinter>(new Mock<Output>().Object, new Mock<TransactionFormatter>().Object);
            _clockMock = new Mock<Clock>();
            
            _atm = new Atm(new TransactionRepository(), _clockMock.Object, _statementPrinterMock.Object);
        }
        
        [Test]
        public void PrintStatement()
        {
            var firstCreditDate = new DateTime(2016, 03, 06);
            var debitDate = new DateTime(2020, 10, 12);
            var secondCreditDate = new DateTime(2019, 05, 18);
            
            _clockMock.SetupSequence(c => c.GetCurrentDate())
                .Returns(firstCreditDate)
                .Returns(debitDate)
                .Returns(secondCreditDate);
            
            _atm.Deposit(250);
            _atm.Withdraw(50);
            _atm.Deposit(150);
            
            _atm.PrintStatement();
            
            _statementPrinterMock.Verify(o => o.PrintHeader(), Times.Once);
            
            _statementPrinterMock.Verify(o => o.PrintBody(
                    It.Is<List<Transaction>>(
                        list => list[0].Amount == 250 && list[0].Date == firstCreditDate &&
                                list[1].Amount == -50 && list[1].Date == debitDate &&
                                list[2].Amount == 150 && list[2].Date == secondCreditDate)
                ), Times.Once
            );
        }
    }
}