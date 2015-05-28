using UnityEngine;
using System.Collections;

public static class ValidMoveCheck {

    public static bool IsValidMove( Tile a, Tile b )
    {
        if( GameState.CurrentState == EGameState.PLANNING )
            return true;

        return false;
    }

}
