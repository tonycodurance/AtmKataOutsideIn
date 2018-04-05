using System;

namespace BankAccountKataOI
{
    public class Credit : Transaction
    {
        public Credit(decimal amount, DateTime date)
        {
            Date = date;
            Amount = amount;
        }
    }
}