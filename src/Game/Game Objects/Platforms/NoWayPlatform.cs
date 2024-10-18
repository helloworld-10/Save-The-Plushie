using System;
using System.Collections.Generic;
using System.Text;

class NoWayPlatform : Platform
{
    public NoWayPlatform(Vector2 position, String spriteLoc = "Platforms\\No Way Platform - Branch.png", String typeGround = "none") : base(position, spriteLoc : spriteLoc) 
    {
        //Console.Write(typeGround);
        base.position = position;
        Texture localTexture;
        if (typeGround == "none")
        {
            localTexture = Engine.LoadTexture("Platforms\\No Way Platform - Branch.png"); // 32, 32 bit 
        }
        else if (typeGround.Substring(typeGround.Length - 5, 1) == "1")
        {
            localTexture = Engine.LoadTexture("Background\\Level 1 Floor.png"); // 32, 32 bit 
            //Console.Write("got 1");
        }
        else if (typeGround.Substring(typeGround.Length - 5, 1) == "2")
        {
            localTexture = Engine.LoadTexture("Background\\Level 2 Floor.png"); // 32, 32 bit 
            //Console.Write("got 2");
        }
        else if (typeGround.Substring(typeGround.Length - 5, 1) == "3")
        {
            localTexture = Engine.LoadTexture("Background\\Level 3 Floor.png"); // 32, 32 bit 
            //Console.Write("got 3");
        }
        else
        {
            localTexture = Engine.LoadTexture("Background\\Level 1 Floor.png"); // 32, 32 bit
        }

        sprite = new Sprite(new Vector2(32, 32), this.size, localTexture, 1);

    }

    public override void updatePlatform(Player pl)
    {
        base.updatePlatform(pl);
    }
    public override void resolveColl(GameObject gameObject, Physics.collData data)
    {
        

    }


}
