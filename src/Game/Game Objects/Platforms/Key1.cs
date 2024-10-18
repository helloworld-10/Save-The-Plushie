using System;
using System.Collections.Generic;
using System.Text;
class Key1 : Platform
{
    public Key1(Vector2 position, String spriteLoc = "Platforms\\Key.png") : base(position, spriteLoc : spriteLoc)
    {
        base.position = position;
    }

    public override void updatePlatform(Player pl)
    {
        base.updatePlatform(pl);
    }

    //if the player has the key then hasKey is set to true which allows the player to go through the door. The key disappears from the map. 
    public override void resolveColl(GameObject gameObject, Physics.collData data)
    {
        if (boundsBox.Overlaps(gameObject.boundsBox)) // checks for tunneling
        {
            position = new Vector2(0, -100);
            gameObject.hasKey = true;
        }
    }

}

