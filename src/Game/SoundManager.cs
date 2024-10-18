using System;
using System.Collections.Generic;
using System.Text;


class SoundManager
{
    private Dictionary<string, SoundInstance> sounds;
    public SoundManager()
    {
        //initializes list of sounds
        sounds = new Dictionary<string, SoundInstance>();
    }
    //adds sound to list with string as key
    public void addSound(string name, string filepath, bool repeat = false)
    {
        Sound s = Engine.LoadSound(filepath);
        SoundInstance si = Engine.PlaySound(s, repeat, 0);
        sounds.Add(name, si);
    }
    //stops the sound that corresponds with the input key
    public void stopSound(string name)
    {
        SoundInstance s;
        sounds.TryGetValue(name,out s);
        if(s == null)
        {
            return;
        }
        else
        {
            Engine.StopSound(s);
        }
        sounds.Remove(name);
    }
    //clears the entire sound list
    public void clearSound()
    {
        foreach(SoundInstance s in sounds.Values){
            Engine.StopSound(s);
        }
        sounds.Clear();
    }
}

