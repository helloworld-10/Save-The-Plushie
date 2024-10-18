using System;
using System.Collections.Generic;
using System.Text;

class Button
{
    Vector2 position;
    Vector2 size;
    String text;
    Bounds2 boundBox;
    Color color;

    public Button(Vector2 position, Vector2 size, String text, Color color) 
    {
        this.position = position;
        this.size = size;
        this.text = text;
        boundBox = new Bounds2(position, size);
        this.color = color;
    }

    public void draw(Font f) 
    {
        Engine.DrawRectSolid(boundBox, color);
        Engine.DrawString(text, position, Color.White, f);
    }

    public bool isClicked(bool clickBool, Vector2 mousePos) 
    {
        return clickBool && boundBox.Contains(mousePos);
    }

}
