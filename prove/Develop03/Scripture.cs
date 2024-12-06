using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ScriptureApp
{
    public class Scripture
    {
        private List<Word> _words;
        private string _reference;

        public Scripture(string reference, string text)
        {
            _reference = reference;
            _words = text.Split(' ').Select(wordText => new Word(wordText)).ToList();
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

        public void RevealHint()
        {
            foreach (var word in _words.Where(word => word.IsHidden))
            {
                Console.WriteLine($"Hint: The first letter of the word is {word.Text[0]}.");
                break;
            }
        }

        public static Scripture LoadFromFile(string filePath)
        {
            var scriptures = new List<Scripture>();
            foreach (var line in File.ReadLines(filePath))
            {
                var parts = line.Split('|');
                scriptures.Add(new Scripture(parts[0], parts[1]));
            }

            Random rand = new Random();
            return scriptures[rand.Next(scriptures.Count)];
        }
    }
}
