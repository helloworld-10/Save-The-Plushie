using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Reflection;

class GameLevelScreen : Screen
{
    public Player player;
    Screen winScreen;
    GameDebug gd;
    int levelNum = 1;
    Vector2 resolution;
    GameObject pixelatedBackground;
    

    public int gameState = 0; // 0 = still playing, 1 = win, 2 = loss

  
    public List<Actor> actorsList;
    public List<Token> tokenList;
    public List<Actor> allEnemies;
    //public List<GameObject> allGameObjects;

    SoundManager soundManager;
    public MapEditor map;
    Key1 key;
    Camera camera;

    String bgm = "bgm";
    bool start = false;

    public GameLevelScreen(Vector2 resolution, int levelNum) : base(resolution)
    {
        // path strings for objects that have a S"..\\Assets\\Actors\\Enemy 1.png");      
        gd = new GameDebug();
        this.resolution = resolution;
        this.levelNum = levelNum;

        if (levelNum == 0)
        {
            map = new MapEditor(".\\Game\\Level Files\\" + "Tutorial.txt");
            pixelatedBackground = new GameObject(new Vector2(0, 0), spriteLoc: "Background\\level " + 1 + " background.png", oSize: new Vector2(1342, 448), pixelSize: new Vector2(1342, 448), depth: .699f); //1920
        } 
        else if (levelNum == 4)
        {
            map = new MapEditor(".\\Game\\Level Files\\" + "Level " + levelNum + ".txt");
            pixelatedBackground = new GameObject(new Vector2(0, 0), spriteLoc: "Background\\level " + 3 + " background.png", oSize: new Vector2(1342, 448), pixelSize: new Vector2(1342, 448), depth: .699f); //1920
        } 
        else 
        {
            map = new MapEditor(".\\Game\\Level Files\\" + "Level " + levelNum + ".txt");
            pixelatedBackground = new GameObject(new Vector2(0, 0), spriteLoc: "Background\\level " + levelNum + " background.png", oSize: new Vector2(1342, 448), pixelSize: new Vector2(1342, 448), depth: .699f); //1920
        }

    }

    public void init() 
    {
        allGameObjects = new List<GameObject>();
        actorsList = new List<Actor>();
        tokenList = new List<Token>();
        allEnemies = new List<Actor>();
        soundManager = new SoundManager();

        player = new Player(new Vector2(32 * 3, 32 * 13), allGameObjects);
        map.createMap(player, allGameObjects);

        tokenList.AddRange(map.getAllTokens());
        actorsList.AddRange(map.getAllEnemies());
        allEnemies.AddRange(map.getAllEnemies());
        actorsList.Add(player);

        allGameObjects.AddRange(actorsList);
        allGameObjects.AddRange(map.getAllPlatforms());
        allGameObjects.AddRange(map.getAllTokens());
        allGameObjects.AddRange(map.getAllEnemies());
        allGameObjects.AddRange(map.getAllPickables());

        allGameObjects.Insert(0, pixelatedBackground);

        camera = new Camera(allGameObjects);
    }

    public override void drawPositions()
    {


        Engine.DrawString("Player Position:", new Vector2(5, 0), Color.White, size4Font);
        Engine.DrawString(getPlayer().position.ToString(), new Vector2(110, 0), Color.White, size4Font);

        Engine.DrawString("Bunny Position:", new Vector2(5, 15), Color.White, size4Font);
        Engine.DrawString(map.getBunny().position.ToString(), new Vector2(110, 15), Color.White, size4Font);

        int PositionY = 30;

        foreach (Actor Enemy2 in map.getAllBees())
        {
            Engine.DrawString("Enemy Position:", new Vector2(5, PositionY), Color.White, size4Font);
            Engine.DrawString(Enemy2.position.ToString(), new Vector2(110, PositionY), Color.White, size4Font);
            PositionY += 15;
        }
        PositionY += 15;

    }

    public override void draw()
    {
        Console.Write(gameState);
        if (!start) 
        {
            init();
            soundManager.addSound(bgm, "Sounds\\bgm.wav", true);
            start = true;
        }

        float deltaTime = Engine.TimeDelta;

        Engine.DrawRectSolid(boundsBox, Color.LawnGreen); // background


        List<int> frames = new List<int>();
        frames.Add(1);

        // updating position of bullets
        foreach(Actor tempActor in player.bullets)
        {
            tempActor.update(deltaTime);
        }
        
        // deleting bullets as necessary
        foreach(Projectile bullet in player.deleteBullets)
        {
            player.bullets.Remove(bullet);
        }

        player.deleteBullets.Clear();

        Physics.update(allGameObjects, map.getAllPlatforms(), Engine.TimeDelta, getPlayer());
        camera.showScreen(Resolution, getPlayer());

        // keeps track of carrots left to collect before boss
        Engine.DrawString(
                "Tokens left: " + tokenList.Count + "  ",
                new Vector2(resolution.X, 0),
                Color.White,
                size3Font,
                TextAlignment.Right
            );

        // calculate game play grading 
        StartScreen.curTime.timePassed();
        // calculting the final score based on how much time elapsed
        double finalScore = StartScreen.curTime.finalTime;
        // string displaying they won + their score
        String output = "Score: " + finalScore.ToString("0.00") + "  ";

        // keeps track of player score
        Engine.DrawString(
                output,
                new Vector2(resolution.X, 32),
                Color.White,
                size3Font,
                TextAlignment.Right
            );



        foreach (Actor enemy in allEnemies)
        {
            if (enemy.type != null && enemy.type.Equals("good enemy"))
            {
                if (enemy.isGameOver)
                {
                    soundManager.clearSound();
                    gameState = 1;
                }
            }
            else if (enemy.isGameOver || Actor.gameOver)
            {
                Actor.gameOver = false;
                soundManager.clearSound();
                gameState = 2;
            }
        }

        if (gameState == 0)
        {
            if (tokenList.Count == 0)
            {
                gameState = 4;
            }
        }

        if (gameState == 4)
        {
            allGameObjects.AddRange(map.getPhaseTwo());
            allEnemies.AddRange(map.getPhaseTwo());
            gameState = 5;
        }


        
        foreach (Pickable p in Pickable.needToDelete)
        {
            Pickable.allPickable.Remove(p);
            if (p.unique.Equals("Token"))
            {
                tokenList.Remove((Token)p);
            }
        }
        Pickable.needToDelete.Clear(); 
        
    }

    public override Screen checkActivity(bool isMouseClicked, Vector2 mousePos)
    {
        if (gameState == 1)
        {
            soundManager.stopSound(bgm);
            if (levelNum >= 4)
            {
                winScreen = new WinScreen(resolution);
            }
            else 
            {
                winScreen = new GameLevelScreen(resolution, levelNum + 1);
            }

            soundManager.clearSound();
            return winScreen;
        }
        else if (gameState == 2) 
        {
            soundManager.stopSound(bgm);
            soundManager.addSound("dead", "Sounds\\dead.wav");

            soundManager.clearSound();
            return new LoseScreen(Resolution);
        }
        return null;
    }

    public Player getPlayer() { return player; }
}

