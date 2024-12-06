using System;  // For basic input/output operations
using ScriptureApp;  // Ensure this matches the namespace of the Scripture class

namespace Develop03
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                // File path for loading the scripture file
                string filePath = "scriptures.txt";  
                
                // Load the scripture (ensure the LoadFromFile method exists in your Scripture class)
                Scripture scripture = Scripture.LoadFromFile(filePath);  

                Console.WriteLine("Scripture Memorization Program");

                while (true)
                {
                    Console.Clear();

                    // Display the scripture
                    Console.WriteLine(scripture.ToString());

                    // Get progress using the method correctly (with parentheses)
                    int progress = (int)scripture.GetProgress();  // Explicit cast to int to fix CS0266 error

                    Console.WriteLine($"Progress: {progress}%");

                    // Check if the scripture is completely hidden
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
                            // Hide 3 words (ensure HideWords expects an integer argument)
                            scripture.HideWords(3);
                            break;
                        case 'R':
                            // Since RevealHint is not defined, you may want to replace it with something else
                            // Assuming you want to show a hint in some way, you could implement it here
                            Console.WriteLine("RevealHint is not implemented yet.");
                            break;
                        case 'Q':
                            return;  // Exit the program
                        default:
                            Console.WriteLine("Invalid choice. Please try again.");
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                // Display any errors that occur
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
    }
}
