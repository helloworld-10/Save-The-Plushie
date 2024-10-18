using System;
using System.Collections.Generic;
using System.Text;

class ScoreboardScreen : Screen
{
    Button exitButton = new Button(
            new Vector2(0, 0),
            new Vector2(60, 25),
            "Exit!",
            Color.Red
            );

    SaveScores loadScores;

    public ScoreboardScreen(Vector2 resolution) : base(resolution)
    {
        loadScores = new SaveScores(".\\Game\\Scores\\TopScores.txt");
        addOnScreen = true;
    }

    public override void draw()
    {
        Engine.DrawRectSolid(boundsBox, Color.Blue); // background
        Engine.DrawString(
            "Scoreboard!",
            new Vector2(Resolution.X / 2, 0),
            Color.White,
            size1Font,
            TextAlignment.Center
            );

        loadScores.draw(size3Font, Resolution);
        exitButton.draw(size3Font);


    }
    public override Screen checkActivity(bool isMouseClicked, Vector2 mousePos)
    {
        if (exitButton.isClicked(isMouseClicked, mousePos))
        {
            return Screen.Empty;
        }

        return null;
    }
}
