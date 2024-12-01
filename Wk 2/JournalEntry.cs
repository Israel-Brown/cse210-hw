namespace JournalApp
{
    class JournalEntry
    {
        public string Date { get; }
        public string Prompt { get; }
        public string Response { get; }

        public JournalEntry(string date, string prompt, string response)
        {
            Date = date;
            Prompt = prompt;
            Response = response;
        }
    }
}
