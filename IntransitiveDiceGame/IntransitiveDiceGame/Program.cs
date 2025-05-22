using IntransitiveDiceGame;
using OnixLabs.Core;
using OnixLabs.Core.Text;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Security.Cryptography;
using System.Text;
internal class Program
{
    private static ProbabilityProcessor probabilityProcessor = new ProbabilityProcessor();

    private static void Main(string[] args)
    {
        List<Dice> diceList = new List<Dice>();
        foreach (var arg in args)
        {
            diceList.Add(new Dice(arg));
        }              
        try
        {
            do
            {                
                var generator = new FairNumberGenerator();

                Console.WriteLine("Lets choose who makes first move");
                bool isFirst = generator.DetermineFirstMove();

                InitializeGame(isFirst, diceList);
                
                Console.WriteLine("Continue? y/n");
                switch (Console.ReadLine())
                {
                    case "y":
                        break;
                    case "n":
                        return;
                    default:
                        break;
                }
            }
            while (true);
         
        }
        catch (Exception ex)
        {
            Console.WriteLine("ERROR: " + ex.Message + ex.Data.Values + ex.InnerException);
        }
        Console.ReadLine();
    }
    static void InitializeGame(bool isFirst, List<Dice> diceList)
    { 
        int p1, p2;
        List<Dice> remainDices = new List<Dice>();
        Dice playerDice, opponentDice;
        remainDices.AddRange(diceList);

        switch (isFirst)
        {
            case true:                

                DefinePlayerDice(out playerDice, diceList, remainDices);
                DefineOpponentDice(out opponentDice, remainDices);
                break;          

            case false:

                DefineOpponentDice(out opponentDice, remainDices);
                DefinePlayerDice(out playerDice, diceList, remainDices);
                break;
        }
        p1 = playerDice.Roll();
        p2 = opponentDice.Roll();

        DefineResult(p1, p2);
    }

    static void DefinePlayerDice(out Dice playerDice, List<Dice> diceList, List<Dice> remainDices)
    {
        string input;
        int choice;
        do
        {
            int i = 1;
            Console.WriteLine("Choose your dice:");

            foreach (var dice in remainDices)
            {
                Console.WriteLine($"{i++} - " + dice.ToString());
            }
            Console.WriteLine("X - exit");
            Console.WriteLine("? - help");

            input = Console.ReadLine();
            choice = CheckMenuInput(input, diceList, remainDices);

        }
        while (choice == 0);

        playerDice = remainDices[choice - 1];
        remainDices.RemoveAt(choice - 1);
        Console.WriteLine($"You selected: {playerDice}");
    }

    static void DefineOpponentDice(out Dice opponentDice, List<Dice> remainDices)
    {
        int diceIndex = new Random().Next(remainDices.Count);
        opponentDice = remainDices[diceIndex];
        remainDices.RemoveAt(diceIndex);
        Console.WriteLine($"My choice is {opponentDice.ToString()}");
    }

    static int CheckMenuInput(string input,List<Dice> diceList, List<Dice> remainDices)
    {
        if (Int32.TryParse(input, out int choice) && choice <= remainDices.Count)
        {
            return choice;
        }
        else if (input.Equals("X", StringComparison.OrdinalIgnoreCase))
        {
            return -1;
        }
        else if (input.Equals("?", StringComparison.OrdinalIgnoreCase))
        {
            TableClass.CreateHelpTable(diceList, probabilityProcessor.Process(diceList));
        }

        return 0;
    }

    static void DefineResult(int p1, int p2)
    {
        if (p1 > p2)
        {
            Console.WriteLine($"You Win: {p1} > {p2}");
        }
        else if (p1 < p2)
        {
            Console.WriteLine($"I Win: {p1} < {p2}");
        }
        else if (p1 == p2)
        {
            Console.WriteLine($"Draw: {p1} = {p2}");
        }
    }

}