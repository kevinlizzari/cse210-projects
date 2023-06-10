using System;
using System.Collections.Generic;
using System.Threading;

namespace ActivityProgram
{
    class Program
    {
        static void Main(string[] args)
        {
            bool exit = false;

            while (!exit)
            {
                DisplayMenu();
                int choice = GetMenuChoice();

                switch (choice)
                {
                    case 1:
                        PerformBreathingActivity();
                        break;
                    case 2:
                        PerformReflectionActivity();
                        break;
                    case 3:
                        PerformListingActivity();
                        break;
                    case 4:
                        exit = true;
                        Console.WriteLine("Exiting program...");
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }

                Console.WriteLine();
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
                Console.Clear();
            }
        }

        static void DisplayMenu()
        {
            Console.WriteLine("========== Activity Program ==========");
            Console.WriteLine("1. Breathing Activity");
            Console.WriteLine("2. Reflection Activity");
            Console.WriteLine("3. Listing Activity");
            Console.WriteLine("4. Exit");
            Console.WriteLine("======================================");
        }

        static int GetMenuChoice()
        {
            Console.Write("Enter your choice: ");
            int choice;
            while (!int.TryParse(Console.ReadLine(), out choice))
            {
                Console.WriteLine("Invalid input. Please enter a valid choice.");
                Console.Write("Enter your choice: ");
            }
            return choice;
        }

        static void PerformBreathingActivity()
        {
            Console.WriteLine("========== Breathing Activity ==========");
            Console.WriteLine("This activity will help you relax by walking you through breathing in and out slowly.");
            int duration = GetActivityDuration();
            Console.WriteLine("Get ready to begin...");
            PauseSeconds(3);
            Console.WriteLine();

            DateTime startTime = DateTime.Now;
            DateTime endTime = startTime.AddSeconds(duration);

            while (DateTime.Now < endTime)
            {
                Console.WriteLine("Breathe in...");
                PauseSeconds(3);
                Console.WriteLine("Breathe out...");
                PauseSeconds(3);
            }

            Console.WriteLine();
            Console.WriteLine("Good job! You have completed the Breathing Activity.");
            DisplayDuration(startTime, DateTime.Now);
            PauseSeconds(3);
        }

        static void PerformReflectionActivity()
{
    Console.WriteLine("========== Reflection Activity ==========");
    Console.WriteLine("This activity will help you reflect on times in your life when you have shown strength and resilience.");
    int duration = GetActivityDuration();
    Console.WriteLine("Get ready to begin...");
    PauseSeconds(3);
    Console.WriteLine();

    DateTime startTime = DateTime.Now;
    DateTime endTime = startTime.AddSeconds(duration);

    List<string> prompts = new List<string>
    {
        "Think of a time when you stood up for someone else.",
        "Think of a time when you did something really difficult.",
        "Think of a time when you helped someone in need.",
        "Think of a time when you did something truly selfless."
    };

    Random random = new Random();

    while (DateTime.Now < endTime)
    {
        string prompt = prompts[random.Next(prompts.Count)];
        Console.WriteLine(prompt);
        PauseSeconds(3);
        Console.WriteLine();

        List<string> questions = new List<string>
        {
            "Why was this experience meaningful to you?",
            "Have you ever done anything like this before?",
            "How did you get started?",
            "How did you feel when it was complete?",
            "What made this time different than other times when you were not as successful?",
            "What is your favorite thing about this experience?",
            "What could you learn from this experience that applies to other situations?",
            "What did you learn about yourself through this experience?",
            "How can you keep this experience in mind in the future?"
        };

        foreach (string question in questions)
        {
            if (DateTime.Now >= endTime)
                break;

            Console.WriteLine(question);
            PauseSeconds(5);
            Console.WriteLine();
        }
    }

    Console.WriteLine("Good job! You have completed the Reflection Activity.");
    DisplayDuration(startTime, DateTime.Now);
    PauseSeconds(3);
}

        static void PerformListingActivity()
        {
            Console.WriteLine("========== Listing Activity ==========");
            Console.WriteLine("This activity will help you reflect on the good things in your life by having you list as many things as you can in a certain area.");
            int duration = GetActivityDuration();
            Console.WriteLine("Get ready to begin...");
            PauseSeconds(3);
            Console.WriteLine();

            DateTime startTime = DateTime.Now;
            DateTime endTime = startTime.AddSeconds(duration);

            List<string> prompts = new List<string>
            {
                "Who are people that you appreciate?",
                "What are personal strengths of yours?",
                "Who are people that you have helped this week?",
                "When have you felt the Holy Ghost this month?",
                "Who are some of your personal heroes?"
            };

            Random random = new Random();
            string prompt = prompts[random.Next(prompts.Count)];

            Console.WriteLine(prompt);
            Console.WriteLine("You have " + duration + " seconds to list as many items as you can.");
            PauseSeconds(3);
            Console.WriteLine();

            List<string> items = new List<string>();

            while (DateTime.Now < endTime)
            {
                Console.Write("Enter an item: ");
                string item = Console.ReadLine();
                items.Add(item);
            }

            Console.WriteLine();
            Console.WriteLine("Number of items entered: " + items.Count);
            Console.WriteLine();
            Console.WriteLine("Good job! You have completed the Listing Activity.");
            DisplayDuration(startTime, DateTime.Now);
            PauseSeconds(3);
        }

        static int GetActivityDuration()
        {
            Console.Write("Enter the duration of the activity (in seconds): ");
            int duration;
            while (!int.TryParse(Console.ReadLine(), out duration) || duration <= 0)
            {
                Console.WriteLine("Invalid duration. Please enter a positive integer value.");
                Console.Write("Enter the duration of the activity (in seconds): ");
            }
            return duration;
        }

        static void DisplayDuration(DateTime startTime, DateTime endTime)
        {
            TimeSpan duration = endTime - startTime;
            Console.WriteLine("Duration: " + Math.Round(duration.TotalSeconds) + " seconds");
            //round because sometimes the end countdown give me decimals
            PauseSeconds(3);
        }

        static void PauseSeconds(int seconds)
        {
            for (int i = 0; i < seconds; i++)
            {
                Console.Write(".");
                Thread.Sleep(1000);
                Console.Write("\b \b");
              // I tried to recreate the clock with | / \ like in the video but it give me some problems
            }
        }
    }
}
//Kevin Lizzari