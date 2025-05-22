using IntransitiveDiceGame;
using System;
using System.Collections.Generic;
using System.Linq;

public class DiceConfiguration
{
    public static List<Dice> Configure(string[] args)
    {
        var dices = new List<Dice>();

        if (args.Length < 3)
        {
            throw new ArgumentException("Required at least 3 dices");
        }
        foreach (var arg in args)
        {
            var cleanedArg = arg.Trim();

            if (string.IsNullOrWhiteSpace(cleanedArg))
            {
                throw new ArgumentException("Input cannot be empty.");
            }

            try
            {
                dices.Add(new Dice(cleanedArg));
            }
            catch (ArgumentException ex)
            {
                throw new ArgumentException($"Invalid input for dice configuration: {cleanedArg}. {ex.Message}");
            }
        }

        return dices;
    }
}