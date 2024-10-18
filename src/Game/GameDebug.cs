using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input; 

class GameDebug
{

    public void HandleKeyPress(Key k)
    {
        switch (k)
        {
            case Key.A:
                //System.Console.WriteLine("Key A Presed");
                break; 

            case Key.D:
                //System.Console.WriteLine("Key D Pressed");
                break;

            case Key.V:
                //System.Console.WriteLine("Key V Pressed");
                break;

            case Key.Space:
                //System.Console.WriteLine("Space key Pressed");
                break; 
        }
    }
    public void debugPrintKey(Actor g,Key k)
    {
        if (k != null) {
            //System.Console.WriteLine("Position: " + g.position.ToString() + "   Velocity: " + g.velocity.ToString() + "     Inputs: " +Engine.GetKeyDown(k));
        }
        else
        {
            //System.Console.Write("Position: " + g.position.ToString() + "   Velocity: " + g.velocity.ToString());
        }
    }
    public void debugPrint(Actor g)
    {
        
           //System.Console.WriteLine(g.type + "Position: " + g.position.ToString() + "   Velocity: " + g.velocity.ToString());
        
    }
    //public static void printAll()
    //{
        //foreach(GameObject g in GameObject.allGameObjects)
        //{
            //debugPrint((Actor)g);
        //}
    //}
}
