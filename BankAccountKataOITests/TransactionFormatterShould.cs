using System;
using System.Collections;
using BankAccountKataOI;
using NUnit.Framework;

namespace BankAccountKataOITests
{
    [TestFixture]
    public class TransactionFormatterShould
    {
        private readonly TransactionFormatter _transactionFormatter = new TransactionFormatter();

        [TestCaseSource(nameof(TestCases))]
        public string FormatTransactionCorrectly(Transaction transaction, decimal balance)
        {
            return _transactionFormatter.Format(transaction, balance);
        }

        private static IEnumerable TestCases()
        {
            yield return new TestCaseData(new Credit(200m, new DateTime(2015, 07, 10)), 300m).Returns(
                "10/07/2015 || 200.00 || || 300.00");
            yield return new TestCaseData(new Debit(100m, new DateTime(2020, 03, 12)), 400m).Returns(
                "12/03/2020 || || 100.00 || 400.00");
        }
    }
}