using System;
using System.Collections.Generic;
using System.Text;

// screen displayed when player looses
class LoseScreen : Screen
{
    // button so player can go back to home

    Button exitButton = new Button(
            new Vector2(0, 0),
            new Vector2(60, 25),
            "Exit!",
            Color.Black
            );
   

    public LoseScreen(Vector2 resolution) : base(resolution)
    {

    }

    // draws the platform
    public override void draw()
    {
        Engine.DrawRectSolid(boundsBox, Color.Red); // background

        // lose string
        Engine.DrawString(
            "LOSE!",
            new Vector2(Resolution.X / 2, Resolution.Y / 2),
            Color.White,
            size1Font,
            TextAlignment.Center
            );

        exitButton.draw(size3Font);
    }

    // checks if player clicked to go to the StartScreen and takes them there
    public override Screen checkActivity(bool isMouseClicked, Vector2 mousePos)
    {
        if (exitButton.isClicked(isMouseClicked, mousePos)) 
        {
            return new StartScreen(Resolution);
        }

        return null;
    }
}
