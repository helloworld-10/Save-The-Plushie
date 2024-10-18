using System;
using System.Collections.Generic;
using System.Text;
// helper class has a bunch of useful functions that we have used
class HelperClass
{
    // signed distance returns the CLOSEST direction & distance from a player to an actor, accounting for the cylyndrical wrap
    public static double signedDistance(GameObject one, GameObject two)
    {
        // one is ENEMY
        // two is Player
        // output is distance (from player) to Enemy

        // returns the shortest distance between object one and two - if it is to the right of one, return positive, otherwise negative
        float width = Game.Resolution.X * 3;
        float firstX = one.position.X % width;
        float secondX = two.position.X % width;

        float distOne = firstX - secondX;
        float distTwo = 0;
        // checks the case where the enemy is ahead of player
        if(secondX < firstX)
        {
            distTwo = -1 * (secondX + width - firstX);
        }
        else
        {
            distTwo = firstX + width - secondX;
        }
        // checks which distance to return - the wrapped around distance or direct distance
        if(Math.Abs(distOne) < Math.Abs(distTwo))
        {
            return distOne;
        }
        else
        {
            return distTwo;
        }

     

    }
}
