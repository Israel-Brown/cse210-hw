public class Comment
{
    public string Name { get; set; }
    public string Text { get; set; }

    public Comment(string name, string text)
    {
        Name = name;
        Text = text;
    }

    // Returns a formatted comment string
    public string GetComment()
    {
        return $"\"{Text}\" - {Name}";
    }
}
