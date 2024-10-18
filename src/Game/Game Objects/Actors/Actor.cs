using System;
using System.Collections.Generic;
using System.Text;

class Actor : GameObject
{

    //public Vector2 velocity;

    public bool isGameOver;
    public String type;
    public int direction;
    public static List<Actor> allActors = new List<Actor>();
    public static bool gameOver = false;

    public Actor(Vector2 position, String spriteLoc = null, int numFrames = 1) : base(position, spriteLoc:spriteLoc, numFrames : numFrames)

    {
        base.position = position;
        base.depth = 1;
        this.velocity = new Vector2(0, 0);
        base.boundsBox = new Bounds2(position, size);
        base.isActor = true;
        allActors.Add(this);
        
    }

    //Function used to change velocity of object 
    public void setVelocity(Vector2 velocity)
    {
        this.velocity = velocity;
        if (Math.Sign(this.velocity.X) == 1)
        {
            direction = 1;
        }
        else
        {
            direction = -1;
        }
    }

    //Function used to accelerate player
    public void addForce(Vector2 accel, float time)
    {
        velocity += accel * time;
        if(Math.Sign(this.velocity.X) == 1)
        {
            direction = 1;
        }
        else
        {
            direction = -1;
        }
    }

    public override void drawObject()
    {
        base.drawObject();
    }
}
