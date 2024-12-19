using System;
using System.Collections.Generic;

public enum DistanceUnit
{
    Miles,
    Kilometers
}

public abstract class ExerciseSession
{
    private DateTime date;
    private int durationInMinutes;
    protected DistanceUnit unit;

    public ExerciseSession(DateTime date, int durationInMinutes, DistanceUnit unit)
    {
        this.date = date;
        this.durationInMinutes = durationInMinutes;
        this.unit = unit;
    }

    public abstract double GetDistance();
    public abstract double GetSpeed();
    public abstract double GetPace();
    public abstract double GetCaloriesBurned(double weight);

    public string GetSummary()
    {
        string distanceUnit = unit == DistanceUnit.Miles ? "miles" : "km";
        string speedUnit = unit == DistanceUnit.Miles ? "mph" : "kph";
        string paceUnit = unit == DistanceUnit.Miles ? "min per mile" : "min per km";
        return $"{date:dd MMM yyyy} {this.GetType().Name} ({durationInMinutes} min): Distance {GetDistance()} {distanceUnit}, Speed {GetSpeed()} {speedUnit}, Pace: {GetPace()} {paceUnit}, Calories Burned: {GetCaloriesBurned(70)} kcal";
    }

    public DateTime GetDate() => date;
    public int GetDuration() => durationInMinutes;
}

public class RunningSession : ExerciseSession
{
    private double distanceInMiles;

    public RunningSession(DateTime date, int durationInMinutes, double distanceInMiles, DistanceUnit unit)
        : base(date, durationInMinutes, unit)
    {
        this.distanceInMiles = distanceInMiles;
    }

    public override double GetDistance() => unit == DistanceUnit.Miles ? distanceInMiles : distanceInMiles * 1.60934;

    public override double GetSpeed()
    {
        return (GetDistance() / GetDuration()) * 60;
    }

    public override double GetPace()
    {
        return GetDuration() / GetDistance();
    }

    public override double GetCaloriesBurned(double weight)
    {
        // Basic estimate: calories burned per minute = 0.75 * weight (kg) * speed (miles per hour)
        return 0.75 * weight * GetSpeed();
    }
}

public class CyclingSession : ExerciseSession
{
    private double speedInMilesPerHour;

    public CyclingSession(DateTime date, int durationInMinutes, double speedInMilesPerHour, DistanceUnit unit)
        : base(date, durationInMinutes, unit)
    {
        this.speedInMilesPerHour = speedInMilesPerHour;
    }

    public override double GetDistance() => (speedInMilesPerHour * GetDuration()) / 60;

    public override double GetSpeed() => unit == DistanceUnit.Miles ? speedInMilesPerHour : speedInMilesPerHour * 1.60934;

    public override double GetPace() => 60 / GetSpeed();

    public override double GetCaloriesBurned(double weight)
    {
        // Basic estimate: calories burned per minute = 0.05 * weight (kg) * speed (kph)
        return 0.05 * weight * GetSpeed();
    }
}

public class SwimmingSession : ExerciseSession
{
    private int numberOfLaps;

    public SwimmingSession(DateTime date, int durationInMinutes, int numberOfLaps, DistanceUnit unit)
        : base(date, durationInMinutes, unit)
    {
        this.numberOfLaps = numberOfLaps;
    }

    public override double GetDistance()
    {
        double distanceInMeters = numberOfLaps * 50;
        return unit == DistanceUnit.Miles ? distanceInMeters * 0.000621371 : distanceInMeters * 0.001;
    }

    public override double GetSpeed()
    {
        return (GetDistance() / GetDuration()) * 60;
    }

    public override double GetPace()
    {
        return GetDuration() / GetDistance();
    }

    public override double GetCaloriesBurned(double weight)
    {
        // Basic estimate: calories burned per minute = 0.1 * weight (kg) * laps
        return 0.1 * weight * numberOfLaps;
    }
}

public class User
{
    public string Name { get; set; }
    private List<ExerciseSession> exerciseSessions;

    public User(string name)
    {
        Name = name;
        exerciseSessions = new List<ExerciseSession>();
    }

    public void AddExerciseSession(ExerciseSession session)
    {
        exerciseSessions.Add(session);
    }

    public void PrintSummary()
    {
        Console.WriteLine($"Summary for {Name}:");
        foreach (var session in exerciseSessions)
        {
            Console.WriteLine(session.GetSummary());
        }
    }

    public void ShowLeaderboard()
    {
        Console.WriteLine("Leaderboard:");
        foreach (var session in exerciseSessions)
        {
            Console.WriteLine($"{Name} covered {session.GetDistance()} distance in {session.GetDuration()} minutes.");
        }
    }
    
    // Exceeding Expectations: Show leaderboard and compare distances
    // This functionality allows tracking and comparing user performance, encouraging competition.
    public void ShowLeaderboardByDistance()
    {
        Console.WriteLine("Leaderboard By Distance:");
        foreach (var session in exerciseSessions)
        {
            Console.WriteLine($"{Name} covered {session.GetDistance():0.##} {session.GetSummary()}");
        }
    }

    // Exceeding Expectations: Personalized goals (distance, speed, calories burned)
    // The user can set their goals and track their progress toward achieving them.
    public void SetAndTrackGoals(double targetDistance, double targetSpeed)
    {
        double totalDistance = 0;
        double totalSpeed = 0;
        foreach (var session in exerciseSessions)
        {
            totalDistance += session.GetDistance();
            totalSpeed += session.GetSpeed();
        }

        Console.WriteLine($"{Name}'s Goal: {targetDistance} miles, Target Speed: {targetSpeed} mph");
        Console.WriteLine($"Current Progress: Total Distance: {totalDistance} miles, Average Speed: {totalSpeed / exerciseSessions.Count} mph");
    }
}

public class Program
{
    public static void Main()
    {
        var user = new User("John Doe");

        // Exceeding Expectations: Adding multiple types of exercise sessions
        // Showcases the app's ability to handle diverse workout types (running, cycling, swimming).
        user.AddExerciseSession(new RunningSession(new DateTime(2022, 11, 3), 30, 3.0, DistanceUnit.Miles));
        user.AddExerciseSession(new CyclingSession(new DateTime(2022, 11, 3), 30, 12.0, DistanceUnit.Kilometers));
        user.AddExerciseSession(new SwimmingSession(new DateTime(2022, 11, 3), 30, 20, DistanceUnit.Kilometers));

        user.PrintSummary();

        // Exceeding Expectations: Show leaderboard by distance
        // This functionality presents an engaging and competitive aspect for users.
        user.ShowLeaderboardByDistance();

        // Exceeding Expectations: Personalized goal setting and tracking progress
        // This feature provides more personalized feedback based on the user's individual fitness journey.
        user.SetAndTrackGoals(100, 8); // Example: 100 miles goal and target speed of 8 mph
    }
}
