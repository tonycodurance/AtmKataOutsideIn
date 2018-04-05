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
        private TransactionRepository _transactionRepository;
        private Atm _atm;
        private Output _output;
        private Clock _clock;

        private readonly DateTime _dateTime = new DateTime(2018, 03, 13);
        private Mock<TransactionFormatter> _transactionFormatterMock;
        private TransactionFormatter _transactionFormatter;

        [SetUp]
        public void Init()
        {
            _transactionRepositoryMock = new Mock<TransactionRepository>();
            _outPutMock = new Mock<Output>();
            _clockMock = new Mock<Clock>();
            _transactionFormatterMock = new Mock<TransactionFormatter>();
            _transactionRepository = _transactionRepositoryMock.Object;
            _output = _outPutMock.Object;
            _clock = _clockMock.Object;
            _transactionFormatter = _transactionFormatterMock.Object;
            
            _clockMock.Setup(c => c.GetCurrentDate()).Returns(_dateTime);
            
            _atm = new Atm(_output, _transactionRepository, _clock, _transactionFormatter);
        }
        
        [Test]
        public void AddCreditCorrectly()
        {   
            //Act
            _atm.Deposit(250);
            
            //Assert
            _transactionRepositoryMock
                .Verify(t => t.AddCredit(It.Is<Credit>(d => d.Amount == 250 && d.Date == _dateTime)), Times.Once());
        }
        
        [Test]
        public void AddDebitCorrectly()
        {   
            //Act
            _atm.Withdraw(250);
            
            //Assert
            _transactionRepositoryMock.Verify(t => t.AddDebit(It.Is<Debit>(d => d.Amount == -250 && d.Date == _dateTime)), Times.Once());
        }
        
        [Test]
        public void PrintStatementCorrectly()
        {   
            //Arrange
            var transactions = new List<Transaction>
            {
                new Credit(150, _dateTime),
                new Debit(50, _dateTime),
                new Credit(200, _dateTime)
            };
            _transactionRepositoryMock.Setup(t => t.GetTransactions()).Returns(transactions);
            _transactionFormatterMock.Setup(tf => tf.Format(It.IsAny<Transaction>(), It.IsAny<decimal>()))
                .Returns("FormattedTransaction");
            
            //Act
            _atm.PrintStatement();
            
            //Assert            
            _outPutMock.Verify(o => o.PrintLine("FormattedTransaction"), Times.Exactly(3));
        }
    }
}