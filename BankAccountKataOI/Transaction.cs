using System;

namespace BankAccountKataOI
{
    public abstract class Transaction
    {
        public decimal Amount { get; protected set; }
        public DateTime Date { get; protected set; }
    }
}