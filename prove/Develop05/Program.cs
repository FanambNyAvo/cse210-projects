using System;
using System.Collections.Generic;
using System.IO;

public abstract class Goal
{
    protected string name;
    protected bool completed;

    public Goal(string name)
    {
        this.name = name;
        completed = false;
    }

    public abstract int RecordEvent(); 
    public string DisplayStatus()
    {
        return completed ? "[X]" : "[ ]";
    }

    public string Name => name;
}

public class SimpleGoal : Goal
{
    private int points;

    public SimpleGoal(string name, int points) : base(name)
    {
        this.points = points;
    }

    public override int RecordEvent()
    {
        completed = true;
        return points;
    }
}

public class EternalGoal : Goal
{
    private int points;

    public EternalGoal(string name, int points) : base(name)
    {
        this.points = points;
    }

    public override int RecordEvent()
    {
        return points;
    }
}

public class ChecklistGoal : Goal
{
    private int pointsPerCompletion;
    private int targetCount;
    private int bonusPoints;
    private int completedCount;

    public ChecklistGoal(string name, int pointsPerCompletion, int targetCount, int bonusPoints) : base(name)
    {
        this.pointsPerCompletion = pointsPerCompletion;
        this.targetCount = targetCount;
        this.bonusPoints = bonusPoints;
        completedCount = 0;
    }

    public override int RecordEvent()
    {
        completedCount++;
        if (completedCount == targetCount)
        {
            completed = true;
            return pointsPerCompletion * targetCount + bonusPoints;
        }
        else
        {
            return pointsPerCompletion;
        }
    }

    public string ChecklistStatus()
    {
        return $"Completed {completedCount}/{targetCount} times";
    }
}

public class EternalQuest
{
    private List<Goal> goals;
    private int score;

    public EternalQuest()
    {
        goals = new List<Goal>();
        score = 0;
    }

    public void AddGoal(Goal goal)
    {
        goals.Add(goal);
    }

    public int RecordEvent(int index)
    {
        int pointsEarned = goals[index].RecordEvent();
        score += pointsEarned;
        return pointsEarned;
    }

    public void DisplayGoals()
    {
        for (int i = 0; i < goals.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {goals[i].Name} - {goals[i].DisplayStatus()}");
            if (goals[i] is ChecklistGoal)
            {
                Console.WriteLine($"   {((ChecklistGoal)goals[i]).ChecklistStatus()}");
            }
        }
    }

    public int Score => score;

    public void SaveProgress(string filename)
    {
        using (StreamWriter writer = new StreamWriter(filename))
        {
            foreach (Goal goal in goals)
            {
                writer.WriteLine(goal.GetType().Name); 
                writer.WriteLine(goal.Name);
                writer.WriteLine(goal.DisplayStatus()); 
            }
            writer.WriteLine(score);
        }
    }

    public static EternalQuest LoadProgress(string filename)
    {
        EternalQuest eternalQuest = new EternalQuest();
        using (StreamReader reader = new StreamReader(filename))
        {
            while (!reader.EndOfStream)
            {
                string type = reader.ReadLine(); 
                string name = reader.ReadLine(); 
                string status = reader.ReadLine(); 
                switch (type)
                {
                    case "SimpleGoal":
                        eternalQuest.AddGoal(new SimpleGoal(name, 0));
                        break;
                    case "EternalGoal":
                        eternalQuest.AddGoal(new EternalGoal(name, 0));
                        break;
                    case "ChecklistGoal":
                        eternalQuest.AddGoal(new ChecklistGoal(name, 0, 0, 0));
                        break;
                }
                if (status == "[X]")
                {
                    eternalQuest.RecordEvent(eternalQuest.goals.Count - 1); 
                }
            }
            eternalQuest.score = int.Parse(reader.ReadLine()); 
        }
        return eternalQuest;
    }
}

class Program
{
    static void Main(string[] args)
    {
        EternalQuest eternalQuest = new EternalQuest();

        Goal marathonGoal = new SimpleGoal("Run a Marathon", 1000);
        Goal scripturesGoal = new EternalGoal("Read Scriptures", 100);
        Goal templeGoal = new ChecklistGoal("Attend Temple", 50, 10, 500);

        eternalQuest.AddGoal(marathonGoal);
        eternalQuest.AddGoal(scripturesGoal);
        eternalQuest.AddGoal(templeGoal);

        eternalQuest.RecordEvent(0);
        eternalQuest.RecordEvent(1); 
        eternalQuest.RecordEvent(2); 
        eternalQuest.RecordEvent(2); 

        eternalQuest.DisplayGoals();
        Console.WriteLine("Score: " + eternalQuest.Score);
s
        eternalQuest.SaveProgress("progress.txt");

        // Load progress
        EternalQuest loadedEternalQuest = EternalQuest.LoadProgress("progress.txt");
        Console.WriteLine("\nLoaded goals and score:");
        loadedEternalQuest.DisplayGoals();
        Console.WriteLine("Loaded Score: " + loadedEternalQuest.Score);
    }
}
