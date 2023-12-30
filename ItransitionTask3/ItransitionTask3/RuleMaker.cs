namespace task3;

public class RuleMaker
{
    public Rule[,] MakeRules(string[] moves)
    {
        int len = moves.Length;
        var tableForWinner = new Rule[len, len];
        for (int row = 0; row < len; row++)
        {
            var column = row;
            var count = 0;
            var winCount = 0;
            var midle = len / 2;
            while (true)
            {
                if (column >= len)
                {
                    column = 0;
                }
                if (row == column)
                {
                    tableForWinner[row, column] = Rule.Draw;
                    if (count == 1)
                    {
                        break;
                    }
                    count++;
                    column++;
                    continue;
                }
                if (winCount < midle)
                {
                    tableForWinner[row, column] =Rule.Lose ;
                    winCount++;
                }
                else
                {
                    tableForWinner[row, column] = Rule.Win;
                }
                column++;
            }
        }
        return tableForWinner;
    }
}