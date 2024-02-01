using System;
using System.Threading;

class MindfulnessActivity
{
    private string _name;
    private string _description;
    protected int _duration; 

    public MindfulnessActivity(string name, string description)
    {
        _name = name;
        _description = description;
    }

    public void Start()
    {
        Console.WriteLine($"{_name} Activity - {_description}");
        SetDuration();
        Console.WriteLine($"Get ready to begin. Starting in 3 seconds...");
        Thread.Sleep(3000);
        PerformActivity();
        DisplayEndingMessage();
    }

    protected virtual void SetDuration()
    {
        Console.Write("Enter the duration in seconds: ");
        _duration = int.Parse(Console.ReadLine());
    }

    protected virtual void PerformActivity()
    {

    }

    protected void DisplayEndingMessage()
    {
        Console.WriteLine("Good job! You have completed the activity.");
        Console.WriteLine($"You spent {_duration} seconds on {_name} activity.");
        Thread.Sleep(3000);
    }
}

class BreathingActivity : MindfulnessActivity
{
    public BreathingActivity() : base("Breathing", "Helps you relax by guiding you through deep breathing.")
    {
    }

    protected override void PerformActivity()
    {
        for (int i = 0; i < _duration; i += 2)
        {
            Console.WriteLine("Breathe in...");
            Thread.Sleep(1000);
            Console.WriteLine("Breathe out...");
            Thread.Sleep(1000);
        }
    }
}

class ReflectionActivity : MindfulnessActivity
{
    private string[] _prompts = {
        "Think of a time when you stood up for someone else.",
        "Think of a time when you did something really difficult.",
        "Think of a time when you helped someone in need.",
        "Think of a time when you did something truly selfless."
    };

    public ReflectionActivity() : base("Reflection", "Helps you reflect on times when you showed strength and resilience.")
    {
    }

    protected override void PerformActivity()
    {
        Random random = new Random();

        for (int i = 0; i < _duration; i += 5)
        {
            string prompt = _prompts[random.Next(_prompts.Length)];
            Console.WriteLine(prompt);
            Thread.Sleep(2000);

            Console.WriteLine("Reflect on the following questions:");

            string[] questions = {
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

            foreach (var question in questions)
            {
                Console.WriteLine(question);
                Thread.Sleep(2000);
            }
        }
    }
}
class ListingActivity : MindfulnessActivity
{
    private string[] _prompts = {
        "Who are people that you appreciate?",
        "What are personal strengths of yours?",
        "Who are people that you have helped this week?",
        "When have you felt the Holy Ghost this month?",
        "Who are some of your personal heroes?"
    };

    public ListingActivity() : base("Listing", "Helps you reflect on the good things in your life by listing items.")
    {
    }

    protected override void PerformActivity()
    {
        Random random = new Random();
        string prompt = _prompts[random.Next(_prompts.Length)];

        Console.WriteLine($"{prompt} Starting in 3 seconds...");
        Thread.Sleep(3000);

        Console.WriteLine($"List as many items as you can in {_duration} seconds:");

        DateTime startTime = DateTime.Now;
        DateTime endTime = startTime.AddSeconds(_duration);

        int itemCount = 0;

        while (DateTime.Now < endTime)
        {
            string item = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(item))
