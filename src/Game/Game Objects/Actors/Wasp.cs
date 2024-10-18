using System;
using System.Collections.Generic;
using System.Text;

    class Wasp : Actor
    {
    // bad enemy (the wasp flies and attacks player)
    Player p;
    private bool isAttacking = false;
    private float timeElapsed;
    TimingClass timer;
    // default params
    public Wasp(Vector2 pos, Player p, String spriteLoc = "Actors\\Enemy 2.png") : base(pos, spriteLoc: spriteLoc, numFrames: 4)
    {
        type = "enemy"; 
        this.p = p;
        this.position = pos;
        this.velocity = new Vector2(0, 0);
        this.timer = new TimingClass();
    }
    public override void update(float time)
    {
        // vision is how far the wasp can see
        int vision = 200;
        // checks if it can see player
        if (Math.Abs(HelperClass.signedDistance(this, this.p)) < vision)
        {
            // goes toward player
            Vector2 v = (new Vector2((float)HelperClass.signedDistance(this, this.p) * 1, this.position.Y - p.position.Y).Normalized());
            this.setVelocity(v * -5f); // speed of bad enemy
            // at certain increments, increases speed of wasp that it travels
            if( (((int) TimingClass.timeElapsed()) / 5) % 2 == 0)
            {
                this.setVelocity(v * -20f);
            }
        }
        // adds force to player velocity
        this.addForce(this.velocity.Normalized() * 0.2f, time);
        if (Math.Abs(HelperClass.signedDistance(this, this.p)) < vision) // spots player and chases after them
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
            // ADD FLICKERING ANIMATION HERE 
            timeElapsed += Engine.TimeDelta;
        }           
    }
    // if it hits the player, sets game to be over
    public override void resolveColl(GameObject obj, Physics.collData data)
    {
        if(obj.Name == "Player")
        {
            isGameOver = ((Player)obj).isHit(this);
        }
    }
}

