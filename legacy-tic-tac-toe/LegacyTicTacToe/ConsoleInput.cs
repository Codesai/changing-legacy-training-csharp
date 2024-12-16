namespace LegacyTicTacToe;

public class ConsoleInput : Input
{
    public string Read()
    {
        return Console.ReadLine() ?? string.Empty;
    }
}