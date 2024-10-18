using System;
using System.Collections.Generic;
using System.Text;

class Pickable : GameObject
{
    // maintain a list of all pickable times
    public static List<Pickable> allPickable;
    // list for when we want to delete stuff
    public static List<Pickable> needToDelete;
    public String unique;

    static Pickable()
    {
        allPickable = new List<Pickable>();
        needToDelete = new List<Pickable>();
    }
    public Pickable(Vector2 position, String spriteFile, String unique) : base(position, spriteLoc: spriteFile)
    {
        // setting defaults of pickable
        base.position = position;
        base.depth = 1;
        base.boundsBox = new Bounds2(position, size);
        // obviously true, as we can pick it up
        base.isPickable = true;
        // every instance of a pickable is unique - can either be a jump potion, armor, carrot, etc.
        this.unique = unique;
        // adding pickable to ilst
        allPickable.Add(this);
    }
    public override void resolveColl(GameObject obj, Physics.collData data)
    {
        // checks if player picked it up (as this is the only one that can pick it up)
        if (obj.Name == "Player")
        {
            // if speed potion, activate it
            if (this.unique == "speed potion")
            {
                ((Player)obj).hasSpeedPotion = true;
            }
            // if armor, activate it
            if (this.unique == "Armor")
            {
                ((Player)obj).hasArmor = true;
            }

            // after player picked it up, delete it
            needToDelete.Add(this);
            // removes object from global list of all game objects
            ScreenManager.currentScreen.allGameObjects.Remove(this);
        }
        //delete stuff

    }
}
