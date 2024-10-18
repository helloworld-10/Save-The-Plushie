using System;
using System.Collections.Generic;
using System.Text;

class Screen
{
    public Vector2 Resolution;
    public Bounds2 boundsBox;
    public static Font size1Font = Engine.LoadFont("Retro Gaming.ttf", 60);
    public static Font size2Font = Engine.LoadFont("Retro Gaming.ttf", 40);
    public static Font size3Font = Engine.LoadFont("Retro Gaming.ttf", 20);
    public static Font size4Font = Engine.LoadFont("Retro Gaming.ttf", 9);

    public static readonly Screen Empty = new Screen(Vector2.Zero);
    public bool addOnScreen = false;
    public List<GameObject> allGameObjects;
    public Screen(Vector2 resolution) 
    {
        boundsBox = new Bounds2(Vector2.Zero, resolution);
        Resolution = resolution;
    }

    public virtual void draw() { }

    public virtual void drawPositions() { }
    public virtual Screen checkActivity(bool isMouseClicked, Vector2 mousePos) 
    {
        return null;
    }
}
