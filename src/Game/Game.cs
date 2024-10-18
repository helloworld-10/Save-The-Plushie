using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Numerics;
using System.IO;

// ALL SOUNDS CREDITS TO FREESOUND.ORG

class Game
{
    public static readonly int BlockSize = 32;
    public static readonly string Title = "Minimalist Game Framework";
    public static readonly Vector2 Resolution = new Vector2(640, 480);
    public static ScreenManager screenManager;


    public Game()
    {
        screenManager = new ScreenManager(new StartScreen(Resolution));
    }

    public void Update()
    {
        screenManager.manageInput(Engine.GetMouseButtonUp(MouseButton.Left), Engine.MousePosition);

        //Skips which makes it easy for developers to test out levels. 
        if(Engine.GetKeyDown(Key.NumRow0)) { screenManager.addScreen(new GameLevelScreen(Resolution, 0)); }
        if (Engine.GetKeyDown(Key.NumRow1)) { screenManager.addScreen(new GameLevelScreen(Resolution, 1)); }
        if (Engine.GetKeyDown(Key.NumRow2)) { screenManager.addScreen(new GameLevelScreen(Resolution, 2)); }
        if (Engine.GetKeyDown(Key.NumRow3)) { screenManager.addScreen(new GameLevelScreen(Resolution, 3)); }
        if (Engine.GetKeyDown(Key.NumRow6)) { screenManager.addScreen(new GameLevelScreen(Resolution, 4)); }

        //These conditionals are used to toggle on or off player, enemy, and boss positions friom screen. Press 4 to toggle positions on and 5 to toggle positions off. 
        if (Engine.GetKeyDown(Key.NumRow4)) { screenManager.canShowPositions = true; }
        if (Engine.GetKeyDown(Key.NumRow5)) { screenManager.canShowPositions = false; }


    }
}