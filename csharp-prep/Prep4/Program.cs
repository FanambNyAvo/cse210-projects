using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static void Main()
    {

        List<int> numbers = new List<int>();


        while (true)
        {
            Console.Write("Enter a number (type 0 when finished): ");
            int input = Convert.ToInt32(Console.ReadLine());

            if (input == 0)
            {
                break; // 
            }


            numbers.Add(input);
        }


        int sum = numbers.Sum();
        double average = numbers.Average();
        int maxNumber = numbers.Max();


        Console.WriteLine($"The sum is: {sum}");
        Console.WriteLine($"The average is: {average}");
        Console.WriteLine($"The largest number is: {maxNumber}");


        List<int> positiveNumbers = numbers.Where(num => num > 0).ToList();

        if (positiveNumbers.Count > 0)
        {
            int smallestPositive = positiveNumbers.Min();
            List<int> sortedList = numbers.OrderBy(num => num).ToList();

            Console.WriteLine($"The smallest positive number is: {smallestPositive}");
            Console.WriteLine("The sorted list is:");
            foreach (int num in sortedList)
            {
                Console.WriteLine(num);
            }
        }
        else
        {
            Console.WriteLine("No positive numbers entered.");
        }
    }
}
