using System;
using System.Collections.Generic;

// Comment class
class Comment
{
    public string CommenterName { get; set; }
    public string Text { get; set; }

    public Comment(string commenterName, string text)
    {
        CommenterName = commenterName;
        Text = text;
    }
}

// Video class
class Video
{
    public string Title { get; set; }
    public string Author { get; set; }
    public int LengthInSeconds { get; set; }
    public List<Comment> Comments { get; set; }

    public Video(string title, string author, int lengthInSeconds)
    {
        Title = title;
        Author = author;
        LengthInSeconds = lengthInSeconds;
        Comments = new List<Comment>();
    }

    public void AddComment(Comment comment)
    {
        Comments.Add(comment);
    }

    public int GetCommentCount()
    {
        return Comments.Count;
    }

    // Enhanced: format length in minutes and seconds
    public string GetFormattedLength()
    {
        int minutes = LengthInSeconds / 60;
        int seconds = LengthInSeconds % 60;
        return $"{minutes}m {seconds}s";
    }
}

// Program class
class Program
{
    static void Main()
    {
        // Create videos
        Video video1 = new Video("C# Basics", "Alice", 300);
        Video video2 = new Video("OOP in C#", "Bob", 450);
        Video video3 = new Video("YouTube API Tutorial", "Charlie", 600);

        // Add comments to videos
        video1.AddComment(new Comment("John", "Great explanation!"));
        video1.AddComment(new Comment("Emma", "Very helpful, thanks!"));
        video1.AddComment(new Comment("Liam", "I learned a lot from this video."));

        video2.AddComment(new Comment("Olivia", "Clear and concise."));
        video2.AddComment(new Comment("Noah", "Good examples."));
        video2.AddComment(new Comment("Sophia", "I love this tutorial."));
        video2.AddComment(new Comment("Mason", "Awesome content!"));

        video3.AddComment(new Comment("Ava", "Exactly what I needed."));
        video3.AddComment(new Comment("Ethan", "Super informative."));
        video3.AddComment(new Comment("Isabella", "Well explained!"));

        // Store videos in a list
        List<Video> videos = new List<Video> { video1, video2, video3 };

        // Display video details and comments
        foreach (Video video in videos)
        {
            Console.WriteLine("Title: " + video.Title);
            Console.WriteLine("Author: " + video.Author);
            Console.WriteLine("Length: " + video.GetFormattedLength());
            Console.WriteLine("Number of Comments: " + video.GetCommentCount());
            Console.WriteLine("Comments:");
            foreach (Comment comment in video.Comments)
            {
                Console.WriteLine("- " + comment.CommenterName + ": " + comment.Text);
            }
            Console.WriteLine(new string('-', 40));
        }
    }
}