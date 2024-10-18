using System;
using System.Collections.Generic;
using System.Text;

class Sprite
{
    // how big the sprite is (when we are getting the image) and how big the sprite is when we are displaying it
    Vector2 spriteSize;
    Vector2 characterHitSize;
    public Vector2 spriteDisplaySize;
    int frames;
    Texture texture;
    Bounds2 firstFrameSource;
    // the size of pixels & display, the texture, and how many frames (default is 1)
    public Sprite(Vector2 spriteSize, Vector2 spriteDisplaySize, Texture texture, int frames = 1)
    {
        this.texture = texture;
        this.spriteSize = spriteSize;
        this.characterHitSize = spriteSize;
        this.spriteDisplaySize = spriteDisplaySize;
        this.frames = frames;
        this.firstFrameSource = new Bounds2(new Vector2(0, 0), spriteSize);

    }

    // draws (or animates, if given multiple frames) the object
    // setting defaults for blend/scale

    public void DrawTexture(Vector2 position,
                               Color? color = null,
                               TextureMirror mirror = TextureMirror.None,
                               Bounds2? source = null,
                               TextureBlendMode blendMode = TextureBlendMode.Normal,
                               TextureScaleMode scaleMode = TextureScaleMode.Linear)
    {
        // given how much time passed, figures out which frame to draw
        int currentFrame = ((int)Math.Round(TimingClass.timeElapsed() * 10.0)) % frames;
       
        // gets the size of the frame
        Bounds2 frameSource = new Bounds2(new Vector2(currentFrame * 32, 0), spriteSize);
        // draws the frame to the GUI
        Engine.DrawTexture(this.texture, position, color, spriteDisplaySize, 0, null, mirror, frameSource, blendMode, scaleMode);

    }
    
}
