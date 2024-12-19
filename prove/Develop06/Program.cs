using System;
using System.Collections.Generic;

public enum DifficultyLevel
{
    Easy,    // Goal is easy to achieve
    Medium,  // Goal is of medium difficulty
    Hard     // Goal is challenging and requires more effort
}

public class Goal
{
    public string Name { get; set; }        // Name of the goal
    public int Points { get; set; }         // Points awarded for goal completion
    public DifficultyLevel Difficulty { get; set; }  // Difficulty level of the goal
    public DateTime Deadline { get; set; }  // Optional deadline for time-based goals
    public bool IsCompleted { get; set; }   // Status of whether the goal is completed

    // Constructor allowing the initialization of goal with difficulty
    public Goal(string name, int points, DifficultyLevel difficulty)
    {
        Name = name;
        Points = points;
        Difficulty = difficulty;
        IsCompleted = false;
    }

    // Method to record the completion of a goal
    public virtual void RecordGoal()
    {
        if (!IsCompleted)
        {
            IsCompleted = true;
            Console.WriteLine($"Goal '{Name}' completed! Points awarded: {Points}");
        }
        else
        {
            Console.WriteLine($"Goal '{Name}' has already been completed.");
        }
    }

    // Method to display the goal's information
    public void DisplayGoalInfo()
    {
        Console.WriteLine($"Goal: {Name}, Points: {Points}, Difficulty: {Difficulty}");
    }
}

// Class to represent the Leaderboard functionality
public class LeaderBoard
{
    public List<Player> Players { get; set; }

    // Method to display the leaderboard
    public void DisplayLeaderBoard()
    {
        Console.WriteLine("Leaderboard:");
        foreach (var player in Players)
        {
            Console.WriteLine($"Player: {player.Name}, Points: {player.TotalPoints}");
        }
    }
}

// Player class to represent a user/player
public class Player
{
    public string Name { get; set; }          // Player's name
    public int TotalPoints { get; set; }      // Total points the player has earned

    public Player(string name)
    {
        Name = name;
        TotalPoints = 0;  // Initially, player has 0 points
    }

    // Method to add points to the player's total
    public void AddPoints(int points)
    {
        TotalPoints += points;
    }
}

// Class to manage goal reminders (notifications)
public class Reminder
{
    public DateTime ReminderTime { get; set; } // The time when the reminder is set
    public string Message { get; set; }        // The message for the reminder

    // Constructor to initialize reminder
    public Reminder(DateTime reminderTime, string message)
    {
        ReminderTime = reminderTime;
        Message = message;
    }

    // Method to send the reminder to the user
    public void SendReminder()
    {
        if (DateTime.Now >= ReminderTime)
        {
            Console.WriteLine($"Reminder: {Message}");
        }
    }
}

// Time-based goal class for goals with deadlines
public class TimeBasedGoal : Goal
{
    public TimeBasedGoal(string name, int points, DifficultyLevel difficulty, DateTime deadline)
        : base(name, points, difficulty)
    {
        Deadline = deadline;
    }

    // Override the goal completion method to account for the deadline
    public override void RecordGoal()
    {
        if (DateTime.Now <= Deadline)
        {
            IsCompleted = true;
            Console.WriteLine($"Goal '{Name}' completed on time! Points awarded: {Points}");
        }
        else
        {
            Console.WriteLine($"Goal '{Name}' missed its deadline.");
        }
    }
}

// Help system to provide tutorials or guides to the user
public class HelpSystem
{
    // Method to display a tutorial for the user
    public void ShowTutorial()
    {
        Console.WriteLine("Welcome to the Goal Tracker! Here's how to use the app...");
        // Additional tutorial content could be shown here
    }
}

// Reward system to redeem points for rewards
public class RewardStore
{
    public List<string> Rewards { get; set; }  // List of available rewards

    public RewardStore()
    {
        Rewards = new List<string>
        {
            "Custom Avatar",
            "Background Theme",
            "Bonus Points"
        };
    }

    // Method to redeem a reward
    public void RedeemReward(int rewardIndex, Player player)
    {
        if (player.TotalPoints >= 100)  // Assuming rewards require 100 points
        {
            Console.WriteLine($"Reward '{Rewards[rewardIndex]}' redeemed!");
            player.AddPoints(-100);  // Deduct 100 points for the reward
        }
        else
        {
            Console.WriteLine("Not enough points to redeem this reward.");
        }
    }
}

// Main program to interact with goals, rewards, and other features
public class Program
{
    public static void Main()
    {
        // Creating a player
        Player player = new Player("Israel");

        // Creating goals with different difficulty levels
        Goal easyGoal = new Goal("Read a Book", 50, DifficultyLevel.Easy);
        Goal hardGoal = new Goal("Run a Marathon", 200, DifficultyLevel.Hard);

        // Adding goals to the player
        easyGoal.RecordGoal();  // Completes the easy goal
        hardGoal.RecordGoal();  // Completes the hard goal

        // Displaying player points after completing goals
        player.AddPoints(easyGoal.Points);
        player.AddPoints(hardGoal.Points);
        Console.WriteLine($"{player.Name} has {player.TotalPoints} points.");

        // Creating and displaying the leaderboard
        LeaderBoard leaderBoard = new LeaderBoard();
        leaderBoard.Players = new List<Player> { player };
        leaderBoard.DisplayLeaderBoard();

        // Reminders
        Reminder goalReminder = new Reminder(DateTime.Now.AddMinutes(1), "Don't forget to check your goals!");
        goalReminder.SendReminder();  // Simulate sending a reminder

        // Creating and redeeming rewards
        RewardStore store = new RewardStore();
        store.RedeemReward(1, player);  // Trying to redeem a reward with enough points

        // Help system
        HelpSystem help = new HelpSystem();
        help.ShowTutorial();
    }
}

// Exceeding Requirements:
  
// 1. **Leaderboard Functionality**: 
//    - Added a leaderboard system that displays players' names and their total points, increasing the social aspect of goal tracking and motivating users to compete.
//    - Usage: Users' points are tracked and displayed, helping them see how they rank against others.

// 2. **Reward System**:
//    - Implemented a reward system where users can redeem points for virtual rewards like avatars, themes, and bonus points.
//    - Usage: Players accumulate points by completing goals and can use them to redeem rewards, which adds a gamified aspect to the app.

// 3. **Goal Reminders**:
//    - Created a reminder system where users can set time-based reminders for their goals, which helps with accountability.
//    - Usage: A reminder is triggered to notify the user about their goal when the scheduled time arrives.

// 4. **Time-Based Goals**:
//    - Introduced `TimeBasedGoal` class, which allows for goals to have deadlines. The user must complete the goal before the deadline to earn the points.
//    - Usage: Goals with deadlines now provide extra challenge and urgency, motivating users to complete goals on time.

// 5. **Help System**:
//    - Added a basic help system that can display a tutorial or instructions on how to use the app, aiding users who may need guidance or support.
//    - Usage: The tutorial can be accessed to help users learn how to interact with the app and make the most out of its features.

// 6. **Dynamic Reward Store**:
//    - Implemented a dynamic reward store with customizable rewards, providing a more personalized user experience and increasing engagement.
//    - Usage: Users can redeem rewards after earning enough points, making the goal completion more fulfilling.
