using NUnit.Framework;

namespace RefactoringExercise
{
    [TestFixture()]
    public class ReduceCodeComplexity1
    {

        static int CalculateControlDigit(long number)
        {

            int sum = 0;
            int pos = 1;

            do
            {

                int digit = (int)(number % 10);

                if (pos % 2 == 0)
                {
                    sum += 3 * digit;
                }
                else
                {
                    sum += digit;
                }

                pos++;

                number /= 10;
            }
            while (number > 0);

            int result = sum % 11;
            if (result == 10)
                result = 1;

            return result;

        }


        [Test]
        public void Test()
        {
            var result = CalculateControlDigit(82712476);
            Assert.AreEqual(8, result);
        }


    }

}
