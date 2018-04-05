namespace BankAccountKataOI
{
    public class Atm
    {
        private readonly TransactionRepository _transactionRepository;
        private readonly Output _output;
        private readonly Clock _clock;
        private readonly TransactionFormatter _transactionFormatter;

        public Atm(Output output, TransactionRepository transactionRepository, Clock clock, TransactionFormatter transactionFormatter)
        {
            _transactionRepository = transactionRepository;
            _clock = clock;
            _transactionFormatter = transactionFormatter;
            _output = output;
        }

        public void Deposit(decimal amount)
        {
            _transactionRepository.AddCredit(new Credit(amount, _clock.GetCurrentDate()));
        }

        public void Withdraw(decimal amount)
        {
            _transactionRepository.AddDebit(new Debit(amount, _clock.GetCurrentDate()));
        }

        public void PrintStatement()
        {
            _output.PrintHeader();
            
            foreach (var transaction in _transactionRepository.GetTransactions())
            {
                var balance = _transactionRepository.GetBalanceFor(transaction);
                var formattedTransaction = _transactionFormatter.Format(transaction, balance);
                
                _output.PrintLine(formattedTransaction);
            }
        }
    }
}