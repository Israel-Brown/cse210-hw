using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

public class Scripture
{
    private Reference _reference;
    private List<Word> _words;

    public Scripture(Reference reference, string text)
    {
        _reference = reference;
        _words = text.Split(' ').Select(word => new Word(word)).ToList();
    }

public static List<Scripture> LoadFromFile(string filePath)
{
    var scriptures = new List<Scripture>();
    var lines = File.ReadAllLines(filePath);

    foreach (var line in lines)
    {
        // Skip empty lines
        if (string.IsNullOrWhiteSpace(line)) continue;

        // Split the line into reference and text
        var parts = line.Split(':');

        // Ensure the line contains exactly two parts
        if (parts.Length != 2)
        {
            Console.WriteLine($"Skipping malformed line: {line}");
            continue;
        }

        var reference = parts[0].Trim();
        var text = parts[1].Trim();

        // Create a Scripture object and add it to the list
        scriptures.Add(new Scripture(new Reference(reference), text));
    }

    return scriptures;
}


    public void HideRandomWords(int count)
    {
        Random random = new Random();
        var visibleWords = _words.Where(w => !w.IsHidden).ToList();
        int wordsToHide = Math.Min(count, visibleWords.Count);

        for (int i = 0; i < wordsToHide; i++)
        {
            var word = visibleWords[random.Next(visibleWords.Count)];
            word.Hide();
            visibleWords.Remove(word);
        }
    }

    public void RevealHint()
    {
        var hiddenWords = _words.Where(w => w.IsHidden).ToList();
        if (hiddenWords.Count > 0)
        {
            Random random = new Random();
            var wordToReveal = hiddenWords[random.Next(hiddenWords.Count)];
            wordToReveal.RevealFirstLetter();
        }
    }

    public bool IsCompletelyHidden()
    {
        return _words.All(w => w.IsHidden);
    }

    public double GetProgress()
    {
        double hiddenCount = _words.Count(w => w.IsHidden);
        double totalWords = _words.Count;
        return (hiddenCount / totalWords) * 100;
    }

    public override string ToString()
    {
        string scriptureText = string.Join(" ", _words);
        return $"{_reference}\n{scriptureText}";
    }
}
