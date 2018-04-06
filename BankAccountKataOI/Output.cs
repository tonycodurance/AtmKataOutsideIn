using System;

namespace BankAccountKataOI
{
    public class Output
    {
        public virtual void PrintLine(string formattedTransaction)
        {
            Console.WriteLine(formattedTransaction);
        }

        public virtual void PrintHeader(string header)
        {
            Console.WriteLine(header);
        }
    }
}