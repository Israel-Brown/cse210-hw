using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public class Scripture
{
    private List<Word> _words;
    private string _reference;

    public Scripture(string reference, string text)
    {
        _reference = reference;
        _words = text.Split(' ')
                     .Select(wordText => new Word(wordText))
                     .ToList();
    }

    public void Display()
    {
        Console.WriteLine(_reference);
        Console.WriteLine(string.Join(" ", _words));
    }

    public bool IsCompletelyHidden()
    {
        return _words.All(word => word.IsHidden);
    }

    public void HideWords(int count)
    {
        Random random = new Random();
        var wordsToHide = _words.Where(word => !word.IsHidden)
                                .OrderBy(_ => random.Next())
                                .Take(count);

        foreach (var word in wordsToHide)
        {
            word.Hide();
        }
    }

    public void RevealWords(int count)
    {
        Random random = new Random();
        var wordsToReveal = _words.Where(word => word.IsHidden)
                                  .OrderBy(_ => random.Next())
                                  .Take(count);

        foreach (var word in wordsToReveal)
        {
            word.Reveal();
        }
    }

    public double GetProgress()
    {
        int hiddenCount = _words.Count(word => word.IsHidden);
        return (double)hiddenCount / _words.Count * 100;
    }

    public static Scripture LoadFromFile(string filePath)
    {
        if (!File.Exists(filePath))
            throw new FileNotFoundException("Scripture file not found.");

        var lines = File.ReadAllLines(filePath);
        if (lines.Length < 2)
            throw new InvalidOperationException("Invalid file format.");

        string reference = lines[0];
        string text = string.Join(" ", lines.Skip(1));
        return new Scripture(reference, text);
    }
}
