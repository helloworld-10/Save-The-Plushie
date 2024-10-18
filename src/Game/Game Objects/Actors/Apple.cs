using System;
using System.Collections.Generic;
using System.Text;

class Apple : Actor
{
    // player
    Player p;
    public bool isGameOver = false;
    public List<GameObject> allGameObjects;
    public TimingClass appleTimer = new TimingClass(.01);
    public Vector2 originalPos;

    // default params
    public Apple(Vector2 position, Player p, List<GameObject> allGameObjects, String spriteLoc = "Actors\\apple.png"): base(position, spriteLoc : spriteLoc, numFrames: 1){
        type = "apple";
        this.p = p;
        this.position = position;
        originalPos = position;
        this.velocity = new Vector2(0, 0);
        this.allGameObjects = allGameObjects;
        base.boundsBox.Size.X = 16;
        base.boundsBox.Size.Y = 16;
        base.size.X = 16;
        base.size.Y = 16;
        // adds it to a list of all gameobjects
        allGameObjects.Add(this);
    }
    // moves after discrete time
    public override void update(float time)
    {
        if (appleTimer.timeLimitReached())
        {
            if(this.position.Y == 500)
            {
                this.position.Y = 0;
            }
            this.addForce(new Vector2(0, 5f), time); // gravity

        }
        else
        {
            this.position.X = p.position.X;
            this.position.Y = 500;
        }
    }


    public override void resolveColl(GameObject obj, Physics.collData coll)
    {
        if (obj.Name == "Player")
        {
            Console.Write("TRIGGER");
            Actor.gameOver = ((Player)obj).isHit(this);
        }
        if (obj.Name == "platform" && position.Y + boundsBox.Size.Y <= obj.position.Y)
        {
            velocity.Y = 0;
            position = originalPos;
            appleTimer.resetTimer(10);

        }
        else if (obj.Name == "platform" && velocity.Y < 0)
        {
            velocity.Y = 0;
            position = originalPos;
            appleTimer.resetTimer(10);
        }
    }
}
