using System;
using System.Collections.Generic;
using System.Text;

class RulesScreen : Screen
{
    Button exitButton = new Button(
            new Vector2(0, 0),
            new Vector2(60, 25),
            "Exit!",
            Color.Red
            );

    float spacing = 42;

    public RulesScreen(Vector2 resolution) : base(resolution)
    {
        addOnScreen = true;
    }

    public override void draw()
    {
        Engine.DrawRectSolid(boundsBox, Color.Blue); // background

        Engine.DrawString(
            "Rules Screen!",
            new Vector2(Resolution.X / 2, 0),
            Color.White,
            size1Font,
            TextAlignment.Center
            );

        int subtitlePlacement1 = 2;

        Engine.DrawString(
            "Movement Controls:",
            new Vector2(0, (float)(spacing * subtitlePlacement1)),
            Color.White,
            size2Font,
            TextAlignment.Left
            );

        Engine.DrawString(
            "SPACE - jump",
            new Vector2(0, (float)(spacing * (subtitlePlacement1 + 2))),
            Color.White,
            size3Font,
            TextAlignment.Left
            );

        Engine.DrawString(
            "A - move left",
            new Vector2(0, (float)(spacing * (subtitlePlacement1 + 2.5))),
            Color.White,
            size3Font,
            TextAlignment.Left
            );

        Engine.DrawString(
            "D - move right",
            new Vector2(0, (float)(spacing * (subtitlePlacement1 + 3))),
            Color.White,
            size3Font,
            TextAlignment.Left
            );

        Engine.DrawString(
            "P - throw tomatoes",
            new Vector2(0, (float)(spacing * (subtitlePlacement1 + 3.5))),
            Color.White,
            size3Font,
            TextAlignment.Left
            ); 

        Engine.DrawString(
            "SHIFT - duck",
            new Vector2(0, (float)(spacing * (subtitlePlacement1 + 4))),
            Color.White,
            size3Font,
            TextAlignment.Left
            );

        int subtitlePlacement2 = subtitlePlacement1 + 5;

        Engine.DrawString(
            "Objective:",
            new Vector2(0, (float)(spacing * subtitlePlacement2)),
            Color.White,
            size2Font,
            TextAlignment.Left
            );

        Engine.DrawString(
            "1) collect all of the items in the level",
            new Vector2(0, (float)(spacing * (subtitlePlacement2 + 2))),
            Color.White,
            size3Font,
            TextAlignment.Left
            );

        Engine.DrawString(
            "2) catch the animal with your plushie",
            new Vector2(0, (float)(spacing * (subtitlePlacement2 + 2.5))),
            Color.White,
            size3Font,
            TextAlignment.Left
            );

        Engine.DrawString(
            "3) avoid enemies!",
            new Vector2(0, (float)(spacing * (subtitlePlacement2 + 3))),
            Color.White,
            size3Font,
            TextAlignment.Left
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
