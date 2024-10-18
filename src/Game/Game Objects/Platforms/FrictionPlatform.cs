using System;
using System.Collections.Generic;
using System.Text;

class FrictionPlatform : Platform
{
    public String type; 
    public FrictionPlatform(Vector2 position, String spriteLoc = "Platforms\\Friction Platform.png") : base(position, spriteLoc : spriteLoc)
    {
        base.position = position;
    }

    //if the player is on the platform then fricCol is set to true which results in the player's velocity being slowed down. 
    public override void updatePlatform(Player pl)
    {
        if (Math.Abs(pl.position.Y - (position.Y - pl.size.Y - 1)) < 1)
        {
            pl.fricCol = true;
        }
        else
        {
            pl.fricCol = false;
        }

    }
    public override void resolveColl(GameObject gameObject, Physics.collData data)
    {
        

    }


}
