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
        private Mock<Output> _outPutMock;
        private Mock<Clock> _clockMock;
        private Atm _atm;

        private readonly DateTime _dateTime = new DateTime(2018, 03, 13);
        private Mock<TransactionFormatter> _transactionFormatterMock;

        [SetUp]
        public void Init()
        {
            _transactionRepositoryMock = new Mock<TransactionRepository>();
            _outPutMock = new Mock<Output>();
            _clockMock = new Mock<Clock>();
            _transactionFormatterMock = new Mock<TransactionFormatter>();
            
            _clockMock.Setup(c => c.GetCurrentDate()).Returns(_dateTime);
            
            _atm = new Atm(_outPutMock.Object, _transactionRepositoryMock.Object, _clockMock.Object, _transactionFormatterMock.Object);
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
            _transactionFormatterMock.Setup(tf => tf.Format(It.IsAny<Transaction>(), It.IsAny<decimal>()))
                .Returns("FormattedTransaction");
            
            _atm.PrintStatement();
            
            _outPutMock.Verify(o => o.PrintLine("FormattedTransaction"), Times.Exactly(3));
        }
    }
}