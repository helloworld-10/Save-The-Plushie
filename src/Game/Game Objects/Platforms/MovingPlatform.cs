using System;
using System.Collections.Generic;
using System.Text;

class MovingPlatform : Platform
{
    //maxMovement = the maximum right position
    //rightReached = boolean which is true if maxMovement is reached and false if not. 

    public float maxMovement;
    public Boolean rightReached;

    public MovingPlatform(Vector2 position, String spriteLoc = "Platforms\\Moving Platform.png") : base(position, spriteLoc: spriteLoc)
    {
        base.position = position;
        maxMovement = position.X + 150f;
        rightReached = false;
    }

   
    public override void updatePlatform(Player pl)
    {
        //Conditionals which determine if the platform moves left or right. 
        if (position.X >= maxMovement)
        {
            rightReached = true;
        }
        if (position.X <= maxMovement - 150f)
        {
            rightReached = false;
        }

        if (rightReached == false)
        {
            //Conditional which checks if player is on platform. 
            if (Math.Abs(pl.position.Y - (position.Y - pl.size.Y - 1)) < 1 && (pl.position.X <= position.X + 2 * size.X && pl.position.X >= position.X - 2 * size.X))
            {
                pl.position.X += 1f; //If player is on platform then player moves with platform. 


            }


            position.X += 3f; //moves platform 
        }
        else
        {
            //Conditional which checks if player is on platform. 
            if (Math.Abs(pl.position.Y - (position.Y - pl.size.Y - 1)) < 1 && (pl.position.X <= position.X + 2 * size.X && pl.position.X >= position.X - 2 * size.X))
            {
                pl.position.X -= 1f; //If player is on platform then player moves with platform. 

            }

            position.X -= 3f; //moves platform 
        }

    }
    public override void resolveColl(GameObject gameObject, Physics.collData data)
    {

    }

}