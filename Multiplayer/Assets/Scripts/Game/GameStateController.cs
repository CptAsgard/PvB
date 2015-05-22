using UnityEngine;
using System.Collections;

public enum GameState
{
    PLANNING,
    PLAY,
    END
}

public static class GameStateController 
{
    public static GameState CurrentState;
}
