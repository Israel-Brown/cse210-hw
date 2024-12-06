using System;

class Program
{
    static void Main(string[] args)
    {
        try
        {
            string filePath = "scriptures.txt"; // Path to the scripture file
            Scripture scripture = Scripture.LoadFromFile(filePath);

            Console.WriteLine("Scripture Memorization Program");
            while (true)
            {
                Console.Clear();
                scripture.Display();

                // Display progress
                double progress = scripture.GetProgress();
                Console.WriteLine($"Progress: {progress:F2}%");

                if (scripture.IsCompletelyHidden())
                {
                    Console.WriteLine("You have memorized the scripture!");
                    break;
                }

                Console.WriteLine("Press 'H' to hide words, 'R' to reveal hints, or 'Q' to quit:");
                char choice = char.ToUpper(Console.ReadKey(true).KeyChar);

                switch (choice)
                {
                    case 'H':
                        scripture.HideWords(3);
                        break;
                    case 'R':
                        scripture.RevealWords(3);
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
