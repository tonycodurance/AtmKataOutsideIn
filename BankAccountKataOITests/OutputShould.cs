using System;
using System.IO;
using BankAccountKataOI;
using NUnit.Framework;

namespace BankAccountKataOITests
{
    [TestFixture]
    public class OutputShould
    {
        private readonly DateTime _dateTime = new DateTime(2016, 03, 16);
        
        [Test]
        public void PrintTransactionLineToConsoleCorrectly()
        {
            using (var stringWriter = new StringWriter())
            {
                //Arrange
                Console.SetOut(stringWriter);
                var output = new Output();
                var expected = $"{_dateTime:dd/MM/yyyy} || 150.00 || || 150.00\n";
                
                //Act
                output.PrintLine(expected);

                //Assert
                Assert.AreEqual(expected, stringWriter.ToString());
            }
        }

        [Test]
        public void PrintHeaderToConsoleCorrectly()
        {
            using (var stringWriter = new StringWriter())
            {
                //Arrange
                Console.SetOut(stringWriter);
                var output = new Output();
                const string header = "date || credit || debit || balance";
                const string expected = header + "\n";
                
                //Act
                output.PrintLine(header);

                //Assert
                Assert.AreEqual(expected, stringWriter.ToString());
            }
        }
    }
}