using System;

class Program
{
    static void Main()
    {
        bool playAgain = true;

        while (playAgain)
        {

            Random random = new Random();
            int magicNumber = random.Next(1, 101);

            int guessCount = 0;


            while (true)
            {

                Console.Write("What is your guess? ");
                int userGuess = Convert.ToInt32(Console.ReadLine());


                guessCount++;


                if (userGuess == magicNumber)
                {
                    Console.WriteLine("You guessed it!");
                    break;
                }
                else if (userGuess < magicNumber)
                {
                    Console.WriteLine("Higher");
                }
                else
                {
                    Console.WriteLine("Lower");
                }
            }


            Console.WriteLine($"It took you {guessCount} guesses.");


            Console.Write("Do you want to play again? (yes/no): ");
            string playAgainInput = Console.ReadLine().ToLower();


            if (playAgainInput != "yes")
            {
                playAgain = false;
            }

            Console.Clear(); 
        }
    }
}
