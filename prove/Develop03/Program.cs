using System;
using System.Diagnostics;
using ScriptureApp; 

namespace Develop03
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                string filePath = "scriptures.txt";
                Scripture scripture = Scripture.LoadFromFile(filePath);

                Console.WriteLine("Scripture Memorization Program");

                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Start();

                while (true)
                {
                    Console.Clear();
                    Console.WriteLine(scripture.ToString());

                    int progress = (int)scripture.GetProgress();
                    Console.WriteLine($"Progress: {progress}%");

                    if (scripture.IsCompletelyHidden())
                    {
                        stopwatch.Stop();
                        Console.WriteLine($"You have memorized the scripture in {stopwatch.ElapsedMilliseconds / 1000} seconds!");
                        break;
                    }

                    if (progress >= 75) Console.WriteLine("Great job! You're almost there!");
                    else if (progress >= 50) Console.WriteLine("You're doing well, keep it up!");
                    else Console.WriteLine("You're just getting started, stay focused!");

                    Console.WriteLine("Press 'H' to hide words, 'R' to reveal hints, or 'Q' to quit:");
                    char choice = char.ToUpper(Console.ReadKey(true).KeyChar);

                    switch (choice)
                    {
                        case 'H':
                            scripture.HideWords(3);
                            break;
                        case 'R':
                            scripture.RevealHint();
                            break;
                        case 'Q':
                            return;
                        default:
                            Console.WriteLine("Invalid choice. Please try again.");
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
    }
}
