using System.Collections.Generic;

namespace BankAccountKataOI
{
    public class StatementPrinter
    {
        private  readonly Output _output;
        private readonly TransactionFormatter _transactionFormatter;

        public StatementPrinter(Output output, TransactionFormatter transactionFormatter)
        {
            _output = output;
            _transactionFormatter = transactionFormatter;
        }
        
        public virtual void PrintHeader()
        {
            _output.PrintHeader("date || credit || debit || balance");
        }

        public virtual void PrintBody(List<Transaction> transactions)
        {
            foreach (var transaction in transactions)
            {
                var balance = GetBalanceFor(transaction, transactions);
                var formattedTransaction = _transactionFormatter.Format(transaction, balance);
                
                _output.PrintLine(formattedTransaction);
            }
        }

        private decimal GetBalanceFor(Transaction transaction, List<Transaction> transactions)
        {
            var transactionIndex = transactions.FindIndex(t => t == transaction);

            decimal balance = 0;
            for (var i = 0; i <= transactionIndex; i++)
            {
                balance += transactions[i].Amount;
            }
            
            return balance;
        }
    }
}