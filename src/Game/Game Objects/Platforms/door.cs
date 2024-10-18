using System;
using System.Collections.Generic;
using System.Text;

class door : Platform
{
    public door(Vector2 position, String spriteLoc = "Platforms\\Door.png") : base(position, spriteLoc: spriteLoc)
    {
        this.size = new Vector2(50, 500);
        base.position = position;
    }

    public override void updatePlatform(Player pl)
    {
        base.updatePlatform(pl);
    }

    //If player has the key then the door opens. 
    public override void resolveColl(GameObject gameObject, Physics.collData data)
    {
        if (boundsBox.Overlaps(gameObject.boundsBox)) // checks for tunneling
        {
            if (gameObject.hasKey)
            {
                position = new Vector2(0, -1000000);

            }

        }
    }
}