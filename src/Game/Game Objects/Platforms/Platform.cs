using System;
using System.Collections.Generic;
using System.Text;

class Platform : GameObject
{
    
    public Platform(Vector2 position, String spriteLoc = null) : base(position, spriteLoc : spriteLoc)
    {

        base.position = position;
        base.Name = "platform";
        base.immovable = true;
    }

    //function ran which continously updates certain platforms (ex.) moving platform)
    public virtual void updatePlatform(Player pl)
    {

    }




}
