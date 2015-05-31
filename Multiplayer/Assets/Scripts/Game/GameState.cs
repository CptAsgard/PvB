using UnityEngine;
using System.Collections;

public enum EGameState
{
    PLANNING,
    PLAY,
    END
}

public static class GameState
{
    public static EGameState CurrentState { get; private set; }

    public static Side CurrentPlayerTurn { get; private set; }

    public static bool GameIsPlaying { get; private set; }

    static GameState()
    {
        CurrentState = EGameState.PLANNING;
    }

    public static void Initialize( Side startingSide )
    {
        CurrentPlayerTurn = startingSide;
    }

    /**
     * Call this to change the current active side to the next player
     * 
     * If the player's turn is over, and it's the next player's turn,
     * this sends a message saying that it's the next player's turn.
     */
    public static void NextPlayerTurn()
    {
        if( CurrentPlayerTurn == Side.BLUE )
            CurrentPlayerTurn = Side.RED;
        else
            CurrentPlayerTurn = Side.BLUE;

        Messenger.Bus.Route( new TurnStateChange()
        {
            Side = CurrentPlayerTurn
        } );
    }

    /**
     * Call this when the game goes from the planning to the playing phase
     * Can only be called once
     */
    public static void StartGame()
    {
        if( GameIsPlaying )
            return;

        GameIsPlaying = true;
        CurrentState = EGameState.PLAY;

        Messenger.Bus.Route( new StartGame() );
    }

    /**
     * Call this when the game goes from the playing to the end phase
     * Can only be called once
     */
    public static void EndGame()
    {
        if( !GameIsPlaying )
            return;

        GameIsPlaying = false;
        CurrentState = EGameState.END;

        Messenger.Bus.Route( new EndGame() );
    }
}
