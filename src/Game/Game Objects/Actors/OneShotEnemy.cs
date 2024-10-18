using System;
using System.Collections.Generic;
using System.Text;

class OneShotEnemy : Actor
{
    Player player;
    public Vector2 velo;
    public List<GameObject> allGameObjects;
    public TimingClass timer;
    Vector2 tempVelocity = new Vector2(20, 0);
    Vector2 initialPos;
    int range;

    public OneShotEnemy(Vector2 position, Player p, List<GameObject> allGameObjects, String spriteLoc = "Actors\\One Shot Enemy.png"): base(position, spriteLoc : spriteLoc, numFrames: 1)
    {
        type = "bullet";
        player = p;
        this.position = position;
        this.velocity = new Vector2(10f, 0);
        this.velo = new Vector2(p.direction * 30f, 0);
        this.allGameObjects = allGameObjects;
        allGameObjects.Add(this);
        initialPos = position;
        this.range = 30;
    }
    public override void update(float time)
    {
        bool shouldDel = false;

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

        
        foreach (Projectile obj in player.bullets) 
        {
            if (boundsBox.Overlaps(obj.boundsBox)) 
            {
                shouldDel = true;
            }
        }
        

        if (shouldDel)
        {
            allGameObjects.Remove(this);
        }
    }


}
