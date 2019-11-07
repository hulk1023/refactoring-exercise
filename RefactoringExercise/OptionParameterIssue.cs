using NUnit.Framework;
using System;

namespace RefactoringExercise
{
    [TestFixture]
    class OptionParameterIssue
    {
        static decimal GetItemPrice(string itemName, decimal relativeDiscount = 0)
        {

            if (relativeDiscount < 0 || relativeDiscount >= 1)
            {
                throw new ArgumentException("Incorrect discount.", "relativeDiscount");
            }

            if (relativeDiscount > 0)
            {
                Console.WriteLine("LOG Discount {0}% applied.", 100 * relativeDiscount);
            }

            return 100.0M * itemName.Length * (1.0M - relativeDiscount);

        }

        [Test]
        public void Test()
        {
            Console.WriteLine(GetItemPrice("laptop", .1M));
            Console.WriteLine(GetItemPrice("pen"));

        }
    }
}
