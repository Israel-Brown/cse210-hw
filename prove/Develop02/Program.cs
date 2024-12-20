using ScriptureApp;  // Make sure this matches the namespace of your Scripture class

class Program
{
    static void Main(string[] args)
    {
        try
        {
            string filePath = "scriptures.txt"; // Path to the scripture file
            Scripture scripture = Scripture.LoadFromFile(filePath);

            Console.WriteLine("Scripture memorization program");

            while (true)
            {
                Console.Clear();
                Console.WriteLine(scripture.ToString());

                // Get the progress correctly
                int progress = scripture.GetProgress();  // Call the method

                Console.WriteLine($"Progress: {progress}%");

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
                        scripture.HideWords(3);  // Assuming HideWords accepts an int argument
                        break;
                    case 'R':
                        scripture.RevealHint(3);  // Assuming RevealHint accepts an int argument
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
