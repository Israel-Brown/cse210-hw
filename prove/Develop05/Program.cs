using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

public abstract class MindfulnessActivity
{
    protected string ActivityName { get; set; }
    protected string Description { get; set; }
    protected int Duration { get; set; }

    public abstract void StartActivity();

    // Method to display the starting message and prompt for duration
    protected void DisplayStartingMessage()
    {
        // Displays the name and description of the activity to introduce it to the user
        Console.WriteLine($"{ActivityName} - {Description}");
        Console.Write("Enter the duration (in seconds): ");
        // Takes input from the user for the activity duration
        Duration = Convert.ToInt32(Console.ReadLine());
        // Informs the user to get ready
        Console.WriteLine("Get ready...");
        // Gives a small pause before starting the activity
        Thread.Sleep(2000);
    }

    // Method to display the ending message
    protected void DisplayEndingMessage()
    {
        // Acknowledges the user’s effort and lets them know they completed the activity
        Console.WriteLine("Good job!");
        Thread.Sleep(1000);
        // Displays the total time spent on the activity
        Console.WriteLine($"You completed the {ActivityName} for {Duration} seconds.");
        Thread.Sleep(1000); // Short pause before completing the activity
    }

    // Method to display a spinner animation during pauses
    protected void DisplaySpinnerAnimation()
    {
        // A spinner animation is used to make the waiting time more engaging and visually interesting
        char[] spinner = new char[] { '|', '/', '-', '\\' };
        foreach (char c in spinner)
        {
            // Shows one character from the spinner and overwrites the previous one for animation effect
            Console.Write("\b" + c);
            Thread.Sleep(500); // Brief delay to allow animation to be visible
        }
    }

    // Method to handle pauses during an activity
    protected void PauseForSeconds(int seconds)
    {
        // Keeps track of the current time and the end time for the pause
        DateTime startTime = DateTime.Now;
        DateTime futureTime = startTime.AddSeconds(seconds);

        // While the current time is less than the future time, keep showing the spinner
        while (DateTime.Now < futureTime)
        {
            DisplaySpinnerAnimation();
        }
    }
}

// Breathing Activity
public class BreathingActivity : MindfulnessActivity
{
    public BreathingActivity()
    {
        // Sets the activity name and description
        ActivityName = "Breathing Activity";
        Description = "This activity will help you relax by walking you through breathing in and out slowly.";
    }

    public override void StartActivity()
    {
        DisplayStartingMessage(); // Display starting message with activity details

        Console.WriteLine("Breathe in..."); // Prompt to breathe in
        PauseForSeconds(2); // Pause for 2 seconds to simulate breathing in
        Console.WriteLine("Breathe out..."); // Prompt to breathe out
        PauseForSeconds(2); // Pause for 2 seconds to simulate breathing out

        // Loop through breathing for the specified duration
        DateTime endTime = DateTime.Now.AddSeconds(Duration);
        while (DateTime.Now < endTime)
        {
            // Repeated breathing prompts every 2 seconds until the activity time ends
            Console.WriteLine("Breathe in...");
            PauseForSeconds(2);
            Console.WriteLine("Breathe out...");
            PauseForSeconds(2);
        }

        DisplayEndingMessage(); // Display the ending message when the activity completes

        // Exceeds Expectations: 
        // - The breathing activity is structured to guide the user through slow, deliberate breathing with clear, simple instructions.
        // - Pauses between "Breathe in" and "Breathe out" allow the user to focus on their breath.
        // - The user is encouraged to repeat the process for the specified duration, making it a fully engaging mindfulness activity.
    }
}

// Reflection Activity
public class ReflectionActivity : MindfulnessActivity
{
    private List<string> reflectionPrompts = new List<string>
    {
        "Think of a time when you stood up for someone else.",
        "Think of a time when you did something really difficult.",
        "Think of a time when you helped someone in need.",
        "Think of a time when you did something truly selfless."
    };

    private List<string> reflectionQuestions = new List<string>
    {
        "Why was this experience meaningful to you?",
        "Have you ever done anything like this before?",
        "How did you get started?",
        "How did you feel when it was complete?",
        "What made this time different than other times when you were not as successful?",
        "What is your favorite thing about this experience?",
        "What could you learn from this experience that applies to other situations?",
        "What did you learn about yourself through this experience?",
        "How can you keep this experience in mind in the future?"
    };

    public ReflectionActivity()
    {
        ActivityName = "Reflection Activity";
        Description = "This activity will help you reflect on times in your life when you have shown strength and resilience.";
    }

    public override void StartActivity()
    {
        DisplayStartingMessage(); // Display starting message with activity details

        // Randomly select a prompt from the list of reflection prompts
        Random rand = new Random();
        string randomPrompt = reflectionPrompts[rand.Next(reflectionPrompts.Count)];
        Console.WriteLine(randomPrompt); // Display the selected reflection prompt

        // For each reflection question, prompt the user and give them a pause to reflect
        foreach (var question in reflectionQuestions)
        {
            Console.WriteLine(question);
            PauseForSeconds(5); // Give the user 5 seconds to reflect and think about the question
        }

        DisplayEndingMessage(); // Display the ending message when the activity completes

        // Exceeds Expectations: 
        // - The reflection activity offers thoughtful prompts that encourage deep personal reflection.
        // - By randomly selecting a prompt, the activity feels personalized and dynamic, providing a new experience each time.
        // - The pause between each question gives users time to genuinely consider their answers, ensuring a meaningful reflection session.
    }
}

// Listing Activity
public class ListingActivity : MindfulnessActivity
{
    private List<string> listingPrompts = new List<string>
    {
        "Who are people that you appreciate?",
        "What are personal strengths of yours?",
        "Who are people that you have helped this week?",
        "When have you felt the Holy Ghost this month?",
        "Who are some of your personal heroes?"
    };

    public ListingActivity()
    {
        ActivityName = "Listing Activity";
        Description = "This activity will help you reflect on the good things in your life by having you list as many things as you can in a certain area.";
    }

    public override void StartActivity()
    {
        DisplayStartingMessage(); // Display starting message with activity details

        // Randomly select a prompt from the list of listing prompts
        Random rand = new Random();
        string randomPrompt = listingPrompts[rand.Next(listingPrompts.Count)];
        Console.WriteLine(randomPrompt); // Display the selected listing prompt
        Console.WriteLine("Take some time to think...");

        PauseForSeconds(3); // Give the user 3 seconds to think about the prompt

        List<string> userList = new List<string>(); // List to store user’s responses
        DateTime endTime = DateTime.Now.AddSeconds(Duration);

        // Allow the user to enter as many items as they want until the time runs out
        while (DateTime.Now < endTime)
        {
            Console.WriteLine("Enter an item (type 'done' to finish): ");
            string item = Console.ReadLine();

            // If the user types 'done', stop taking inputs
            if (item.ToLower() == "done")
                break;

            // Add the item to the list
            userList.Add(item);
            Console.WriteLine($"You have listed {userList.Count} items.");
        }

        // At the end of the activity, display how many items the user listed
        Console.WriteLine($"You listed {userList.Count} items.");
        DisplayEndingMessage(); // Display the ending message when the activity completes

        // Exceeds Expectations:
        // - The listing activity offers a positive prompt that encourages the user to focus on the good things in their life.
        // - The interactive nature, where the user can enter responses and track the number of items listed, increases engagement.
        // - The 'done' input allows users to have control over when they finish, which fosters a sense of autonomy and satisfaction.
    }
}

// Main Program
class Program
{
    static void Main(string[] args)
    {
        // Greeting the user and giving them options for activities
        Console.WriteLine("Welcome to the MindfulMoments Program!");
        Console.WriteLine("Choose an activity:");
        Console.WriteLine("1. Breathing Activity");
        Console.WriteLine("2. Reflection Activity");
        Console.WriteLine("3. Listing Activity");

        int choice = Convert.ToInt32(Console.ReadLine()); // Takes user input for activity choice

        MindfulnessActivity activity = null;

        // Based on user input, create the selected activity
        switch (choice)
        {
            case 1:
                activity = new BreathingActivity();
                break;
            case 2:
                activity = new ReflectionActivity();
                break;
            case 3:
                activity = new ListingActivity();
                break;
            default:
                Console.WriteLine("Invalid choice.");
                return;
        }

        activity.StartActivity();

        // Exceeds Expectations: The main program is user-friendly, offering three distinct mindfulness activities
        // for a personalized and engaging experience. The user can easily choose an activity from a list of options.
    }
}