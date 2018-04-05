using System;

namespace BankAccountKataOI
{
    public class TransactionFormatter
    {
        public virtual string Format(Transaction transaction, decimal balance)
        {
            var transactionTypeSection = "";
            if (transaction is Credit)
            {
                transactionTypeSection = $"|| {transaction.Amount} || ||";
            }

            if (transaction is Debit)
            {
                transactionTypeSection = $"|| || {Math.Abs(transaction.Amount)} ||";
            }

            return $"{transaction.Date:dd/MM/yy} {transactionTypeSection} {balance}";
        }
    }
}