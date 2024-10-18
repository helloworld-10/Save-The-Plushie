using System;
using System.Collections.Generic;

using System.Text;

class Camera
{
    // array list of all objects in the game
    List<GameObject> allObjects;

    public Camera(List<GameObject> allObjects)
    {
        this.allObjects = allObjects;
    }

    public void showScreen(Vector2 Resolution, GameObject Player)
    {
        // map size is 1920 pixels long
        // visual display is 640 pixels long

        float xVisualDisplay = Resolution.X;
        float xMapSize = Resolution.X * 3;

        // if the player goes out of the map, because it is cylindrical, we just teleport them back to the start
        // Player.position.X = Math.Sign(Player.position.X) * -1 * xMapSize + Player.position.X;
        
        float maximum = Math.Max(Player.position.X, xMapSize);
        float minimum = Math.Min(Player.position.X, 0);
        float selfX = Player.position.X;

        Player.position.X = Math.Sign(maximum-xMapSize) * (maximum - xMapSize - selfX) - Math.Sign(minimum) * (xMapSize + minimum - selfX) + selfX;

        // resetting actor positions
        
        foreach(Actor act in Actor.allActors)
        {
            if (act.type != null && act.type.Contains("enemy"))
            {
                if(act.position.X > xMapSize)
                {
                    act.position.X = act.position.X - xMapSize;
                }
                if(act.position.X < 0)
                {
                    act.position.X = act.position.X + xMapSize;
                }
             }
        }
        

        // camera is the size of the screen, with x & y = 0

        Bounds2 camera = new Bounds2(Vector2.Zero, Resolution);

        // iterate over all game objects
        foreach(GameObject obj in allObjects)
        {
            for (int i = 0; i < 4; i++)
            {
                obj.position.X += xMapSize;
                // memerize the original X positions of the object
                double ogX = obj.position.X;

                // find how much the new X position is, when the player moves
                // if player moves to the right 5 units, object shifts 5 units to left
                // multiply by object depth for parallax effect, 1 being no parallax effect

                double newX = (ogX - Player.position.X) * obj.depth + Resolution.X / 2;

                // make the obj store the new x position
                obj.position.X = (float)newX;
                obj.boundsBox.Position = obj.position;

                // check if the object, shifted, overlaps with camera (and thus should be displayed)
                //if (camera.Overlaps(obj.boundsBox))
                //{
                    // draw the object
                    obj.drawObject();
                //}
                // set the objects position to the original x coordinates
                obj.position.X = (float)ogX;
                obj.boundsBox.Position = obj.position;
                if (i == 1)
                {
                    obj.position.X -= 4 * xMapSize;
                }
            }
        }
    }
}

