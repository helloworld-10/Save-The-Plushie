using System;
using System.Collections.Generic;
using System.Text;

class DestructuablePlatform : Platform
{
    public DestructuablePlatform(Vector2 position, String spriteLoc = "Platforms\\Destructible Platform.png") : base(position, spriteLoc : spriteLoc)
    {
        base.position = position;
    }

    public override void updatePlatform(Player pl)
    {
        base.updatePlatform(pl);
    }

    //If object collides with platform the platform disapears. 
    public override void resolveColl(GameObject gameObject, Physics.collData data)
    {
        if (boundsBox.Overlaps(gameObject.boundsBox)) // checks for tunneling
        {
            position = new Vector2(0, -1000);
        }
    }

}
