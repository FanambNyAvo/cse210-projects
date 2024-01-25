using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static void Main()
    {
        Scripture scripture = new Scripture("John 3:16", "For God so loved the world, that he gave his only Son...");

        while (!scripture.AllWordsHidden)
        {
            Console.Clear();
            scripture.Display();
            Console.WriteLine("Press Enter to continue or type 'quit' to exit.");
            string input = Console.ReadLine();

            if (input.ToLower() == "quit")
                break;

            scripture.HideRandomWords();
        }

        Console.WriteLine("Program ended. Press any key to exit.");
        Console.ReadKey();
    }
}

class Scripture
{
    private readonly ScriptureReference reference;
    private readonly List<ScriptureWord> words;
    private readonly List<int> hiddenWordIndices;

    public bool AllWordsHidden => hiddenWordIndices.Count == words.Count;

    public Scripture(string reference, string text)
    {
        this.reference = new ScriptureReference(reference);
        this.words = text.Split(' ').Select(word => new ScriptureWord(word)).ToList();
        this.hiddenWordIndices = new List<int>();
    }

    public void Display()
    {
        Console.WriteLine($"{reference}: {GetHiddenText()}");
    }

    public void HideRandomWords()
    {
        int indexToHide = GetRandomWordIndexToHide();
        hiddenWordIndices.Add(indexToHide);
        words[indexToHide].Hide();
    }

    private int GetRandomWordIndexToHide()
    {
        List<int> availableIndices = Enumerable.Range(0, words.Count).Except(hiddenWordIndices).ToList();
        Random random = new Random();
        return availableIndices[random.Next(0, availableIndices.Count)];
    }

    private string GetHiddenText()
    {
        string hiddenText = "";
        for (int i = 0; i < words.Count; i++)
        {
            if (hiddenWordIndices.Contains(i))
                hiddenText += words[i].GetHiddenRepresentation() + " ";
            else
                hiddenText += words[i].Text + " ";
        }
        return hiddenText.Trim();
    }
}

class ScriptureReference
{
    public string Text { get; private set; }

    public ScriptureReference(string text)
    {
        Text = text;
    }
}

class ScriptureWord
{
    public string Text { get; private set; }
    public bool IsHidden { get; private set; }

    public ScriptureWord(string text)
    {
        Text = text;
        IsHidden = false;
    }

    public void Hide()
    {
        IsHidden = true;
    }

    public string GetHiddenRepresentation()
    {
        return new string('_', Text.Length);
    }
}
