using System;
using MathNet.Numerics.LinearAlgebra;

namespace harness
{
   /*
    *   PLEASE SEE THE README FILE FOR AN EXPLANATION.
    */
    
   class Program
    {
        static void Main(string[] args)
        {
            var salaries = new[] { 25.0, 50.0, 125.0 };

            var profit = 1000;
            var percentOfProfitBonus = 0;

            var bonusCalculations = CalculateBonuses(salaries, profit, percentOfProfitBonus);

            foreach (var item in bonusCalculations)
            {
                Console.WriteLine(item);
            }

        }

        private static double[] CalculateBonuses(double[] salaries, int profit, int percentOfProfitBonus)
        {
            if (salaries == null)
            {
                throw new ArgumentOutOfRangeException("Salaries can not be null.");
            }

            if (percentOfProfitBonus > 100 || percentOfProfitBonus < 0)
            {
                throw new ArgumentOutOfRangeException("Expected percentOfProfitBonus to be between the range of O and 100.");
            }

            /*
            * negative profit could potentially happen hense profit being a Signed integer (as opposed to an Unsigned Int).
            * In this case we shouldn't offer a bonus but we shouldn't have a case where the employees owe money to the company.
            * therefore we'll treat ignore losses but treat profit as 0 for this calculation.
            *
            * Zero profit Zero Bonus seems sensible.
            */
            if (profit < 0)
            {

                profit = 0;
            }

            if (salaries.Length < 1)
            {
                throw new ArgumentOutOfRangeException("At least one salary must be specified.");
            }

            /*
             * Vector Mathematics 😎👌, I think this makes the code much easier to read (just 5 lines of code).
             * It can also be quite efficient to solve this mathematically.
             */

            var totalBonus = profit / 100 * percentOfProfitBonus;
            var wagesVector = Vector<Double>.Build.Dense(salaries);
            var totalWageBudget = wagesVector.Sum();
            var wageBudgetPercentages = 100 / totalWageBudget * wagesVector;
            var bonusCalculations = totalBonus / 100 * wageBudgetPercentages;

            return bonusCalculations.ToArray();
        }
    }
}
