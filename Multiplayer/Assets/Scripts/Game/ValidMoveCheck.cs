using UnityEngine;
using System.Collections;

public static class ValidMoveCheck {

    public static bool IsValidMove( Tile a, Tile b )
    {
        if( GameStateController.CurrentState == GameState.PLANNING )
            return true;

        return false;
    }

}
