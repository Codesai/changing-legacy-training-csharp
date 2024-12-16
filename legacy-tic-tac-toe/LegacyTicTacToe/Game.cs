namespace LegacyTicTacToe;

public class Game
{
    private enum Turn
    {
        X,
        O,
    }

    private readonly Input _inputX;
    private readonly Output _outputX;
    private readonly Input _inputO;
    private readonly Output _outputO;
    private readonly List<Field> _playerXFields;
    private readonly List<Field> _playerOFields;
    private Turn _turn;

    private static readonly Dictionary<string, Field> FieldsByRepresentation = new()
    {
        { "1", Field.One },
        { "2", Field.Two },
        { "3", Field.Three },
        { "4", Field.Four },
        { "5", Field.Five },
        { "6", Field.Six },
        { "7", Field.Seven },
        { "8", Field.Eight },
        { "9", Field.Nine }
    };

    private static readonly Field[][] WinningCombinations =
    {
        new[] { Field.One, Field.Two, Field.Three },
        new[] { Field.Four, Field.Five, Field.Six },
        new[] { Field.Seven, Field.Eight, Field.Nine },
        new[] { Field.One, Field.Four, Field.Seven },
        new[] { Field.Two, Field.Five, Field.Eight },
        new[] { Field.Three, Field.Six, Field.Nine },
        new[] { Field.One, Field.Five, Field.Nine },
        new[] { Field.Three, Field.Five, Field.Seven },
    };

    public Game(Input inputX, Output outputX, Input inputO, Output outputO)
    {
        _inputX = inputX;
        _outputX = outputX;
        _inputO = inputO;
        _outputO = outputO;
        _turn = Turn.X;
        _playerXFields = new List<Field>();
        _playerOFields = new List<Field>();
    }

    public void Start()
    {
        _outputX.Display(RepresentGameState());
        while (!HasWon(_playerXFields) &&
               (_playerXFields.Count + _playerOFields.Count) != 9 &&
               !HasWon(_playerOFields))
        {
            if (_turn == Turn.X)
            {
                _playerXFields.Add(YourTurn(_inputX, _outputX));
                _outputX.Display(RepresentGameState());
                _outputO.Display(RepresentGameState());
                _turn = Turn.O;
            }
            else
            {
                _playerOFields.Add(YourTurn(_inputO, _outputO));
                _outputX.Display(RepresentGameState());
                _outputO.Display(RepresentGameState());
                _turn = Turn.X;
            }
        }
    }

    private string RepresentGameState()
    {
        var board = new[]
        {
            new[] { "1", "2", "3" },
            new[] { "4", "5", "6" },
            new[] { "7", "8", "9" }
        };

        var res = string.Join("---------\n",
            board.Select(
                row => row.Select(s =>
                {
                    var field = FieldsByRepresentation[s];

                    if (_playerXFields.Contains(field))
                    {
                        return "X";
                    }

                    if (_playerOFields.Contains(field))
                    {
                        return "O";
                    }

                    return s;
                })
            ).Select(row => string.Join(" | ", row) + "\n")
        );
        
        if (!HasWon(_playerXFields) &&
            (_playerXFields.Count + _playerOFields.Count) != 9 &&
            !HasWon(_playerOFields))
        {
            res += "";
        } else if (HasWon(_playerOFields))
        {
            res += "O wins!\n";
        } else if (HasWon(_playerXFields))
        {
            res += "X wins!\n";
        }
        else
        {
            res += "Draw!\n";
        }

        return res;
    }

    public static bool HasWon(List<Field> fields)
    {
        return WinningCombinations.Any(combination =>
            combination.All(fields.Contains)
        );
    }

    private Field YourTurn(Input input, Output output)
    {
        output.Display("your turn...");
        while (true)
        {
            var value = input.Read();
            if (FieldsByRepresentation.TryGetValue(value, out var turn))
            {
                return turn;
            }

            output.Display("invalid input, please try again");
        }
    }
}

public enum Field
{
    One,
    Two,
    Three,
    Four,
    Five,
    Six,
    Seven,
    Eight,
    Nine
}