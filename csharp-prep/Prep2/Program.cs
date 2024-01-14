using System;

class Program
{
    static void Main()
    {

        Console.Write("Enter your grade percentage: ");
        int gradePercentage = Convert.ToInt32(Console.ReadLine());


        string letter;
        if (gradePercentage >= 90)
        {
            letter = "A";
        }
        else if (gradePercentage >= 80)
        {
            letter = "B";
        }
        else if (gradePercentage >= 70)
        {
            letter = "C";
        }
        else if (gradePercentage >= 60)
        {
            letter = "D";
        }
        else
        {
            letter = "F";
        }


        if (gradePercentage >= 70)
        {
            Console.WriteLine($"Congratulations! You passed with a grade of {letter}.");
        }
        else
        {
            Console.WriteLine($"Sorry, you did not pass. Better luck next time!");
        }


        int lastDigit = gradePercentage % 10;
        string sign = "";

        if (lastDigit >= 7)
        {
            sign = "+";
        }
        else if (lastDigit < 3 && gradePercentage != 100)
        {
            sign = "-";
        }


        if (letter == "A" && sign == "+")
        {
            letter = "A-";
            sign = "";
        }
        else if (letter == "F" && (sign == "+" || sign == "-"))
        {
            letter = "F";
            sign = "";
        }


        Console.WriteLine($"Your final grade is {letter}{sign}");
    }
}
