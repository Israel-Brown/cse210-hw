using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public class Scripture
{
    private List<Word> _words;
    private Reference _reference;

    public Scripture(Reference reference, string text)
    {
        _reference = reference;
        _words = text.Split(' ').Select(word => new Word(word)).ToList();
    }

    public static Scripture LoadFromFile(string filePath)
    {
        var lines = File.ReadAllLines(filePath);
        if (lines.Length < 2)
            throw new Exception("Invalid file format. Each scripture must have a reference and text.");

        string referenceLine = lines[0];
        string textLine = lines[1];

        string[] referenceParts = referenceLine.Split(' ');
        string book = referenceParts[0];
        string[] verseParts = referenceParts[1].Split(':');
        int chapter = int.Parse(verseParts[0]);
        string[] verses = verseParts[1].Split('-');
        int startVerse = int.Parse(verses[0]);
        int? endVerse = verses.Length > 1 ? int.Parse(verses[1]) : null;

        Reference reference = new Reference(book, chapter, startVerse, endVerse);
        return new Scripture(reference, textLine);
    }

    public void HideWords(int numberOfWords)
    {
        var visibleWords = _words.Where(word => !word.IsHidden).ToList();
        if (visibleWords.Count == 0) return;

        Random random = new Random();
        for (int i = 0; i < numberOfWords && visibleWords.Count > 0; i++)
        {
            int index = random.Next(visibleWords.Count);
            visibleWords[index].IsHidden = true;
            visibleWords.RemoveAt(index);
        }
    }

    public void RevealHint(int numberOfWords)
    {
        var hiddenWords = _words.Where(word => word.IsHidden).ToList();
        if (hiddenWords.Count == 0) return;

        Random random = new Random();
        for (int i = 0; i < numberOfWords && hiddenWords.Count > 0; i++)
        {
            int index = random.Next(hiddenWords.Count);
            hiddenWords[index].IsHidden = false;
            hiddenWords.RemoveAt(index);
        }
    }

    public int GetProgress()
    {
        int totalWords = _words.Count;
        int hiddenWords = _words.Count(word => word.IsHidden);
        return (int)((hiddenWords / (double)totalWords) * 100);
    }

    public bool IsCompletelyHidden()
    {
        return _words.All(word => word.IsHidden);
    }

    public override string ToString()
    {
        return $"{_reference.ToString()} {string.Join(' ', _words)}";
    }
}
