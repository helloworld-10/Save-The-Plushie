using System;
using System.Collections.Generic;
using System.Text;
// projectile is the tomato/bullet that the player can shoot at enemies

class Projectile : Actor
{
    // player
    Player p;
    private float timeElapsed;
    public bool isGameOver;
    public Vector2 velo;
    public List<GameObject> allGameObjects;
    public TimingClass timer;
    // default params
    public Projectile(Vector2 position, Player p, List<GameObject> allGameObjects, String spriteLoc = "Actors\\bullet.png"): base(position, spriteLoc : spriteLoc, numFrames: 4){
        type = "bullet";
        this.p = p;
        this.position = position;
        this.velocity = new Vector2(10f, 0);
        this.velo = new Vector2(p.direction * 30f, 0);
        this.allGameObjects = allGameObjects;
        this.timer = new TimingClass(3);
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
        // deletes itself after a certain time frame
        bool shouldDel = false;
        if (timer.timeLimitReached())
        {
            shouldDel = true;
        }
        // checks if it hits obj
        foreach(GameObject obj in allGameObjects)
        {
            if (obj.boundsBox.Overlaps(base.boundsBox) && !obj.Equals(this) &&( obj.type=="platform" || obj.type=="enemy") )
            {
                // if so, it deletes / removes itself
                shouldDel = true;
                break;
            }
        }
        if (shouldDel)
        {
            // removes itself from list if it needs to get deleted
            allGameObjects.Remove(this);
            p.deleteBullets.Add(this);
        }
        // moves bullet in direction of velocity
        this.position = this.position + velo * 0.1f;
    }
    public override void resolveColl(GameObject obj, Physics.collData coll)
    {
        if (obj.Name != "Player")
        {
            allGameObjects.Remove(this);
            p.deleteBullets.Add(this);
        }
    }
}
