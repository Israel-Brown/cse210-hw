using System;
using System.Collections.Generic;
using System.IO;

class Journal
{
    private List<JournalEntry> entries = new List<JournalEntry>();
    private List<string> prompts = new List<string>
    {
        "Who was the most interesting person I interacted with today?",
        "What was the best part of my day?",
        "How did I see the hand of the Lord in my life today?",
        "What was the strongest emotion I felt today?",
        "If I had one thing I could do over today, what would it be?"
    };

    public void AddEntry()
    {
        Random rand = new Random();
        string prompt = prompts[rand.Next(prompts.Count)];
        Console.WriteLine($"\nPrompt: {prompt}");
        Console.Write("Your response: ");
        string response = Console.ReadLine();

        string date = DateTime.Now.ToShortDateString();
        entries.Add(new JournalEntry(prompt, response, date));

        Console.WriteLine("Entry added successfully.");
    }

    public void DisplayEntries()
    {
        if (entries.Count == 0)
        {
            Console.WriteLine("\nNo entries found.");
            return;
        }

        Console.WriteLine("\nJournal Entries:");
        foreach (var entry in entries)
        {
            Console.WriteLine($"Date: {entry.Date}");
            Console.WriteLine($"Prompt: {entry.Prompt}");
            Console.WriteLine($"Response: {entry.Response}\n");
        }
    }

    public void SaveToCsv()
    {
        Console.Write("\nEnter a filename to save (e.g., journal.csv): ");
        string filename = Console.ReadLine();

        try
        {
            using (StreamWriter writer = new StreamWriter(filename))
            {
                writer.WriteLine("Date,Prompt,Response");
                foreach (var entry in entries)
                {
                    string sanitizedResponse = entry.Response.Replace("\"", "\"\""); // Escape quotes
                    writer.WriteLine($"\"{entry.Date}\",\"{entry.Prompt}\",\"{sanitizedResponse}\"");
                }
            }
            Console.WriteLine("Journal saved successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error saving journal: {ex.Message}");
        }
    }

    public void LoadFromCsv()
    {
        Console.Write("\nEnter a filename to load (e.g., journal.csv): ");
        string filename = Console.ReadLine();

        try
        {
            string[] lines = File.ReadAllLines(filename);
            entries.Clear(); // Clear current entries before loading new ones

            for (int i = 1; i < lines.Length; i++) // Skip header line
            {
                string[] parts = lines[i].Split(",");
                string date = parts[0].Trim('"');
                string prompt = parts[1].Trim('"');
                string response = parts[2].Trim('"');
                entries.Add(new JournalEntry(prompt, response, date));
            }

            Console.WriteLine("Journal loaded successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error loading journal: {ex.Message}");
        }
    }

    public void SearchEntries()
    {
        Console.Write("\nEnter a keyword or date to search: ");
        string searchTerm = Console.ReadLine();

        var results = entries.FindAll(entry => entry.Prompt.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                                                entry.Response.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                                                entry.Date.Contains(searchTerm));

        if (results.Count == 0)
        {
            Console.WriteLine("No matching entries found.");
            return;
        }

        Console.WriteLine("\nSearch Results:");
        foreach (var entry in results)
        {
            Console.WriteLine($"Date: {entry.Date}");
            Console.WriteLine($"Prompt: {entry.Prompt}");
            Console.WriteLine($"Response: {entry.Response}\n");
        }
    }
}
