using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public class Scripture
{
    private Reference _reference;
    private List<Word> _words;

    public Scripture(Reference reference, string text)
    {
        _reference = reference;
        _words = text.Split(' ').Select(word => new Word(word)).ToList();
    }

    public void Display()
    {
        Console.WriteLine($"{_reference.ToString()}");
        Console.WriteLine(string.Join(" ", _words.Select(word => word.ToString())));
    }

    public void HideRandomWords(int count)
    {
        // Get words that are not hidden yet
        var visibleWords = _words.Where(word => !word.IsHidden).ToList();

        if (visibleWords.Count == 0)
            return;

        // Randomly pick words to hide
        Random random = new Random();
        for (int i = 0; i < count && visibleWords.Count > 0; i++)
        {
            int index = random.Next(visibleWords.Count);
            visibleWords[index].Hide();
            visibleWords.RemoveAt(index); // Remove to avoid hiding the same word again
        }
    }

    public bool AllWordsHidden()
    {
        return _words.All(word => word.IsHidden);
    }

    public static List<Scripture> LoadFromFile(string filePath)
    {
        var scriptures = new List<Scripture>();

        if (!File.Exists(filePath))
        {
            Console.WriteLine($"Error: File not found at {filePath}");
            return scriptures;
        }

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
}
