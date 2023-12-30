using task3;

var hashMaker = new HashMaker();
var ruleMaker = new RuleMaker();    
var helpOption = new HelpOption();

var secretKey = RandomKeyGenerator.Generate(32);

if (ValidateArguments(args)) return;

var moves = args;

var rnd = new Random();
var randomMoveIndex = rnd.Next(0, moves.Length);
var randomMove = moves[ randomMoveIndex];
var randomMoveHash = hashMaker.GetHash(randomMove, secretKey);

Console.WriteLine($"HMAC: {randomMoveHash}");

var rules= ruleMaker.MakeRules(moves);

while (true)
{
    ShowMenu();

    var userCommand = GetUserCommand(moves.Length);

    while (!userCommand.isValidInput)
    {
        userCommand = GetUserCommand(moves.Length);
    }
        
    if (userCommand.command == "0")
    {
        break;
    }

    if (userCommand.command == "?")
    {
        helpOption.DrawTable(moves, rules);
    }

    if (int.TryParse(userCommand.command, out var index))
    {
        var userMoveIndex = index - 1;

        var rule = rules[userMoveIndex, randomMoveIndex];

        Console.WriteLine($"Your move: {moves[userMoveIndex]}");
        Console.WriteLine($"Computer move: {moves[randomMoveIndex]}");

        switch (rule)
        {
            case Rule.Draw:
                Console.WriteLine("Draw");
                break;
            case Rule.Win:
                Console.WriteLine("You win");
                break;
            case Rule.Lose:
                Console.WriteLine("You lose");
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
            
        Console.WriteLine($"HMAC key: {secretKey}");
        break;
    }
}


void ShowMenu()
{
    Console.WriteLine("Available moves:");
    for (int i = 0; i < moves.Length; i++)
    {
        Console.WriteLine($"{i+1} - {moves[i]}");
    }
    Console.WriteLine("0 - exit");
    Console.WriteLine("? - help");
}

(bool isValidInput, string command) GetUserCommand(int movesCount)
{
    Console.Write("Enter your move: ");
    var userCommand = Console.ReadLine()?.Trim();

    if (string.IsNullOrWhiteSpace(userCommand))
    {
        Console.WriteLine("You should not enter empty string or only space");

        return (false, null);
    }

    if (int.TryParse(userCommand, out var number))
    {
        if (number < 0 || number > movesCount)
        {
            Console.WriteLine($"Command should be between 0 to {movesCount}");

            return (false, null);
        }
        
        return (isValidInput: true, command: userCommand);
    }
    
    if (userCommand != "?")
    {
        Console.WriteLine("Invalid command");

        return (false, null);
    }

    return (isValidInput: true, command: userCommand);
}

bool ValidateArguments(string[] strings)
{
    if (strings.ToHashSet().Count < strings.Length)
    {
        Console.WriteLine("Given argument has some duplicates!");
        Console.WriteLine("Arguments should be unique!");
        Console.WriteLine("Example: rock paper scissors");
        return true;
    }

    if (strings.Length < 3)
    {
        Console.WriteLine("Arguments should be more than 3 or equal!");
        return true;
    }

    if (strings.Length % 2 == 0)
    {
        Console.WriteLine("Arguments' count should be odd!");
        return true;
    }

    return false;
}
