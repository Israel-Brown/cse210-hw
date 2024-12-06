using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public class Scripture
{
    private List<Word> _words; // Stores the words of the scripture
    private Reference _reference; // Stores the reference (e.g., John 3:16)

    // Constructor to initialize Scripture with a reference and text
    public Scripture(Reference reference, string text)
    {
        _reference = reference;
        _words = text.Split(' ').Select(word => new Word(word)).ToList();
    }

    // Method to load scriptures from a file
    public static Scripture LoadFromFile(string filePath)
    {
        try
        {
            var lines = File.ReadAllLines(filePath);
            if (lines.Length < 2)
                throw new Exception("Invalid file format. Each scripture must have a reference and text.");

            // First line is the reference, second line is the scripture text
            string referenceLine = lines[0];
            string textLine = lines[1];

            // Handle single verse or range of verses
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
        catch (Exception ex)
        {
            throw new Exception($"Error loading scripture from file: {ex.Message}");
        }
    }

    // Hide a specified number of words
    public void HideWords(int numberOfWords)
    {
        var visibleWords = _words.Where(word => !word.IsHidden).ToList();
        if (visibleWords.Count == 0)
            return; // All words are already hidden

        Random random = new Random();
        for (int i = 0; i < numberOfWords && visibleWords.Count > 0; i++)
        {
            int index = random.Next(visibleWords.Count);
            visibleWords[index].IsHidden = true; // Hide the word
            visibleWords.RemoveAt(index); // Remove from visible words list
        }
    }

    // Reveal a specified number of hidden words
    public void RevealHint(int numberOfWords)
    {
        var hiddenWords = _words.Where(word => word.IsHidden).ToList();
        if (hiddenWords.Count == 0)
            return; // No hidden words left to reveal

        Random random = new Random();
        for (int i = 0; i < numberOfWords && hiddenWords.Count > 0; i++)
        {
            int index = random.Next(hiddenWords.Count);
            hiddenWords[index].IsHidden = false; // Reveal the word
            hiddenWords.RemoveAt(index); // Remove from hidden words list
        }
    }

    // Get the progress (percentage of hidden words)
    public int GetProgress()
    {
        int totalWords = _words.Count;
        int hiddenWords = _words.Count(word => word.IsHidden);

        return (int)((hiddenWords / (double)totalWords) * 100);
    }

    // Check if all words are completely hidden
    public bool IsCompletelyHidden()
    {
        return _words.All(word => word.IsHidden);
    }

    // String representation of the scripture
    public override string ToString()
    {
        return $"{_reference.ToString()} {string.Join(' ', _words)}";
    }
}
