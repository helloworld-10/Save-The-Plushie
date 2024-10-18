using System;
using System.Collections.Generic;
using System.Text;

// Helper Class to figure out timing

class TimingClass
{
    // time limit is when you want a timer that counts down
    double timeLimit;
    // starting time for when we started the timer
    public long startingTime;
    // getting global time
    static long globalStartingTime = DateTime.UtcNow.Ticks;
    // final time is when timer is stopped
    public Boolean isFinalTime;
    public double finalTime;
    public Boolean isReset = false;
    // option to initialize the timer with a timelimit, and it will count down
    public TimingClass(double timeLimit)
    {
        this.timeLimit = timeLimit;
        this.startingTime = DateTime.UtcNow.Ticks;
    }
    // if user doens't initalize the timer with a timelimit, then they want to count UP not down
    public TimingClass()
    {
        this.startingTime = DateTime.UtcNow.Ticks;
        isFinalTime = false;

    }
    // resets the tier back to zero (useful for repeat timers)
    public void resetTimer(double timeLimit)
    {
        this.timeLimit = timeLimit;
        this.startingTime = DateTime.UtcNow.Ticks;
        this.isReset = true;
    }
    // checks how much time has passed
    public double timePassed()
    {
        TimeSpan elapsed = new TimeSpan(DateTime.UtcNow.Ticks - this.startingTime);
        double secondsPassed = elapsed.TotalSeconds;

        if (!isFinalTime)
        {
            // sets how much time passed if we haven't called this before
            finalTime = secondsPassed;
        }
        return secondsPassed;
    }
    // checks whether the time lmit is passed
    public bool timeLimitReached()
        // if time limit passed, return true, otherwise, false
    {
        TimeSpan elapsed = new TimeSpan(DateTime.UtcNow.Ticks - startingTime);
        double secondsPassed = elapsed.TotalSeconds;
        if(secondsPassed > timeLimit)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    // checks how much time passed (global timer --> static)
    public static double timeElapsed()
    {
        // does math magic & returns the time
        TimeSpan elapsed = new TimeSpan(DateTime.UtcNow.Ticks - globalStartingTime);
        double secondsPassed = elapsed.TotalSeconds;
        return secondsPassed;
    }


}
