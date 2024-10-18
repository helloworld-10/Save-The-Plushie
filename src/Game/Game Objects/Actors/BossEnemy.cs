using System;
using System.Collections.Generic;
using System.Text;

class BossEnemy : Actor
{
    //bad enemy    
    Player p;

    private bool isAttacking = false;
    private float timeElapsed;
    public List<GameObject> allGameObjects;
    Texture localTexture;
    public int killCounter;
    public Boolean isDead; 

    public BossEnemy(Vector2 pos, Player p, List<GameObject> allGameObjects, String spriteLoc = "Actors\\Deer.png") : base(pos, spriteLoc: spriteLoc, numFrames : 1)
    {
        type = "Boss Enemy";
        this.p = p;
        position = pos;
        this.velocity = new Vector2(0, 0);
        //creates bounds box 3x bigger than normal
        this.size = new Vector2(32 * 3, 32 * 3);
        this.boundsBox.Size = new Vector2(32 * 3, 32 * 3);
        localTexture = Engine.LoadTexture("Actors\\Deer.png");
        sprite = new Sprite(new Vector2(32, 32), this.size, localTexture, 1);
        this.allGameObjects = allGameObjects;
        killCounter = 0; 

    }

    //Used for enemy movement and check if enemy is still alive or not
    public override void update(float time)
    {

        int vision = 800;

        if ((this.position - p.position).Length() < vision)
        {
            Vector2 v = (new Vector2(this.position.X - p.position.X, this.position.Y - p.position.Y).Normalized());
            this.setVelocity(v * -10f); // speed of bad enemy
        }
        this.addForce(this.velocity.Normalized() * 0.2f, time);
        if ((this.position - p.position).Length() < vision) // spots player and chases after them
        {
            isAttacking = true;
        }

        else
        {

            timeElapsed = 0;
            isAttacking = false;
        }

        if (isAttacking)
        {
            timeElapsed += Engine.TimeDelta;
        }

        foreach (Projectile obj in p.bullets)
        {
            if (boundsBox.Overlaps(obj.boundsBox))
            {
                killCounter += 1; 
            }
        }

        if(killCounter >= 30)
        {
            allGameObjects.Remove(this);
        }

    }


    public override void resolveColl(GameObject obj, Physics.collData data)
    {
        //ends the game if colliding with player
        if (obj.Name == "Player")
        {
            isGameOver = ((Player)obj).isHit(this);
        }
    }
}

