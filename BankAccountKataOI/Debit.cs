using System;

namespace BankAccountKataOI
{
    public class Debit : Transaction
    {   
        public Debit(decimal amount, DateTime date)
        {
            Date = date;
            Amount = decimal.Negate(amount);
        }
    }
}