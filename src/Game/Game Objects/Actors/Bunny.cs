using System;
using System.Collections.Generic;
using System.Text;
class Bunny : Actor
{
    // good enemy
    Player p;
    bool isAttacking;
    private float timeElapsed;
    bool grav = true;
    public bool plushie = false;

    public Bunny(Vector2 pos, Player p, String spriteLoc= "Actors\\Enemy 1.png") : base(pos, spriteLoc:spriteLoc, numFrames:1)
    {
        type = "good enemy"; 
        this.p = p;
        this.position = pos;
        this.velocity = new Vector2(0, 0);
        grav = true;
    }
    public override void update(float time)
    {
        if (!plushie)
        {

            Console.Write(String.Format("{0:.##}", HelperClass.signedDistance(this, this.p)));
            Console.Write("    ");
            Console.Write(String.Format("{0:.##}", this.position.X));
            //Console.Write(this.position.X);
            Console.Write("    ");
            Console.Write(String.Format("{0:.##}", this.p.position.X));
            //Console.Write(this.p.position.X);
            Console.Write("    ");
            Console.Write(Game.Resolution.X * 3);
            Console.Write("\n");

            int vision = 200;

            //toggles running away based on distance to player + adds force of gravity every turn
            if (grav)
            {
                this.addForce(new Vector2(0, 50.0f), time); // gravity
            }

            if (Math.Abs(HelperClass.signedDistance(this, this.p)) < vision)
            {
                isAttacking = true;
            }

            else
            {
                isAttacking = false;
                timeElapsed = 0;
            }
            if (isAttacking)
            {
                // cycle through animation ADD ENEMY FLICKERING HERE
                Vector2 v = new Vector2(Math.Sign(HelperClass.signedDistance(this, this.p)) * 10f, velocity.Y);
                this.setVelocity(v);
                timeElapsed += Engine.TimeDelta;
            }
            else
            {
                velocity.X = velocity.X * 0.8f;
            }

            grav = true;
        }
    }
    public override void resolveColl(GameObject obj, Physics.collData data)
    {
        if(obj.Name == "Player")
        {
            isGameOver = true;
        }
        //platform collision from bottom and top
        if (obj.Name == "platform" && position.Y + boundsBox.Size.Y <= obj.position.Y)
        {
            velocity.Y = 0;
            grav = false;
            
        }
        else if (obj.Name == "platform" && velocity.Y < 0)
        { 
                 velocity.Y = 0;
        }
        //jumps is player is at the same y as bunny
        if (Math.Abs(Math.Abs(this.position.Y - p.position.Y)) < 40 && !grav && isAttacking)
        {
            base.setVelocity(new Vector2(velocity.X, -45f));
        }
    }

}

