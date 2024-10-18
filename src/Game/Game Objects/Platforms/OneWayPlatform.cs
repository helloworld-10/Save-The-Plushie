using System;
using System.Collections.Generic;
using System.Text;

class OneWayPlatform : Platform
{

    public OneWayPlatform(Vector2 position, String spriteLoc = "Platforms\\One Way Platform - Leaf.png") : base(position, spriteLoc : spriteLoc)
    {
        type = "owp";
        base.position = position;
    }

    public override void updatePlatform(Player pl)
    {
        base.updatePlatform(pl);
    }
}
