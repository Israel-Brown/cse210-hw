namespace ScriptureApp // Ensure the correct namespace
{
    public class Reference
    {
        public string Book { get; private set; }
        public int Chapter { get; private set; }
        public int VerseStart { get; private set; }
        public int? VerseEnd { get; private set; } // Nullable for single verses

        public Reference(string book, int chapter, int verseStart, int? verseEnd = null)
        {
            Book = book;
            Chapter = chapter;
            VerseStart = verseStart;
            VerseEnd = verseEnd;
        }

        public override string ToString()
        {
            return VerseEnd == null 
                ? $"{Book} {Chapter}:{VerseStart}" 
                : $"{Book} {Chapter}:{VerseStart}-{VerseEnd}";
        }
    }
}
