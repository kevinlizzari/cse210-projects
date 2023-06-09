using System;
using System.Collections.Generic;
using System.Threading;

namespace ActivityProgram
{
    abstract class Activity
    {
        public string Name { get; set; }
        public int Duration { get; set; }
        public Activity(string name, int duration)
        {
            Name = name;
            Duration = duration;
        }
        public abstract void Perform();
        protected void PauseSeconds(int seconds)
        {
            char[] clockChars = new char[] { '|', '/', '-', '\\' };  //animation
            int clockIndex = 0;

            for (int i = 0; i < seconds; i++)
            {
                Console.Write(clockChars[clockIndex]);
                Thread.Sleep(1000);
                Console.Write("\b \b");
                clockIndex = (clockIndex + 1) % clockChars.Length;
            }
        }
        protected void DisplayDuration(DateTime startTime, DateTime endTime)
        {
            TimeSpan duration = endTime - startTime;
            Console.WriteLine("Duration: " + Math.Round(duration.TotalSeconds) + " seconds");
            PauseSeconds(3);
        }
    }

    class BreathingActivity : Activity
    {
        public BreathingActivity(int duration) : base("Breathing", duration) { }

        public override void Perform()
        {
            Console.WriteLine("========== Breathing Activity ==========");
            Console.WriteLine("This activity will help you relax by walking you through breathing in and out slowly.");
            Console.WriteLine("Get ready to begin...");
            PauseSeconds(3);
            Console.WriteLine();

            DateTime startTime = DateTime.Now;
            DateTime endTime = startTime.AddSeconds(Duration);

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
    }

    class ReflectionActivity : Activity
    {
        public ReflectionActivity(int duration) : base("Reflection", duration) { }

        public override void Perform()
        {
            Console.WriteLine("========== Reflection Activity ==========");
            Console.WriteLine("This activity will help you reflect on times in your life when you have shown strength and resilience.");
            Console.WriteLine("Get ready to begin...");
            PauseSeconds(3);
            Console.WriteLine();

            DateTime startTime = DateTime.Now;
            DateTime endTime = startTime.AddSeconds(Duration);

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
                    PauseSeconds(3);
                    Console.WriteLine();
                }
            }

            Console.WriteLine("Good job! You have completed the Reflection Activity.");
            DisplayDuration(startTime, DateTime.Now);
            PauseSeconds(3);
        }
    }

    class ListingActivity : Activity
    {
        public ListingActivity(int duration) : base("Listing", duration) { }

        public override void Perform()
        {
            Console.WriteLine("========== Listing Activity ==========");
            Console.WriteLine("This activity will help you reflect on the good things in your life by having you list as many things as you can in a certain area.");
            Console.WriteLine("Get ready to begin...");
            PauseSeconds(3);
            Console.WriteLine();

            DateTime startTime = DateTime.Now;
            DateTime endTime = startTime.AddSeconds(Duration);

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
            Console.WriteLine("You have " + Duration + " seconds to list as many items as you can.");
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
    }

    class Program
    {
        private static Dictionary<string, int> activityLog = new Dictionary<string, int>();

        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Activity Program!");

            while (true)
            {
                DisplayMenu();
                string choice = Console.ReadLine();

                if (choice == "1")
                {
                    PerformActivity(new ReflectionActivity(GetActivityDuration()));
                }
                else if (choice == "2")
                {
                    PerformActivity(new BreathingActivity(GetActivityDuration()));
                }
                else if (choice == "3")
                {
                    PerformActivity(new ListingActivity(GetActivityDuration()));
                }
                else if (choice == "4")
                {
                    ShowActivityLog();
                }
                else if (choice == "5")
                {
                    Console.WriteLine("Exit!");
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid choice. Please try again.");
                }
                Console.WriteLine();
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
                Console.Clear();
            }
        }

        static void PerformActivity(Activity activity)
        {
            activity.Perform();
            UpdateActivityLog(activity.Name);
        }

        static void UpdateActivityLog(string activityName)
        {
            if (activityLog.ContainsKey(activityName))
            { 
                activityLog[activityName]++;
            }
            else
            {
                activityLog.Add(activityName, 1);
            }
        }

        static void ShowActivityLog()
        {
            Console.WriteLine("Activity Log:");

            foreach (var entry in activityLog)
            {
                Console.WriteLine($"{entry.Key}: {entry.Value} times");
            }
        }

        static void DisplayMenu()
        {
            Console.WriteLine("========== Activity Program ==========");
            Console.WriteLine("1. Reflection Activity");
            Console.WriteLine("2. Breathing Activity");
            Console.WriteLine("3. Listing Activity");
            Console.WriteLine("4. Show Activity Log");
            Console.WriteLine("5. Exit");
            Console.WriteLine("======================================");
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
    }
}