using System;
using System.Collections.Generic;
using System.IO;

namespace BowlingGame
{
    internal class Score
    {
        public int CurrentScore { get; private set; }
        public int Level { get; private set; }
        private const string HighScoreFilePath = "highscore.txt";

        // Different score for multiple trys
        private int firstTryScore;
        private int secondTryScore;
        private int thirdTryScore;

        public Score()
        {
            CurrentScore = 0;
            Level = 1;
            ResetFrameScores();
        }

        public void IncrementScore(int pinsKnocked, int attempt)
        {
            // Calculating the score if its a strike then 500 point,
            // If a player knocks noOfPins on 1st then: noOfPins * 15, 2nd try: noOfPins * 10 lastly, noOfPins * 5
            if (attempt == 1)
            {
                firstTryScore += pinsKnocked * 15;
            }
            else if (attempt == 2)
            {
                secondTryScore += pinsKnocked * 10;
            }
            else if (attempt == 3)
            {
                thirdTryScore += pinsKnocked * 5;
            }

            CurrentScore = firstTryScore + secondTryScore + thirdTryScore;

            if (attempt == 1 && pinsKnocked == 10)
            {
                CurrentScore += 500;
            }
        }

        // Saving the highscore in txt file
        public void SaveHighScore()
        {
            try
            {
                List<string> lines = new List<string>();

                if (File.Exists(HighScoreFilePath))
                {
                    lines.AddRange(File.ReadAllLines(HighScoreFilePath));
                }

                bool scoreUpdated = false;
                for (int i = 0; i < lines.Count; i++)
                {
                    if (lines[i].StartsWith($"Score Level {Level}:"))
                    {
                        string[] parts = lines[i].Split(':');
                        if (int.TryParse(parts[1].Trim(), out int existingScore) && CurrentScore > existingScore)
                        {
                            lines[i] = $"Score Level {Level}: {CurrentScore}";
                            scoreUpdated = true;
                        }
                        break;
                    }
                }

                if (!scoreUpdated)
                {
                    lines.Add($"Score Level {Level}: {CurrentScore}");
                }

                File.WriteAllLines(HighScoreFilePath, lines);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving high score: {ex.Message}");
            }
        }

        public int GetHighScoreForLevel(int level)
        {
            try
            {
                if (File.Exists(HighScoreFilePath))
                {
                    string[] lines = File.ReadAllLines(HighScoreFilePath);

                    foreach (var line in lines)
                    {
                        if (line.StartsWith($"Score Level {level}:"))
                        {
                            string[] parts = line.Split(':');
                            if (int.TryParse(parts[1].Trim(), out int score))
                            {
                                return score;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading high score: {ex.Message}");
            }

            return 0;
        }

        public void LevelUp()
        {
            Level++;
            ResetFrameScores();
        }

        private void ResetFrameScores()
        {
            firstTryScore = 0;
            secondTryScore = 0;
            thirdTryScore = 0;
        }
    }
}
