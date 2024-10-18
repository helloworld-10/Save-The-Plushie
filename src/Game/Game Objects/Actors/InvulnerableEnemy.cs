using System;
using System.Collections.Generic;
using System.Text;

class InvulnerableEnemy : Actor
{ 
    Player player;
    int range;
    Vector2 initialPos;
    Vector2 tempVelocity = new Vector2(20, 0);


    public InvulnerableEnemy(Vector2 pos, Player p, int range, String spriteLoc = "..\\Assets\\Actors\\Invulnerable Enemy.png") : base(pos, spriteLoc : spriteLoc, numFrames: 1)

    {
        player = p;
        this.position = pos;
        initialPos = pos;
        this.range = range;
        this.velocity = new Vector2(20, 0);
    }
    public override void update(float time)
    {
        this.velocity = tempVelocity;

        if (initialPos.X - range > position.X) // if enemy too far to left
        {
            tempVelocity = new Vector2(20, 0);
        }
        else if (initialPos.X + range < position.X) // if enemy too far to right
        {
            tempVelocity = new Vector2(-20, 0);
        }

        if (boundsBox.Overlaps(player.boundsBox))
        {
            // communicate to player they have been hit
            isGameOver = ((Player)player).isHit(this);
        }
    }
}

