using System;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Choose a number from 1 to 10");
        Random randomGenerator=new Random();
        int number=randomGenerator.Next(1,10);
        int guess = 0;
        while (guess != number)
        {
                Console.Write("What is your guess? ");
                guess = int.Parse(Console.ReadLine());
                if (number >guess )
                {
                    Console.WriteLine("Higher");
                }
                else if (number< guess)
                {
                    Console.WriteLine("Lower");
                }
                else if (number==guess) 
                {
                    Console.WriteLine("Awesome! You guessed it!!");
                }
        }
    }
}
