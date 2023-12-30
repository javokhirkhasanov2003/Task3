using ConsoleTables;

namespace task3;

public class HelpOption
{
    public void DrawTable(string[] moves, Rule[,] rules)
    {
        var header = new string[] { "" }.Concat(moves).ToArray();
        var table = new ConsoleTable(header);
        for (int i = 0; i < moves.Length; i++)
        {
            var move = moves[i];
            object[] row = new object[moves.Length + 1];
            row[0] = move;

            for (int j = 0; j < moves.Length; j++)
            {
                row[j + 1] = rules[i, j].ToString();
            }

            table.AddRow(row);
        }
        Console.WriteLine();
        Console.WriteLine("Move in column win/draw/lose move in row");
        Console.WriteLine("Example: Rock win Scissors, Rock draw Rock, Rock lose Paper");
        table.Write();
    } 
}