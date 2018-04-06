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
                transactionTypeSection = $"|| {transaction.Amount:F} || ||";
            }

            if (transaction is Debit)
            {
                transactionTypeSection = $"|| || {Math.Abs(transaction.Amount):F} ||";
            }

            return $"{transaction.Date:dd/MM/yyyy} {transactionTypeSection} {balance:F}";
        }
    }
}