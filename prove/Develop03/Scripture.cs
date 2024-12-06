using System;
using System.Collections.Generic;
using System.Linq;

namespace ScriptureApp // Ensure the correct namespace
{
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
                word.Hide(); // Use the Hide method
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
                word.Reveal(); // Use the Reveal method
            }
        }

        public double GetProgress()
        {
            int hiddenCount = _words.Count(word => word.IsHidden);
            return (double)hiddenCount / _words.Count * 100; // Percentage of words hidden
        }

        public static Scripture LoadFromFile(string filePath)
        {
            // You can implement loading scripture from file here
            // For now, return a dummy example scripture for testing purposes
            return new Scripture("John 3:16", "For God so loved the world that he gave his only begotten Son.");
        }
    }
}
