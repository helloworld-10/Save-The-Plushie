using System;
using System.Collections.Generic;
using System.Text;

// screen displayed when player wins
class WinScreen : Screen
{
    // final score is based on how fast they completed
    bool endScore = false;
    double finalScore;
    bool isScoreSaved = false;
    // exit button for player to leave
    Button exitButton = new Button(
            new Vector2(0, 0),
            new Vector2(60, 25),
            "Exit!",
            Color.Black
            );

    public WinScreen(Vector2 resolution) : base(resolution)
    {
       

    }
    // drawing the win screen
    public override void draw()
    {
       
        Engine.DrawRectSolid(boundsBox, Color.DarkGreen); // background
        if (endScore == false)
        {
            // calculate game play grading 
            StartScreen.curTime.isFinalTime = true;
            StartScreen.curTime.timePassed();
            // calculting the final score based on how much time elapsed
            finalScore = 600.00 - StartScreen.curTime.finalTime;
            endScore = true;
        }
        
        // string displaying they won + their score
        String output = "You Won, Your GamePlayGrading is " + finalScore.ToString("0.00");
        Engine.DrawString(
            output,
            new Vector2(Resolution.X / 2, Resolution.Y / 2),
            Color.White,
            size3Font,
            TextAlignment.Center
            );

        // if score is not saved, then save it in a text file and add it
        if (!isScoreSaved)
        {
            isScoreSaved = true;
            SaveScores saveScores = new SaveScores(".\\Game\\Scores\\TopScores.txt");
            saveScores.addNewScore((int)finalScore);
            saveScores.saveScores();
        }

        exitButton.draw(size3Font);
    }

    // if the click to exit out, take them to the home screen.
    public override Screen checkActivity(bool isMouseClicked, Vector2 mousePos) 
    {
        if (exitButton.isClicked(isMouseClicked, mousePos))
        {
            return new StartScreen(Resolution);
        }

        return null;
    }
}
