using System;
using System.Collections.Generic;
using System.Text;

class Player : Actor
{

    private float timeElapsed;
    float jumpFactor = (float)1.0;
    // debug state
    private GameDebug gd;

    // potions & checking time
    public bool hasSpeedPotion = false;
    public bool timingActive = false;
    public float speed = 20f;

    // checking props of player
    public TimingClass sTime;
    public List<Projectile> bullets;
    public List<Projectile> deleteBullets;
   
    public List<GameObject> allGameObjects;
    // sound magic
    SoundManager soundManager = new SoundManager();
    public bool grav = true;
    public TimingClass noDamageTimer;

    // checking if has armor / invincible / whether game is over
    public bool isInvincible = false;
    public bool isGameOver = false;
    public bool hasArmor;

    // adding timer for bullet
    public TimingClass bulletTimer = new TimingClass(.01);

    // enables double jump
    public int countBulletsForDoubleJump = 0;
    bool isDoubleJump = false;

    // player def headers
    public Player(Vector2 pos, List<GameObject> allGameObjects, String spriteLoc = "..\\Assets\\Actors\\Player.png") : base(pos, spriteLoc : spriteLoc, numFrames: 4)

    {
        // setting default params
        Name = "Player";
        type = "Player";
        this.position = pos;
        base.fricCol = false;
        gd = new GameDebug();
        // player owns the bullets it shoots
        bullets = new List<Projectile>();
        // makes sure to delete bullets after it uses them
        deleteBullets = new List<Projectile>();
        this.allGameObjects = allGameObjects;
        grav = true;
        // size of player
        boundsBox.Size.X = 24;
        this.boundsBox.Size = new Vector2(100, 100);
    }

    
    // func for when player is collided, returns whether collision is deadly or not
    public bool isHit(GameObject obj)
    {
        
        // if has armor, then player does not lose
        if (hasArmor)
        {
            Console.Write("TRIGGERED");
            // makes them temporarily invincible
            hasArmor = false;
            isInvincible = true;
            noDamageTimer = new TimingClass(2);
            ((GameLevelScreen)ScreenManager.currentScreen).allGameObjects.Remove(obj);
            ((GameLevelScreen)ScreenManager.currentScreen).allGameObjects.Remove(obj);

            /*
            public List<Actor> actorsList;
            public List<Token> tokenList;
            public List<Actor> allEnemies;
            */
        }
        else if(!isInvincible){
            return true;
        }

        return false;

    }
    

    // updating player movement over discrete times
    public override void update(float time)
    {

        // if has the speed potion, increase their jump height temporarily
        if (hasSpeedPotion)
        {
            Console.Write("HAS SPPEED POTION");

            jumpFactor = (float)2 * jumpFactor;
            hasSpeedPotion = false;
            // only increase the jump height for a certain time
            sTime = new TimingClass(5);
            timingActive = true;
        }
        if (hasArmor)
        {
            // changes color of player according to their armor color
            this.color = Color.Brown;

        } else if (isDoubleJump) {
            this.color = Color.HotPink;
        } else {
            // sets player no color be default
            this.color = Color.White;
        }

        // checks if timing is done for jump potion, and then ends it
        if (timingActive)
        {
            if (sTime.timeLimitReached())
            {
                jumpFactor = 1.0f;
                timingActive = false;
                sTime = null;
            }
        }

        // checks if player invincible, and turns it off after a certain time
        if (isInvincible)
        {
            if (noDamageTimer.timeLimitReached())
            {
                isInvincible = false;
            }
        }

        // adds gravity
        if (grav)
        {
            this.addForce(new Vector2(0, 50.0f), time); // gravity
        }
        // checks key corresponding for each player movement - left shift moves them right
        if (Engine.GetKeyHeld(Key.LeftShift)) // move right
        {
            this.boundsBox.Size = new Vector2(16, 16);
            this.size = new Vector2(16, 16);
        }
        else
        {
            this.boundsBox.Size = new Vector2(46, 46);
            this.size = new Vector2(46, 46);
        }
        if (Engine.GetKeyHeld(Key.W)) // move right
        {
            this.speed = 30f;
        }
        else
        {
            this.speed = 20f;
        }
        if (Engine.GetKeyHeld(Key.D)) // move right
        {
            if(fricCol == true)
            {
                Key d = Key.D;
                gd.HandleKeyPress(d);

                    base.setVelocity(new Vector2(speed - 10f, base.velocity.Y));
            }
            //direction = 1;

            else
            {
                Key d = Key.D;
                gd.HandleKeyPress(d);


                    base.setVelocity(new Vector2(speed, base.velocity.Y));
            }

            
        }
        else if (Engine.GetKeyHeld(Key.A)) // move left
        {
            //direction = -1;

            if (fricCol == true)
            {
                Key a = Key.A;
                gd.HandleKeyPress(a);

                    base.setVelocity(new Vector2(-speed + 10f, base.velocity.Y));
            }
            //direction = 1;
            else
            {
                Key a = Key.A;
                gd.HandleKeyPress(a);


                    base.setVelocity(new Vector2(-speed, base.velocity.Y));
            }
        }

        // v makes them fly temporarily
        if (Engine.GetKeyDown(Key.V))
        {
            Key v = Key.V;
            gd.HandleKeyPress(v);

            base.setVelocity(new Vector2(30.0f, -30.0f));
        }


        if (Engine.GetKeyUp(Key.P)) // SHOOTING A BULLET
        {
            // waits a certain time before bullet launched to prevent bullet spam
            if (bulletTimer.timeLimitReached())
            {
                bulletTimer.resetTimer(2);
                Projectile bullet = new Projectile(this.position, this, allGameObjects);
                // sounds magic
                soundManager.stopSound("bullet");
                soundManager.addSound("bullet", "Sounds\\tomatoThrow.wav");
                bullets.Add(bullet);

                countBulletsForDoubleJump++;
                if (countBulletsForDoubleJump >= 5) { isDoubleJump = true; }

            }
    
        }

        

        if (!grav)
        {
            velocity.X *= 0.80f;
        }
        grav = true;
        this.boundsBox = new Bounds2(position, size);

        // changes color accordingly if invincible, armor, or doesn't have armor
        if (isInvincible)
        {
            color = Color.Orange;
        }
        else if (!hasArmor)
        {
            color = Color.White;
        }
    }

    // checks what happens if player collides
    public override void resolveColl(GameObject obj, Physics.collData data)
    {
        // maintains player velocity if it is a go through
        if (obj.Name == "platform" && ((Platform)obj).type == "owp" && data.init.Y< obj.position.Y+5+  obj.boundsBox.Size.Y && data.init.Y + boundsBox.Size.Y > obj.position.Y+5)
        {
            this.position.X = data.init.X;
            this.position.Y = data.init.Y;
            return;
        }
        // makes sure y velocity doesn't sink neg (causing to go through platform)
        if (obj.Name == "platform" && velocity.Y>0 && position.Y + boundsBox.Size.Y<obj.position.Y)
        {
            
                velocity.Y = 0;
                grav = false;
        }
        // maintains velocity if go-through platform
        else if(obj.Name == "platform" && velocity.Y <= 0 )
        {
            if (((Platform)obj).type == "owp")
            {
                position.X = data.init.X;
                position.Y = data.init.Y;
            }
            else if( (position.X < obj.position.X + obj.boundsBox.Size.X-5) && (position.X + boundsBox.Size.X > obj.position.X+5))
            {
                velocity.Y = 0;
            }
        }
 
        if (Engine.GetKeyHeld(Key.Space) && !grav) // jump
        {
            soundManager.stopSound("jump");
            soundManager.addSound("jump", "Sounds\\jump.wav");

            fricCol = false;
            Key space = Key.Space;
            gd.HandleKeyPress(space);
            base.setVelocity(new Vector2(0, -45f * jumpFactor));

        }

       
    }
    public List<Projectile> getBullets()
    {
        return bullets;
    }
}