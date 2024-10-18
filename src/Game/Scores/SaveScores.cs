using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

class SaveScores
{
    String filePath;
    int BlockWidth = 32;
    int[] topScores;
    int numScores = 10;

    //Loads scores and exports to file
    public SaveScores(String filePath)
    {
        this.filePath = filePath;
        topScores = loadScores();
    }

    public void draw(Font f, Vector2 Resolution)
    {
        Vector2 position = new Vector2(Resolution.X / 2, BlockWidth * 3);

        foreach (int score in topScores)
        {
            Engine.DrawString(score.ToString(), position, Color.White, f, TextAlignment.Center);
            position.Y += BlockWidth;
        }
    }

    public void saveScores() 
    {
        if (File.Exists(filePath))
        {
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                foreach (int score in topScores)
                {
                    writer.WriteLine(score.ToString());
                }
                writer.Close();
            }
        }
    }

    public int[] loadScores()
    {
        int[] scoreList = new int[numScores];

        if (File.Exists(filePath))
        {
            using (StreamReader reader = new StreamReader(filePath))
            {
                int count = 0;
                while (!reader.EndOfStream)
                {
                    string content = reader.ReadLine(); // one line
                    string stringScore = content.TrimEnd(); // each string in line
                    scoreList[count] = int.Parse(stringScore);
                    count++;
                }
            }
            return scoreList;
        }
        return null;
    }

    public void addNewScore(int score)
    {
        int index = 0;
        // checks if score is one of the top scores
        while (topScores[index] > score)
        {
            index++;  

            if (index >= numScores) { break; }
        }

        // adds new score to top score
        if (index  != numScores) 
        {
            int[] tempList = new int[numScores];

            for (int i = 0; i < index; i++) { tempList[i] = topScores[i]; }
            tempList[index] = score;
            for (int i = index + 1; i < numScores; i++) { tempList[i] = topScores[i - 1]; }

            topScores = tempList;
        }
    }

}
