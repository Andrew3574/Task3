using OnixLabs.Core.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace IntransitiveDiceGame
{
    class FairNumberGenerator
    {
       
        private int _playerChoice;

        public int PlayerChoice { get { return _playerChoice; } }
        public FairNumberGenerator() { }
        
        public int Generate(int maxValue)
        {
            Console.WriteLine($"I selected number from 0 to {maxValue-1}");
            int randNumber = new Random().Next(0, maxValue);

            var key = HmacGenerator.GenerateSecureKey();
            var hmac = HmacGenerator.GenerateHMAC(randNumber, key);

            Console.WriteLine($"(HMAC: {hmac.ToBase16()})");
            while (true)
            {
                Console.Write("Take your turn: ");

                if (Int32.TryParse(Console.ReadLine(), out _playerChoice) && _playerChoice >= 0 && _playerChoice < maxValue)
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Incorrect! The value must be an integer between 0 and " + (maxValue - 1) + "!");
                }
            }
            Console.WriteLine($"Your choice is {_playerChoice}");
            Console.WriteLine($"I chose {randNumber}\n (KEY: {key.ToBase16()})");

            return randNumber;
        }

        public bool DetermineFirstMove()
        {
            int val = Generate(2);
            return val == _playerChoice;
        }
    }
}
