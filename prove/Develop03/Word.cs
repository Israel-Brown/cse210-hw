public class Word
{
    public string Text { get; private set; }
    public bool IsHidden { get; private set; }
    private string _hint;

    public Word(string text)
    {
        Text = text;
        IsHidden = false;
        _hint = text[0] + new string('_', text.Length - 1); // First letter + underscores
    }

    public void Hide()
    {
        IsHidden = true;
    }

    public void RevealFirstLetter()
    {
        IsHidden = false;
        Text = _hint;
    }

    public override string ToString()
    {
        return IsHidden ? new string('_', Text.Length) : Text;
    }
}
