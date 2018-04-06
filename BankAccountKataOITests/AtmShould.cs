using System;
using System.Collections.Generic;
using BankAccountKataOI;
using Moq;
using NUnit.Framework;

namespace BankAccountKataOITests
{
    [TestFixture]
    public class AtmShould
    {
        private Mock<TransactionRepository> _transactionRepositoryMock;
        private Mock<Clock> _clockMock;
        private Atm _atm;

        private readonly DateTime _dateTime = new DateTime(2018, 03, 13);
        private Mock<StatementPrinter> _statementPrinterMock;

        [SetUp]
        public void Init()
        {
            _transactionRepositoryMock = new Mock<TransactionRepository>();
            _clockMock = new Mock<Clock>();
            _statementPrinterMock = new Mock<StatementPrinter>(new Mock<Output>().Object, new Mock<TransactionFormatter>().Object);
            
            _clockMock.Setup(c => c.GetCurrentDate()).Returns(_dateTime);
            
            _atm = new Atm(_transactionRepositoryMock.Object, _clockMock.Object, _statementPrinterMock.Object);
        }
        
        [Test]
        public void AddCreditCorrectly()
        {   
            _atm.Deposit(250);
            
            _transactionRepositoryMock
                .Verify(t => t.AddCredit(It.Is<Credit>(d => d.Amount == 250 && d.Date == _dateTime)), Times.Once());
        }
        
        [Test]
        public void AddDebitCorrectly()
        {   
            _atm.Withdraw(250);
            
            _transactionRepositoryMock.Verify(t => t.AddDebit(It.Is<Debit>(d => d.Amount == -250 && d.Date == _dateTime)), Times.Once());
        }
        
        [Test]
        public void PrintStatementCorrectly()
        {   
            var transactions = new List<Transaction>
            {
                new Credit(150, _dateTime),
                new Debit(50, _dateTime),
                new Credit(200, _dateTime)
            };
            _transactionRepositoryMock.Setup(t => t.GetTransactions()).Returns(transactions);
            
            _atm.PrintStatement();
            
            _statementPrinterMock.Verify(o => o.PrintHeader(), Times.Exactly(1));
            _statementPrinterMock.Verify(o => o.PrintBody(
                    It.Is<List<Transaction>>(
                        list => list[0].Amount == 150 && list[0].Date == _dateTime &&
                                list[1].Amount == -50 && list[1].Date == _dateTime &&
                                list[2].Amount == 200 && list[2].Date == _dateTime)
                ), Times.Once
            );
        }
    }
}