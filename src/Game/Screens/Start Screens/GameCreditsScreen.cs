using System;
using System.Collections.Generic;
using System.Text;

class GameCreditsScreen : Screen
{
    Button exitButton = new Button(
            new Vector2(0, 0),
            new Vector2(60, 25),
            "Exit!",
            Color.Red
            );

    int spacing = 32;

    public GameCreditsScreen(Vector2 resolution) : base(resolution)
    {
        addOnScreen = true;
    }

    public override void draw()
    {
        Engine.DrawRectSolid(boundsBox, Color.Blue); // background

        Engine.DrawString(
            "Game Credits!",
            new Vector2(Resolution.X / 2, 0),
            Color.White,
            size2Font,
            TextAlignment.Center
            );

        Engine.DrawString(
            "Project Manager - Ashwin Kaliyaperumal",
            new Vector2(Resolution.X / 2, 50),
            Color.White,
            size3Font,
            TextAlignment.Center
            );

        Engine.DrawString(
            "Game Developer - Rithvik Koppolu",
            new Vector2(Resolution.X / 2, 50 + spacing * 1),
            Color.White,
            size3Font,
            TextAlignment.Center
            );

        Engine.DrawString(
            "Game Developer - Rajit Joshi",
            new Vector2(Resolution.X / 2, 50 + spacing * 2),
            Color.White,
            size3Font,
            TextAlignment.Center
            );

        Engine.DrawString(
            "Game Developer - Arpan Agrawal",
            new Vector2(Resolution.X / 2, 50 + spacing * 3),
            Color.White,
            size3Font,
            TextAlignment.Center
            );
        Engine.DrawString(
            "ALL SOUNDS CREDITS TO FREESOUND.ORG",
            new Vector2(Resolution.X / 2, 50 + spacing * 4),
            Color.White,
            size3Font,
            TextAlignment.Center
            );


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
