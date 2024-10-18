using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

class MapEditor
{
    // s = no way platform
    // o = one way platform
    // f = friction platform
    // d = destructable platform
    // m = moving platform
    // k = key
    // r = door
    // # = no way platform
    // 1 = good enemy
    // 2 = bad enemy
    // 3 = invulnerable enemy
    // 4 = one shot enemy
    // 5 = boss
    // t = token
    // a = armor
    // p = speedpot potion


    int textShift = 4;

    //Creating a list for all types of objects in our game 
    String FilePath;
    int BlockWidth = 32;
    List<Platform> allPlatforms = new List<Platform>();
    public List<Actor> allEnemies = new List<Actor>();
    List<Token> allTokens = new List<Token>();
    List<Pickable> allPickables = new List<Pickable>();
    List<Actor> phaseTwo = new List<Actor>();
    public String tokenSprite = "Carrot";
    
    List<String> tutorialStrings = new List<String>();
    List<Vector2> tutorialPos = new List<Vector2>();
    List<Wasp> allBees = new List<Wasp>();
    BossEnemy deer;
    Bunny bunny;


    public static Vector2 Resolution = new Vector2(640, 480);

    //Map Editor is initialized with file path passed in
    public MapEditor(String filePath)
    {
        FilePath = filePath;
    }

    public void createMap(Player player, List<GameObject> allGameObjects)
    {
        //Each line of the file is read one by one and each character is read one by one. Depeding on the character an object is placed in its position. 
        if (File.Exists(FilePath))
        {
            using (StreamReader reader = new StreamReader(FilePath))
            {
                int x = 0;
                int y = -2 * BlockWidth;
                while (!reader.EndOfStream)
                {

                    string content = reader.ReadLine(); // one line
                    x = 0;
                    y += BlockWidth;

                    char[] platforms = content.ToCharArray(); // each string in line

                    foreach (char platform in platforms)
                    {
                        x = x + BlockWidth;

                        if (platform.Equals('s'))
                        {
                            allPlatforms.Add(new NoWayPlatform(new Vector2(x, y)));
                        }
                        else if (platform.Equals('o'))
                        {
                            allPlatforms.Add(new OneWayPlatform(new Vector2(x, y)));
                        }
                        else if (platform.Equals('f'))
                        {
                            allPlatforms.Add(new FrictionPlatform(new Vector2(x, y)));
                        }
                        else if (platform.Equals('d'))
                        {
                            allPlatforms.Add(new DestructuablePlatform(new Vector2(x, y)));
                        }
                        else if (platform.Equals('m'))
                        {
                            allPlatforms.Add(new MovingPlatform(new Vector2(x, y)));
                        }
                        else if (platform.Equals('k'))
                        {
                            allPlatforms.Add(new Key1(new Vector2(x, y)));
                        }
                        else if (platform.Equals('r'))
                        {
                            allPlatforms.Add(new door(new Vector2(x, y)));
                        }
                        else if (platform.Equals('#'))
                        {
                            allPlatforms.Add(new NoWayPlatform(new Vector2(x, y), typeGround: FilePath));
                        }
                        else if (platform.Equals('1'))
                        {
                            bunny = new Bunny(new Vector2(x, y), player);
                            phaseTwo.Add(bunny);
                        }
                        else if (platform.Equals('2'))
                        {
                            Wasp bee = new Wasp(new Vector2(x, y), player);
                            allBees.Add(bee);
                            phaseTwo.Add(bee);
                        }
                        else if (platform.Equals('3'))
                        {
                            allEnemies.Add(new InvulnerableEnemy(new Vector2(x, y), player, BlockWidth));
                        }
                        else if (platform.Equals('4'))
                        {
                            allEnemies.Add(new OneShotEnemy(new Vector2(x, y), player, allGameObjects));
                        }
                        else if (platform.Equals('5'))
                        {
                            deer = new BossEnemy(new Vector2(x, y - (BlockWidth * 3)), player, allGameObjects);
                            phaseTwo.Add(deer);
                        }
                        else if (platform.Equals('t'))
                        {
                            allTokens.Add(new Token(new Vector2(x, y), tokenSprite));
                        }
                        else if (platform.Equals('p'))
                        {
                            allPickables.Add(new Pickable(new Vector2(x, y), "Tokens\\Potion.png", "speed potion"));
                        }
                        else if (platform.Equals('a'))
                        {
                            allPickables.Add(new Pickable(new Vector2(x, y), "Tokens\\Armor.png", "Armor"));
                        }

              

                        else if (platform.Equals('9')) 
                        {
                            allGameObjects.Add(new TextBox("Jump through the leaves to collect the carrot!", 
                                                            new Vector2(x - (32 * textShift), y)));
                        }
                        else if (platform.Equals('8'))
                        {
                            allGameObjects.Add(new TextBox("Jump on the moving platform to avoid falling into the hole!"
                                                ,new Vector2(x - (32 * textShift), y)));
                        }
                        else if (platform.Equals('7'))
                        {
                            allGameObjects.Add(new TextBox("Use P to throw a tomato at the rock!", (new Vector2(x - (32 * textShift), y))));
                        }
                        else if (platform.Equals('6'))
                        {
                            allGameObjects.Add(new TextBox("Jump on the sticky platform to avoid the spider and collect the key!"
                                            ,new Vector2(x - (32 * textShift), y)));
                        }
                        else if (platform.Equals('z'))
                        {
                            allGameObjects.Add(new TextBox("Catch the rabbit and avoid the bee!"
                                            , new Vector2(x - (32 * textShift), y)));
                        }
                        else if (platform.Equals('b'))
                        {
                            bunny = new Bunny(new Vector2(x, y), player, spriteLoc: "Actors\\Plushie.png");
                            bunny.plushie = true;
                            phaseTwo.Add(bunny);
                        }
                        else if (platform.Equals('c'))
                        {
                            Apple apple = new Apple(new Vector2(x, y), player, allGameObjects);
                            allEnemies.Add(apple);
                        }



                        //Console.WriteLine("Platform : " + x + " X " + y + " : " + platform);
                    }
                }
            }
        }
    }

    //Functions to get access to lists of all different types of objects. 
    public List<Platform> getAllPlatforms() { return allPlatforms; }

    public BossEnemy getBoss() { return deer; }

    public Bunny getBunny() { return bunny; }
    public List<Wasp> getAllBees() { return allBees; }
    public List<Actor> getAllEnemies() { return allEnemies; }
    public List<Token> getAllTokens() { return allTokens; }
    public List<Actor> getPhaseTwo() { return phaseTwo; }
    public List<Pickable> getAllPickables() { return allPickables; }


}