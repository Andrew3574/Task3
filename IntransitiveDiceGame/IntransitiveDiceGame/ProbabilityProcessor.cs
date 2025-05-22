using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntransitiveDiceGame
{
    class ProbabilityProcessor
    {
        public ProbabilityProcessor() { }
       public Dictionary<Dice, float[]> Process(IEnumerable<Dice> dices)
{
            List<Dice> diceList = dices.ToList();
            int size = diceList.Count();
            var probabilityDict = new Dictionary<Dice, float[]>();

            foreach (Dice currentDice in diceList)
            {
                float[] winProbabilities = new float[size];

                for (int i = 0; i < size; i++)
                {
                    Dice opponentDice = diceList[i];

                    if (currentDice == opponentDice)
                    {
                        winProbabilities[i] = 0.5f;
                        continue;
                    }

                    int wins = 0;
                    int totalComparisons = 0;

                    foreach (int currentFace in currentDice.Values)
                    {
                        foreach (int opponentFace in opponentDice.Values)
                        {
                            totalComparisons++;
                            if (currentFace > opponentFace)
                            {
                                wins++;
                            }
                        }
                    }

                    winProbabilities[i] = (float)wins / totalComparisons; 
                }

                probabilityDict.Add(currentDice, winProbabilities);
            }

            return probabilityDict;
        }

    }
}
