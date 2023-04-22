using System;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("What is your grade percentage? ");
        string answer = Console.ReadLine();
        int percent =  int.Parse(answer);
        string grade = "";
        if (percent>=90)
        {
            grade = "A";
        }
        else if (percent >=80)
        {
            grade = "B";
        }
        else if (percent >=70)
        {
            grade = "C";
        }
        else if (percent >=60)
        {
            grade = "D";
        }
        else if (percent <60)
        {
            grade = "F";
        }

        Console.WriteLine($"Your grade is: {grade}");
        if(percent>=70)
        {
            Console.WriteLine("You passed! Keep it up!");
        }
        else
        {
            Console.WriteLine("You Failed, next time will be better!");
        }
    }
}