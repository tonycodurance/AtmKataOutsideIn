using System.Collections.Generic;

namespace BankAccountKataOI
{
    public class TransactionRepository
    {
        private readonly List<Transaction> _transactions;

        public TransactionRepository()
        {
            _transactions = new List<Transaction>();
        }
        
        public virtual void AddCredit(Credit credit)
        {
            _transactions.Add(credit);
        }
        
        public virtual void AddDebit(Debit debit)
        {
            _transactions.Add(debit);
        }

        public virtual List<Transaction> GetTransactions()
        {
            return _transactions;
        }

        public virtual decimal GetBalanceFor(Transaction transaction)
        {
            var transactionIndex = _transactions.FindIndex(t => t == transaction);

            decimal balance = 0;
            for (var i = 0; i <= transactionIndex; i++)
            {
                balance += _transactions[i].Amount;
            }
            
            return balance;
        }
    }
}