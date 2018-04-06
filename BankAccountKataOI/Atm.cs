namespace BankAccountKataOI
{
    public class Atm
    {
        private readonly TransactionRepository _transactionRepository;
        private readonly Clock _clock;
        private readonly StatementPrinter _statementPrinter;

        public Atm(TransactionRepository transactionRepository, Clock clock, StatementPrinter statementPrinter)
        {
            _transactionRepository = transactionRepository;
            _clock = clock;
            _statementPrinter = statementPrinter;
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
            _statementPrinter.PrintHeader();
            _statementPrinter.PrintBody(_transactionRepository.GetTransactions());
        }
    }
}