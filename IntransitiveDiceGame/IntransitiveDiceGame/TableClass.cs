using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleTables;

namespace IntransitiveDiceGame
{
    public class TableClass
    {
        private static ConsoleTable _consoleTable;
        public TableClass()
        {
        }     

        public static void CreateHelpTable(IEnumerable<Dice> diceList, Dictionary<Dice, float[]> probDict)
        {
            var headers = new List<string> { "Dices" }.Concat(diceList.Select(d => d.ToString())).ToArray();
            _consoleTable = new ConsoleTable(headers);

            var diceArray = diceList.ToArray();

            foreach (Dice item in diceArray)
            {
                var row = new List<object> { item.ToString() }; 

                float[] probabilities = probDict.TryGetValue(item, out var probs) ? probs : null;

                foreach (Dice opponent in diceArray)
                {
                    int index = Array.IndexOf(diceArray, opponent);

                    row.Add(probabilities != null ? probabilities[index].ToString("F2") : "Not Found");
            
                }

                _consoleTable.AddRow(row.ToArray());
            }

            _consoleTable.Write();
        }
    }
}
