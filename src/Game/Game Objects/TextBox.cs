using System;
using System.Collections.Generic;
using System.Text;

class TextBox : GameObject
{

    //Creates text which is used to display information on screen. 

    public bool isGameOver = false;
    public String type = "Text_Box";

    public static Font size3Font = Engine.LoadFont("Retro Gaming.ttf", 12);
    public String tempStr;

    public TextBox(String str, Vector2 position, String spriteLoc = null) : base(position, spriteLoc:spriteLoc)
    {
        tempStr = str;
        base.position = position;
        base.immovable = true;
        base.boundsBox = new Bounds2(Vector2.Zero, Vector2.Zero);
        
    }

    public void updateString(String newStr) 
    {
        tempStr = newStr;
    }

    public override void drawObject() 
    {
        Engine.DrawString(
                tempStr,
                base.position,
                Color.White,
                size3Font,
                TextAlignment.Left
            );
    }

}
