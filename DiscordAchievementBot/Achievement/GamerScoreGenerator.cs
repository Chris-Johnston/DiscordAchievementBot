using System;
using System.Collections.Generic;
using System.Text;

namespace DiscordAchievementBot
{
    public class GamerScoreGenerator
    {
        private readonly int[] scores = { 10, 20, 25, 50, 75, 100, 125, 150, 200, 300, 500 };
        private readonly int[] rareScores = { 100, 125, 150, 200, 250, 300, 350, 500 };

        private Random rng;

        public GamerScoreGenerator()
        {
            rng = new Random();
        }

        /// <summary>
        /// Returns a randomly-selected gamerscore
        /// </summary>
        /// <returns></returns>
        public int GetGamerScore()
        {
            return scores[rng.Next(scores.Length)];   
        }

        /// <summary>
        /// Returns a randomly-selected rare gamerscore
        /// </summary>
        /// <returns></returns>
        public int GetRareGamerScore()
        {
            return rareScores[rng.Next(rareScores.Length)];
        }

    }
}
