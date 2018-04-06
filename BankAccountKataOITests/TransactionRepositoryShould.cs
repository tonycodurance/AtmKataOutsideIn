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
            var transactionRepository = new TransactionRepository();
            var transaction = new Credit(Amount, Date);
            
            transactionRepository.AddCredit(transaction);
            
            Assert.True(transactionRepository.GetTransactions().Contains(transaction));
        }
        
        [Test]
        public void AddDebit()
        {
            var transactionRepository = new TransactionRepository();
            var transaction = new Debit(Amount, Date);
            
            transactionRepository.AddCredit(new Credit(100, Date));
            transactionRepository.AddDebit(transaction);
            
            Assert.True(transactionRepository.GetTransactions().Contains(transaction));
        }
        
//        [Test]
//        public void GetBalanceCorrectlyForOneCreditTransaction()
//        {
//            var transactionRepository = new TransactionRepository();
//            var credit = new Credit(Amount, It.IsAny<DateTime>());
//            transactionRepository.AddCredit(credit);
//            
//            var balance = transactionRepository.GetBalanceFor(credit);
//            
//            Assert.AreEqual(balance, Amount);
//        }
//        
//        [Test]
//        public void GetBalanceCorrectlyForThreeCreditTransactions()
//        {
//            var transactionRepository = new TransactionRepository();
//            var credit1 = new Credit(Amount, new DateTime(2016, 03, 04));
//            var credit2 = new Credit(Amount, new DateTime(2016, 03, 05));
//            var credit3 = new Credit(Amount, new DateTime(2016, 03, 06));
//            transactionRepository.AddCredit(credit1);
//            transactionRepository.AddCredit(credit2);
//            transactionRepository.AddCredit(credit3);
//            var expectedBalance = 3*Amount;
//            
//            var balance = transactionRepository.GetBalanceFor(credit3);
//            
//            Assert.AreEqual(expectedBalance, balance);
//        }
//        
//        [Test]
//        public void GetBalanceCorrectlyForOneDebitTransaction()
//        {
//            var transactionRepository = new TransactionRepository();
//            var debit = new Debit(Amount, new DateTime(2016, 03, 04));
//            transactionRepository.AddCredit(new Credit(2*Amount, new DateTime(2014, 06, 06)));
//            transactionRepository.AddDebit(debit);
//
//            var expectedBalance = Amount;
//            
//            var balance = transactionRepository.GetBalanceFor(debit);
//            
//            Assert.AreEqual(expectedBalance, balance);
//        }
//        
//        [Test]
//        public void GetBalanceCorrectlyForAMixtureOfCreditsAndDebits()
//        {
//            var creditTransactionToFindBalanceFor = new Debit(Amount, new DateTime(2016, 03, 04));
//            var transactionRepository = new TransactionRepository();
//            transactionRepository.AddCredit(new Credit(4*Amount, new DateTime(2014, 06, 06)));
//            transactionRepository.AddDebit(new Debit(Amount, new DateTime(2016, 03, 04)));
//            transactionRepository.AddCredit(new Credit(Amount, new DateTime(2014, 06, 06)));
//            transactionRepository.AddDebit(creditTransactionToFindBalanceFor);
//            transactionRepository.AddCredit(new Credit(2*Amount, new DateTime(2014, 06, 06)));
//            transactionRepository.AddDebit(new Debit(Amount, new DateTime(2016, 03, 04)));
//
//            var expectedBalance = 3*Amount;
//            
//            var balance = transactionRepository.GetBalanceFor(creditTransactionToFindBalanceFor);
//            
//            Assert.AreEqual(expectedBalance, balance);
//        }
    }
}