using System;

class Program
{
    static void Main(string[] args)
    {
        DisplayWelcomeMessage();
        string name = request_name();
        int number = request_number();
        int squared_num = square_number(number);
        DisplayResult(name, squared_num);
    }
    static void DisplayWelcomeMessage()
    {
        Console.WriteLine("Welcome to the program!");
    }
    static string request_name()
    {
        Console.Write("Please enter your name: ");
        string Name=Console.ReadLine();
        return Name;
    }
    static int request_number()
    {
        Console.Write("Please enter your favorite number: ");
        int Number = int.Parse(Console.ReadLine());
        return Number;
    }
    static int square_number(int Number)
    {
        int square=Number*Number;
        return square;
    }
    static void DisplayResult(string Name, int square)
    {
         Console.WriteLine($"The {Name}, the square of your number is {square}");
    }
}


