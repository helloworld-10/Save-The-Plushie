using System;
using System.Collections.Generic;
using System.Text;

class Token : Pickable
{
    // default params
    public Token(Vector2 position, String tokenPath) : base(position, ("Tokens\\"+ tokenPath+".png"), "Token")
    {
        // setting the params
        base.position = position;
        base.depth = 1;
        base.boundsBox = new Bounds2(position, size);
        base.isPickable = true;
        // adding tokens to all pickables
        allPickable.Add(this);
    }
}
