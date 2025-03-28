using NSubstitute;
using NUnit.Framework;
using static TicTacToe.Tests.helpers.GameStateDtoBuilder;

namespace TicTacToe.Tests.unit;

[TestFixture]
public class GameTests
{
    private PlayerInteraction _playerXInteraction;
    private PlayerInteraction _playerOInteraction;
    private Game _game;
    private Builder _gameDtoBuilder;
    private List<GameStateDto> _playerXInteractionCalls;
    private List<GameStateDto> _playerOInteractionCalls;

    [SetUp]
    public void SetUp()
    {
        _playerXInteraction = Substitute.For<PlayerInteraction>();
        _playerOInteraction = Substitute.For<PlayerInteraction>();
        _playerXInteractionCalls = new List<GameStateDto>();
        _playerOInteractionCalls = new List<GameStateDto>();

        _playerXInteraction.Display(Arg.Do<GameStateDto>(g => _playerXInteractionCalls.Add(g)));
        _playerOInteraction.Display(Arg.Do<GameStateDto>(g => _playerOInteractionCalls.Add(g)));
        
        _gameDtoBuilder = AGameStateDto();
        _game = new Game(_playerXInteraction, _playerOInteraction);
    }

    [Test]
    public void PlayerXWinsAfterThirdTurn()
    {
        _playerXInteraction.YourTurn().Returns(Field.One, Field.Two, Field.Three);
        _playerOInteraction.YourTurn().Returns(Field.Four, Field.Five);
        
        _game.Start();
        
        ExpectInitialDisplay();
        ExpectPlayerTurn(1, AddingFieldToX(Field.One).Build());
        ExpectPlayerTurn(2, AddingFieldToO(Field.Four).Build());
        ExpectPlayerTurn(3, AddingFieldToX(Field.Two).Build());
        ExpectPlayerTurn(4, AddingFieldToO(Field.Five).Build());
        ExpectPlayerTurn(5, AddingFieldToX(Field.Three).WinningPlayerX().Build());
    }

    [Test]
    public void PlayerOWinsAfterHerThirdTurn()
    {
        _playerXInteraction.YourTurn().Returns(Field.Four, Field.Five, Field.Seven);
        _playerOInteraction.YourTurn().Returns(Field.One, Field.Two, Field.Three);
        
        _game.Start();
        
        ExpectInitialDisplay();
        ExpectPlayerTurn(1, AddingFieldToX(Field.Four).Build());
        ExpectPlayerTurn(2, AddingFieldToO(Field.One).Build());
        ExpectPlayerTurn(3, AddingFieldToX(Field.Five).Build());
        ExpectPlayerTurn(4, AddingFieldToO(Field.Two).Build());
        ExpectPlayerTurn(5, AddingFieldToX(Field.Seven).Build());
        ExpectPlayerTurn(6, AddingFieldToO(Field.Three).WinningPlayerO().Build());
    }
    
    [Test]
    public void DrawWhenX1O5X9O2X8O7X3O6X4()
    {
        _playerXInteraction.YourTurn().Returns(Field.One, Field.Nine, Field.Eight, Field.Three, Field.Four);
        _playerOInteraction.YourTurn().Returns(Field.Five, Field.Two, Field.Seven, Field.Six);
        
        _game.Start();
        
        ExpectInitialDisplay();
        ExpectPlayerTurn(1, AddingFieldToX(Field.One).Build());
        ExpectPlayerTurn(2, AddingFieldToO(Field.Five).Build());
        ExpectPlayerTurn(3, AddingFieldToX(Field.Nine).Build());
        ExpectPlayerTurn(4, AddingFieldToO(Field.Two).Build());
        ExpectPlayerTurn(5, AddingFieldToX(Field.Eight).Build());
        ExpectPlayerTurn(6, AddingFieldToO(Field.Seven).Build());
        ExpectPlayerTurn(7, AddingFieldToX(Field.Three).Build());
        ExpectPlayerTurn(8, AddingFieldToO(Field.Six).Build());
        ExpectPlayerTurn(9, AddingFieldToX(Field.Four).WithNoOneWinning().Build());
    }
    

    private void ExpectPlayerTurn(int turnNumber, GameStateDto gameStateDto)
    {
        Assert.That(_playerXInteractionCalls[turnNumber], Is.EqualTo(gameStateDto));
        Assert.That(_playerOInteractionCalls[turnNumber-1], Is.EqualTo(gameStateDto));
    }

    private void ExpectInitialDisplay()
    {
        Assert.That(_playerXInteractionCalls[0], Is.EqualTo(InitialGameStateDto()));
    }
    
    private Builder AddingFieldToO(Field field)
    {
        return _gameDtoBuilder.AddingFieldToO(field);
    }

    private Builder AddingFieldToX(Field field)
    {
        return _gameDtoBuilder.AddingFieldToX(field);
    }
}