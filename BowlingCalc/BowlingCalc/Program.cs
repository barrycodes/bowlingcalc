using System;
using System.Collections.Generic;

namespace Bowling
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // do the bowling exercise
            BowlingExercise();
            // wait for keypress
            Console.ReadKey();
        }

        // try out the bowling score functionality
        private static void BowlingExercise()
        {
            // prepare some bowling series for testing
            int[] scores1 =  { 4,4,  4,4,  4,4,  4,4,  4,4,  4,4,  4,4,  4,4,  4,4,  4,4      }; // 80
            int[] scores2 =  { 4,6,  4,4,  4,4,  4,4,  4,4,  4,4,  4,4,  4,4,  4,4,  4,4      }; // 86
            int[] scores3 =  { 10,   4,4,  4,4,  4,4,  4,4,  4,4,  4,4,  4,4,  4,4,  4,4      }; // 90
            int[] scores4 =  { 10,   10,   4,4,  4,4,  4,4,  4,4,  4,4,  4,4,  4,4,  4,4      }; // 106
            int[] scores5 =  { 4,4,  4,4,  0,0,  4,4,  4,4,  4,4,  4,4,  4,4,  4,4,  4,4      }; // 72
            int[] scores6 =  { 4,4,  4,4,  4,4,  4,4,  4,4,  4,4,  4,4,  4,4,  4,4,  10,10,10 }; // 102
            int[] scores7 =  { 4,4,  4,4,  4,4,  4,4,  4,4,  4,4,  4,4,  4,4,  4,4,  2,8,5    }; // 87
            int[] scores8 =  { 4,4,  4,4,  4,4,  4,4,  4,4,  4,4,  4,4,  4,4,  4,4,  10,0,10  }; // 92
            int[] scores9 =  { 2,3,  10,   10,   10,   0,0,  3,7,  9,1,  4,4,  10,   0,10,9   }; // 145
            int[] scores10 = { 10,   10,   10,   10,   10,   10,   10,   10,   10,   10,10,10 }; // 300

            // try out each prepared series
            TryBowlingCalculator(scores1);
            TryBowlingCalculator(scores2);
            TryBowlingCalculator(scores3);
            TryBowlingCalculator(scores4);
            TryBowlingCalculator(scores5);
            TryBowlingCalculator(scores6);
            TryBowlingCalculator(scores7);
            TryBowlingCalculator(scores8);
            TryBowlingCalculator(scores9);
            TryBowlingCalculator(scores10);
        }

        // calculate bowling score for a given series and print it to the screen
        private static void TryBowlingCalculator(int[] rolls)
        {
            Console.WriteLine("BOWLING!");
            Console.WriteLine("SERIES: " + IntArrayToString(rolls));
            // print result of BowlingCalculater.GetScore method
            Console.WriteLine("TOTAL SCORE: " + new BowlingCalculator().GetScore(rolls));
            Console.WriteLine();
        }

        // concatenate a bunch of integers into a string
        private static string IntArrayToString(int[] ar)
        {
            string result = string.Empty;
            foreach (int n in ar)
                result += n + " ";
            return result;
        }
    }

    // class for calculating bowling score
    public class BowlingCalculator
    {
        // returns the total score for a given series of rolls
        public int GetScore(int[] rolls)
        {
            int totalScore = 0;

            // get all the frame scores
            int[] frames = GetFrames(rolls);

            // loop through the first ten frames and tally up the total
            for (int i = 0; i < 10; ++i)
                totalScore += frames[i];

            return totalScore;
        }

        // returns a set of scores where each score is the total score of a frame
        private int[] GetFrames(int[] rolls)
        {
            // prepare list to store results
            List<int> results = new List<int>();

            // prepare variable to store the first roll of each frame
            int frameFirstRoll = -1;

            // loop through all rolls
            for (int i = 0; i < rolls.Length; ++i)
            {
                // obtain the current roll
                int roll = rolls[i];

                // if it's a strike
                if (roll == 10 && frameFirstRoll == -1)
                    // calculate the strike score and store it to the list
                    results.Add(10 + GetNextTwoRolls(rolls, i));

                // otherwise if it's a spare
                else if (frameFirstRoll >= 0 && frameFirstRoll + roll == 10)
                {
                    // calculate the spare score and store it to the list
                    results.Add(10 + GetNextRoll(rolls, i));
                    // reset variable
                    frameFirstRoll = -1;
                }

                // otherwise if it's the first roll of a frame
                else if (frameFirstRoll < 0)
                    // set variable
                    frameFirstRoll = roll;

                // otherwise it's the second roll of a normal frame
                else
                {
                    // store frame score
                    results.Add(frameFirstRoll + roll);
                    // reset variable
                    frameFirstRoll = -1;
                }
            }
            // return the series of frame scores
            return results.ToArray();
        }

        // returns the roll following the indexed roll (if any)
        private int GetNextRoll(int[] rolls, int index)
        {
            int result = 0;
            if (index + 1 < rolls.Length)
                result = rolls[index + 1];
            return result;
        }

        // returns the sum of the two rolls following the indexed roll (if any)
        private int GetNextTwoRolls(int[] rolls, int index)
        {
            return GetNextRoll(rolls, index) + GetNextRoll(rolls, index + 1);
        }
    }
}