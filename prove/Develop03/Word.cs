namespace ScriptureApp
{
    public class Word
    {
        public string Text { get; private set; }
        public bool IsHidden { get; private set; }

        public Word(string text)
        {
            Text = text;
            IsHidden = false;
        }

        public void Hide()
        {
            IsHidden = true;
        }

        public void Reveal()
        {
            IsHidden = false;
        }

        public override string ToString()
        {
            return IsHidden ? new string('_', Text.Length) : Text;
        }
    }
}
