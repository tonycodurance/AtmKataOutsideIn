using System;

namespace BankAccountKataOI
{
    public class Clock
    {
        public virtual DateTime GetCurrentDate()
        {
            return DateTime.UtcNow;
        }
    }
}