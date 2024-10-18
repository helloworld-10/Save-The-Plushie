using System;
using System.Collections.Generic;
using System.Text;


    class Physics
    {
    //struct for data from collision
    public struct collData
    {
        public Vector2 xy, init, initO;
    }
    //update function
    public static void update(List<GameObject> objects, List<Platform> platfoms, float dt, Player p)
    {
        //loops through all game objects and updates position and calls update function of all the objects
        for(int i = 0; i < objects.Count; i++)
        {
            GameObject g = objects[i];            
            g.position = g.position + g.velocity * 5f*dt;
            g.update(dt);

        }
        //calls detect collision function
        detectCollisions(objects,dt);
        updatePlatforms(platfoms, p); 
        }

    private static void updatePlatforms(List<Platform> platfoms, Player p)
    {
        for (int j = 0; j < platfoms.Count; j++)
        {
            platfoms[j].updatePlatform(p);
        }
    }
    private static void detectCollisions(List<GameObject> objects, float dt)
    {
        //loops through all possible pairs of game objects
        for (int i = 0; i < objects.Count; i++)
        {
            for (int j = i; j < objects.Count; j++)
            {
                //checks if they overlapon the same level and are unique objects
                if (objects[i].boundsBox.Overlaps(objects[j].boundsBox) && !objects[i].Name.Equals(objects[j].Name) && objects[i].depth == objects[j].depth)
                {
                    //calculates the min distance needeed to move out in the x dir
                    float x = Math.Min(
                        Math.Abs(objects[i].position.X - (objects[j].position.X + objects[j].boundsBox.Size.X)), 
                        Math.Abs(objects[j].position.X - (objects[i].position.X + objects[i].boundsBox.Size.X)));
                    //calculates the min distance needeed to move out in the y dir
                    float y = Math.Min(Math.Abs(objects[i].position.Y - (objects[j].position.Y + objects[j].boundsBox.Size.Y)),
                        Math.Abs(objects[j].position.Y - (objects[i].position.Y + objects[i].boundsBox.Size.Y)));
                    //loads collision data into the structs
                    Vector2 initi = new Vector2(objects[i].position.X, objects[i].position.Y);
                    Vector2 initj = new Vector2(objects[j].position.X, objects[j].position.Y);
                    collData collDatai = new collData();
                    collDatai.xy = new Vector2(x, y);
                    collDatai.init = initj;
                    collDatai.initO = initi;
                    collData collDataj = new collData();
                    collDatai.xy = new Vector2(x, y);
                    collDatai.init = initi;
                    collDatai.initO = initj;
                    //chooses which object to move based on immovable boolean
                    if (objects[j].immovable && objects[j].boundsBox.Size.X != 0) {
                        //chooses which direction to move out from
                        if (x < y)
                        {
                            //Moves object out in the direction by the amount
                            objects[i].position.X += Math.Sign(objects[i].position.X - objects[j].position.X) * (x+.0001f);
                        }
                        else
                        {
                            objects[i].position.Y += Math.Sign(objects[i].position.Y - objects[j].position.Y) * (y+.0001f);
                        }
                    }
                    else if (objects[i].boundsBox.Size.X != 0)
                    {
                        //chooses which direction to move out from
                        if (x < y)
                        {
                            objects[j].position.X += Math.Sign(objects[j].position.X - objects[i].position.X) * (x+.0001f);
                        }
                        else
                        {
                            objects[j].position.Y += Math.Sign(objects[j].position.Y - objects[i].position.Y) * (y+.0001f);
                        }
                    }
                    //calls collision function of the two objects in the collision pair
                    objects[i].resolveColl(objects[j],collDatai);
                    objects[j].resolveColl(objects[i],collDataj);
                }
            }
        }
        
       
    }
}

