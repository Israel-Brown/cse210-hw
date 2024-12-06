namespace ScriptureApp  // Make sure this matches the namespace in your other files
{
    public class Word
    {
        public string Text { get; private set; }
        public bool IsHidden { get; private set; }

        // Constructor
        public Word(string text)
        {
            Text = text;
            IsHidden = false;
        }

        // Method to hide the word
        public void Hide()
        {
            IsHidden = true;
        }

        // Method to reveal the word
        public void Reveal()
        {
            IsHidden = false;
        }

        // Override ToString method to display the word or underscores if hidden
        public override string ToString()
        {
            return IsHidden ? new string('_', Text.Length) : Text;
        }
    }
}
