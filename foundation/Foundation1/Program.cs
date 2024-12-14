using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        // Random comments generator for creativity
        List<string> randomComments = new List<string>
        {
            "This is the best video I've ever seen!",
            "I can't stop laughing. 😂",
            "This video saved my day!",
            "Where has this content been all my life?",
            "10/10 would recommend!",
            "I disagree, but it’s interesting.",
            "This was surprisingly insightful!",
            "Can someone explain this part to me?",
            "Wow, the quality is amazing!",
            "This deserves more views."
        };

        // Create videos
        Video video1 = new Video("Learn C# in 10 Minutes", "John Doe", 600, 150, 5);
        Video video2 = new Video("Master OOP in C#", "Jane Smith", 1200, 200, 10);
        Video video3 = new Video("Abstraction Simplified", "Chris Brown", 900, 100, 2);

        // Add random comments to each video
        AddRandomComments(video1, randomComments, 3);
        AddRandomComments(video2, randomComments, 4);
        AddRandomComments(video3, randomComments, 3);

        // Store videos in a list
        List<Video> videos = new List<Video> { video1, video2, video3 };

        // Display video details
        foreach (Video video in videos)
        {
            Console.WriteLine($"Video ID: {video.VideoID}");
            Console.WriteLine($"Title: {video.Title}");
            Console.WriteLine($"Author: {video.Author}");
            Console.WriteLine($"Length: {video.GetFormattedDuration()}");
            Console.WriteLine($"Likes/Dislikes: {video.GetLikeDislikeRatio()}");
            Console.WriteLine($"Number of Comments: {video.GetNumberOfComments()}");

            Console.WriteLine("Comments:");
            foreach (Comment comment in video.GetComments())
            {
                Console.WriteLine($"- {comment.GetComment()}");
            }

            Console.WriteLine(); // Add a blank line between videos
        }
    }

    // Function to add random comments to a video
    static void AddRandomComments(Video video, List<string> randomComments, int numberOfComments)
    {
        Random rand = new Random();

        for (int i = 0; i < numberOfComments; i++)
        {
            string commenterName = $"User{rand.Next(1, 100)}";
            string commentText = randomComments[rand.Next(randomComments.Count)];
            video.AddComment(new Comment(commenterName, commentText));
        }
    }
}
