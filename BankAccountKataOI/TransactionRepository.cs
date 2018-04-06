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
    }
}