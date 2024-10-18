using System;
using System.Collections.Generic;
using System.Text;

class GameObject
{
    // useful params for a game object
    public Vector2 position;
    public Vector2 size = new Vector2(32, 32);
    public Bounds2 boundsBox;
    public Vector2 velocity;
    public bool isActor = false;
    public bool isPickable = false; // whether we can pick up the object
    public float depth; // when we render it
    public int level;
    public String Name =""; // type of object
    public Boolean hasKey;
    public Boolean immovable = false;
    public Boolean fricCol;
    public String type;
    public String spriteLoc; // the file location of the object
    public Sprite sprite;
    public int numFrames; // how many animations we animate the game object (default 1, meaning no animation)
    public Color color;

    public GameObject(Vector2 position, float depth = 1, int level = 3, String spriteLoc = null, int numFrames = 1, Vector2? oSize = null, Vector2? pixelSize = null)
    {

        if (oSize == null)
        {
            this.size = new Vector2(32, 32); // default size
        }
        else
        {
            this.size = (Vector2)oSize; // otherwise set what size user implements
        }

        if (pixelSize == null)
        {
            pixelSize = new Vector2(32, 32); // default
        }

        // setting params
        this.position = position;
        this.depth = depth;
        this.boundsBox = new Bounds2(position, size);
        this.level = level;
        this.type = "game obj";
        this.spriteLoc = spriteLoc; 
        this.numFrames = numFrames;
        this.color = Color.White;

        // if user provides a sprite location in file dir, set spire
        if (spriteLoc != null)
        {
            List<int> frames = new List<int>();
            Texture texture = Engine.LoadTexture(spriteLoc); // 32, 32 bit 
            // adds the frames to the sprite class (which the gameobject owns)
            this.sprite = new Sprite((Vector2)pixelSize, this.boundsBox.Size, texture, numFrames);
        }
    }
    //called upon collision
    public virtual void resolveColl(GameObject gameObject,Physics.collData data) { }
    //updates every frame
    public virtual void update(float time) { }
    // this function draws the object to the screen
    public virtual void drawObject()
    {
        // if has no sprite, just draws a red square instead
        if(spriteLoc == null)
        {
            this.boundsBox = new Bounds2(position, size);
            Engine.DrawRectSolid(this.boundsBox, Color.Red);
        }
        else
        {
            // if this is an actor that moves, then mirrors the sprite based on the direction of the actor
            TextureMirror shouldMirror = TextureMirror.None;
            if (this is Actor)
            {
                Actor actor = (Actor)this;
                if(actor.direction == -1)
                {
                    shouldMirror = TextureMirror.Horizontal;
                }
            }

            // draws the spirite to the screen
            sprite.spriteDisplaySize = boundsBox.Size;
            sprite.DrawTexture(this.position,this.color, mirror: shouldMirror, source: boundsBox);
        }
        
    }
}

