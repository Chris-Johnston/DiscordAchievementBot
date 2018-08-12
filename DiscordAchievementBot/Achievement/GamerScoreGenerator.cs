using System;
using System.Collections.Generic;
using System.Text;

namespace DiscordAchievementBot
{
    public class GamerScoreGenerator
    {
        private readonly long[] scores = { 10, 20, 25, 50, 75, 100, 125, 150, 200, 300, 500 };
        private readonly long[] rareScores = { 100, 125, 150, 200, 250, 300, 350, 500 };

        private readonly Random rng;

        public GamerScoreGenerator()
        {
            rng = new Random();
        }

        /// <summary>
        /// Returns a randomly-selected gamerscore
        /// </summary>
        /// <returns></returns>
        public long GetGamerScore
            => scores[rng.Next(scores.Length)];

        /// <summary>
        /// Gets a rare gamerscore amount.
        /// </summary>
        /// <returns>Returns an integer of a randomly-selected rare gamerscore</returns>
        public long GetRareGamerScore
            => rareScores[rng.Next(rareScores.Length)];

    }
}
