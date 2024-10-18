using System;
using System.Collections.Generic;
using System.Text;

class ScreenManager
{
    public Stack<Screen> screens = new Stack<Screen>();
    public static Screen currentScreen;
    public Boolean canShowPositions;

    public ScreenManager(Screen start) 
    {
        canShowPositions = false; 
        addScreen(start);
    }

    public void addScreen(Screen s)
    {
        if (currentScreen != null)
        {
            screens.Push(currentScreen);
        }

        currentScreen = s;
    }

    //Function which displays player and enemy positions on screen. 
    public void showPositions()
    {
        currentScreen.drawPositions();
    }
    public void deleteScreen() {
        if (screens.Count > 0)
        {
            currentScreen = screens.Pop();
        }
        else 
        {
            currentScreen = null;
        }
        
    }

    public void manageInput(bool isMouseClicked, Vector2 mousePos) // call this method to Game.cs (MAIN)
    {

        if (currentScreen != null)
        {
            currentScreen.draw();
            Screen newScreen = currentScreen.checkActivity(isMouseClicked, mousePos);

            // deletes screen from stack
            if (newScreen == Screen.Empty)
            {
                deleteScreen();
            }
            else if (newScreen != null) // adds new screen to stack
            {
                if (newScreen.addOnScreen) 
                {
                    canShowPositions = false; 
                    addScreen(newScreen); 
                }
                else
                {
                    canShowPositions = false; 
                    deleteScreen();
                    addScreen(newScreen);
                }
            }

            if (canShowPositions)
            {
                showPositions();
            }
        }
    }

    public bool isStartGame() 
    {
        return currentScreen == null;
    }
}
