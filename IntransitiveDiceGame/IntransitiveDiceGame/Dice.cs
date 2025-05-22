using OnixLabs.Core.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace IntransitiveDiceGame
{
    public class Dice
    {
        private int _faceCount { get; set; }
        private int[] _values {  get; set; }
        private FairNumberGenerator generator;
        public int[] Values { get { return _values; } }
        public int FaceCount { get { return _faceCount; } }

        public Dice() { }
        public Dice(string inputs)
        {
            try
            {
                _values = inputs.Split(",").Select(int.Parse).ToArray();
                _faceCount = _values.Length;
                generator = new FairNumberGenerator();
            }
            catch (FormatException ex)
            { 
                Console.WriteLine($"The values must be integer numbers: {ex.Data.Values} is invalid");
                return;
            }
            
        }

        public int Roll()
        {
            int rndNum = generator.Generate(_faceCount);
            int playerChoice = generator.PlayerChoice;
            int result = (rndNum + playerChoice) % _faceCount;

            Console.WriteLine($"Result of {rndNum} + {playerChoice} mod {_faceCount} is {result}");

            return _values[result];
        }

        public override string ToString()
        {
            return $"[{string.Join(",",_values)}]";
        }
    }
}
