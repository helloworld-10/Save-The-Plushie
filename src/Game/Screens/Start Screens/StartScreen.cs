using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TiledCS; 

class StartScreen : Screen
{
    public static TimingClass curTime;

    Button startButton = new Button(
            new Vector2(0, 0),
            new Vector2(80, 25),
            "Start!",
            Color.Red
            );
    Button rulesButton = new Button(
        new Vector2(0, 60),
        new Vector2(90, 25),
        "Rules!",
        Color.Red
        );

    Button gameCreditsButton = new Button(
        new Vector2(0, 90),
        new Vector2(180, 25),
        "Game Credits!",
        Color.Red
        );
    Button scoreboardButton = new Button(
            new Vector2(0, 30),
            new Vector2(165, 25),
            "Scoreboard!",
            Color.Red
            );

    public StartScreen(Vector2 resolution) : base(resolution)
    {

    }

    public override void draw() 
    {
	
	    Engine.DrawRectSolid(boundsBox, Color.Green); // background

        Engine.DrawString(
            "Start Screen",
            new Vector2(Resolution.X / 2, Resolution.Y / 2),
            Color.White,
            size1Font,
            TextAlignment.Center
            ); 

        startButton.draw(size3Font);
        scoreboardButton.draw(size3Font);
        rulesButton.draw(size3Font);
        gameCreditsButton.draw(size3Font);
    }

    public override Screen checkActivity(bool isMouseClicked, Vector2 mousePos) 
    {
        // check start button
        Screen newScreen = checkButton(isMouseClicked, mousePos,
                                        startButton,
                                        new GameLevelScreen(Resolution, 0));
        if (newScreen != null)
        {
            curTime = new TimingClass();
            Console.Write("added da timing class");
        }

        if (newScreen == null)
        {
            // check scoreboard button
            newScreen = checkButton(isMouseClicked, mousePos, 
                                    scoreboardButton,
                                    new ScoreboardScreen(Resolution));
        }
        if (newScreen == null)
        {
            // check rules button
            newScreen = checkButton(isMouseClicked, mousePos,
                                    rulesButton,
                                    new RulesScreen(Resolution));
        }

        if (newScreen == null)
        {
            // check game credits button
            newScreen = checkButton(isMouseClicked, mousePos,
                                    gameCreditsButton,
                                    new GameCreditsScreen(Resolution));
        }

        return newScreen;
    }

    public Screen checkButton(bool isMouseClicked, Vector2 mousePos, Button button, Screen newScreen) 
    {
        if (button.isClicked(isMouseClicked, mousePos))
        {
            return newScreen;
        }
        else
        {
            return null;
        }
    }
}
