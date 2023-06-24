using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public abstract class Goal
{
    public string Name { get; set; }
    public int Points { get; set; }
    public bool IsComplete { get; set; }

    public abstract void RecordEvent();
}

public class SimpleGoal : Goal
{
    public override void RecordEvent()
    {
        IsComplete = true;
    }
}

public class EternalGoal : Goal
{
    public override void RecordEvent()
    {
        // Do nothing
    }
}   

public class ChecklistGoal : Goal
{
    public int TimesCompleted { get; set; }
    public int TargetTimes { get; set; }
    public int BonusPoints { get; set; }

    public override void RecordEvent()
    {
        TimesCompleted++;
        if (TimesCompleted == TargetTimes)
        {
            IsComplete = true;
            Points += BonusPoints;
        }
    }
}
//THE PROGRAM SAVE THE FILE WHEN CHOOSE EXIT OPTION
//AND LOAD WHEN START THE PROGRAM
public class Program
{
    private static List<Goal> goals = new List<Goal>();
    private static int score = 0;

    public static void Main()
    {
        LoadData();

        while (true)
        {
            Console.WriteLine("Current score: " + score);
            Console.WriteLine("Goals:");
            foreach (var goal in goals)
            {
                Console.Write("[" + (goal.IsComplete ? "X" : " ") + "] " + goal.Name);
                if (goal is ChecklistGoal checklistGoal)
                {
                    Console.Write(" (" + checklistGoal.TimesCompleted + "/" + checklistGoal.TargetTimes + ")");
                }
                Console.WriteLine();
            }

            Console.WriteLine();
            Console.WriteLine("1. Record event");
            Console.WriteLine("2. Add new goal");
            Console.WriteLine("3. Exit");
            Console.Write("Enter choice: ");
            var choice = Console.ReadLine();

            if (choice == "1")
            {
                RecordEvent();
            }
            else if (choice == "2")
            {
                AddNewGoal();
            }
            else if (choice == "3")
            {
                break;
            }

            SaveData();
        }
    }

    private static void RecordEvent()
    {
        Console.Write("Enter goal name: ");
        var name = Console.ReadLine();

        var goal = goals.Find(g => g.Name == name);
        if (goal == null)
        {
            Console.WriteLine("Goal not found");
            return;
        }

        goal.RecordEvent();
        score += goal.Points;
    }

    private static void AddNewGoal()
    {
        Console.Write("Enter goal name: ");
        var name = Console.ReadLine();

        Console.Write("Enter goal type (1. Simple, 2. Eternal, 3. Checklist): ");
        var type = Console.ReadLine();

        Goal goal;
        if (type == "1")
        {
            goal = new SimpleGoal();
        }
        else if (type == "2")
        {
            goal = new EternalGoal();
        }
        else if (type == "3")
        {
            goal = new ChecklistGoal();
            
            Console.Write("Enter target times: ");
            var targetTimes = int.Parse(Console.ReadLine());
            
            ((ChecklistGoal)goal).TargetTimes = targetTimes;
            
            Console.Write("Enter bonus points: ");
            var bonusPoints = int.Parse(Console.ReadLine());
            
            ((ChecklistGoal)goal).BonusPoints = bonusPoints;
        }
        else
        {
            Console.WriteLine("Invalid type");
            return;
        }

        goal.Name = name;

        Console.Write("Enter points: ");
        var points = int.Parse(Console.ReadLine());

        goal.Points = points;

        goals.Add(goal);
    }

    private static void LoadData()
    {
        if (!File.Exists("goals.txt"))
        {
            return;
        }

        var lines = File.ReadAllLines("goals.txt");

        score = int.Parse(lines[0]);

        for (int i = 1; i < lines.Length; i++)
        {
            var parts = lines[i].Split(',');

            Goal goal;
            if (parts[0] == "Simple")
            {
                goal = new SimpleGoal();
                ((SimpleGoal)goal).IsComplete = bool.Parse(parts[4]);
                
                goals.Add(goal);
                
                continue;
                }
            else if (parts[0] == "Eternal")
            {
                goal = new EternalGoal();
            }
            else if (parts[0] == "Checklist")
            {
                goal = new ChecklistGoal();
                ((ChecklistGoal)goal).TimesCompleted = int.Parse(parts[4]);
                ((ChecklistGoal)goal).TargetTimes = int.Parse(parts[5]);
                ((ChecklistGoal)goal).BonusPoints = int.Parse(parts[6]);
            }
            else
            {
                continue;
            }

            goal.Name = parts[1];
            goal.Points = int.Parse(parts[2]);
            goal.IsComplete = bool.Parse(parts[3]);

            goals.Add(goal);
        }
    }

    private static void SaveData()
    {
        var lines = new List<string>();

        lines.Add(score.ToString());

        foreach (var goal in goals)
        {
            if (goal is SimpleGoal)
            {
                lines.Add("Simple," + goal.Name + "," + goal.Points + "," + goal.IsComplete + "," + ((SimpleGoal)goal).IsComplete);
            }
            else if (goal is EternalGoal)
            {
                lines.Add("Eternal," + goal.Name + "," + goal.Points + "," + goal.IsComplete);
            }
            else if (goal is ChecklistGoal)
            {
                lines.Add("Checklist," + goal.Name + "," + goal.Points + "," + goal.IsComplete + "," + ((ChecklistGoal)goal).TimesCompleted + "," + ((ChecklistGoal)goal).TargetTimes + "," + ((ChecklistGoal)goal).BonusPoints);
            }
        }

        File.WriteAllLines("goals.txt", lines);
    }
}
//kevin lizzari