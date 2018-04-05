using System;
using BankAccountKataOI;
using Moq;
using NUnit.Framework;

namespace BankAccountKataOITests
{
    [TestFixture]
    public class TransactionRepositoryShould
    {
        private const decimal Amount = 50;
        private readonly DateTime Date = new DateTime(2016, 03, 20);
        
        [Test]
        public void AddCredit()
        {
            //Arrange
            var transactionRepository = new TransactionRepository();
            var transaction = new Credit(Amount, Date);
            
            //Act
            transactionRepository.AddCredit(transaction);
            
            //Assert
            Assert.True(transactionRepository.GetTransactions().Contains(transaction));
        }
        
        [Test]
        public void AddDebit()
        {
            //Arrange
            var transactionRepository = new TransactionRepository();
            var transaction = new Debit(Amount, Date);
            
            //Act
            transactionRepository.AddCredit(new Credit(100, Date));
            transactionRepository.AddDebit(transaction);
            
            //Assert
            Assert.True(transactionRepository.GetTransactions().Contains(transaction));
        }
        
        [Test]
        public void GetBalanceCorrectlyForOneCreditTransaction()
        {
            //Arrange
            var transactionRepository = new TransactionRepository();
            var credit = new Credit(Amount, It.IsAny<DateTime>());
            transactionRepository.AddCredit(credit);
            
            //Act
            var balance = transactionRepository.GetBalanceFor(credit);
            
            //Assert
            Assert.AreEqual(balance, Amount);
        }
        
        [Test]
        public void GetBalanceCorrectlyForThreeCreditTransactions()
        {
            //Arrange
            var transactionRepository = new TransactionRepository();
            var credit1 = new Credit(Amount, new DateTime(2016, 03, 04));
            var credit2 = new Credit(Amount, new DateTime(2016, 03, 05));
            var credit3 = new Credit(Amount, new DateTime(2016, 03, 06));
            transactionRepository.AddCredit(credit1);
            transactionRepository.AddCredit(credit2);
            transactionRepository.AddCredit(credit3);

            var expectedBalance = 3*Amount;
            
            //Act
            var balance = transactionRepository.GetBalanceFor(credit3);
            
            //Assert
            Assert.AreEqual(expectedBalance, balance);
        }
        
        [Test]
        public void GetBalanceCorrectlyForOneDebitTransaction()
        {
            //Arrange
            var transactionRepository = new TransactionRepository();
            var debit = new Debit(Amount, new DateTime(2016, 03, 04));
            transactionRepository.AddCredit(new Credit(2*Amount, new DateTime(2014, 06, 06)));
            transactionRepository.AddDebit(debit);

            var expectedBalance = Amount;
            
            //Act
            var balance = transactionRepository.GetBalanceFor(debit);
            
            //Assert
            Assert.AreEqual(expectedBalance, balance);
        }
        
        [Test]
        public void GetBalanceCorrectlyForAMixtureOfCreditsAndDebits()
        {
            //Arrange
            var creditTransactionToFindBalanceFor = new Debit(Amount, new DateTime(2016, 03, 04));
            var transactionRepository = new TransactionRepository();
            transactionRepository.AddCredit(new Credit(4*Amount, new DateTime(2014, 06, 06)));
            transactionRepository.AddDebit(new Debit(Amount, new DateTime(2016, 03, 04)));
            transactionRepository.AddCredit(new Credit(Amount, new DateTime(2014, 06, 06)));
            
            transactionRepository.AddDebit(creditTransactionToFindBalanceFor);
            
            transactionRepository.AddCredit(new Credit(2*Amount, new DateTime(2014, 06, 06)));
            transactionRepository.AddDebit(new Debit(Amount, new DateTime(2016, 03, 04)));

            var expectedBalance = 3*Amount;
            
            //Act
            var balance = transactionRepository.GetBalanceFor(creditTransactionToFindBalanceFor);
            
            //Assert
            Assert.AreEqual(expectedBalance, balance);
        }
    }
}