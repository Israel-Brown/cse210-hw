using System;
using System.Collections.Generic;

public class Video
{
    public int VideoID { get; private set; }
    public string Title { get; set; }
    public string Author { get; set; }
    public int LengthInSeconds { get; set; }
    private List<Comment> Comments { get; set; }
    public int Likes { get; set; }
    public int Dislikes { get; set; }

    private static int _idCounter = 1;

    public Video(string title, string author, int lengthInSeconds, int likes = 0, int dislikes = 0)
    {
        VideoID = _idCounter++;
        Title = title;
        Author = author;
        LengthInSeconds = lengthInSeconds;
        Comments = new List<Comment>();
        Likes = likes;
        Dislikes = dislikes;
    }

    // Add a comment to the video
    public void AddComment(Comment comment)
    {
        Comments.Add(comment);
    }

    // Get the number of comments
    public int GetNumberOfComments()
    {
        return Comments.Count;
    }

    // Retrieve all comments
    public List<Comment> GetComments()
    {
        return Comments;
    }

    // Converts video length to "minutes:seconds" format
    public string GetFormattedDuration()
    {
        int minutes = LengthInSeconds / 60;
        int seconds = LengthInSeconds % 60;
        return $"{minutes}:{seconds:D2}"; // D2 formats seconds as 2 digits
    }

    // Display likes and dislikes as a ratio
    public string GetLikeDislikeRatio()
    {
        return $"{Likes} 👍 / {Dislikes} 👎";
    }
}
