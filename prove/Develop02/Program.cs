using System;

class Program
{
    static void Main(string[] args)
    {
        Journal journal = new Journal();
        bool isRunning = true;

        while (isRunning)
        {
            Console.WriteLine("\nJournal Program Menu:");
            Console.WriteLine("1. Write a new entry");
            Console.WriteLine("2. Display all entries");
            Console.WriteLine("3. Save journal to a file (CSV)");
            Console.WriteLine("4. Load journal from a file");
            Console.WriteLine("5. Search entries");
            Console.WriteLine("6. Exit");
            Console.Write("Choose an option (1-6): ");

            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    journal.AddEntry();
                    break;
                case "2":
                    journal.DisplayEntries();
                    break;
                case "3":
                    journal.SaveToCsv();
                    break;
                case "4":
                    journal.LoadFromCsv();
                    break;
                case "5":
                    journal.SearchEntries();
                    break;
                case "6":
                    isRunning = false;
                    break;
                default:
                    Console.WriteLine("Invalid option. Please choose between 1 and 6.");
                    break;
            }
        }
    }
}
