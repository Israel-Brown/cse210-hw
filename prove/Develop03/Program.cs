using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        Console.Clear();
        Console.WriteLine("Welcome to the Scripture Memorizer!");

        // Load scriptures from file
        var scriptures = Scripture.LoadFromFile("scriptures.txt");
        Random random = new Random();
        var scripture = scriptures[random.Next(scriptures.Count)];

        Console.WriteLine("Press Enter to hide words, type 'hint' for a hint, or 'quit' to exit.\n");

        while (true)
        {
            Console.Clear();
            Console.WriteLine(scripture);
            Console.WriteLine($"\nProgress: {scripture.GetProgress():F2}% of words hidden.");

            if (scripture.IsCompletelyHidden())
            {
                Console.WriteLine("\nCongratulations! You've hidden all the words.");
                break;
            }

            Console.Write("\nPress Enter to hide words, type 'hint' for a hint, or 'quit' to exit: ");
            string input = Console.ReadLine();

            if (input.ToLower() == "quit")
                break;
            else if (input.ToLower() == "hint")
                scripture.RevealHint();
            else
                scripture.HideRandomWords(3);
        }

        Console.WriteLine("\nThank you for using the Scripture Memorizer!");
    }
}
