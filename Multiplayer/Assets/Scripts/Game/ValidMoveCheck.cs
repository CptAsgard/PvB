using UnityEngine;
using System.Collections;

public static class ValidMoveCheck
{

    public static bool IsValidMove( Tile a, Tile b )
    {
        if( GameState.CurrentState == EGameState.PLANNING )
            return IsValidMove_PlanningState( a, b );
        else if( GameState.CurrentState == EGameState.PLAY )
            return IsValidMove_PlayState( a, b );
        else
            return false;
    }

    private static bool IsValidMove_PlayState( Tile a, Tile b )
    {
        // If the distance between the two tiles is greater than 1, it's not a valid move
        if( Mathf.Abs( ( a.Position.x - b.Position.x ) + ( a.Position.y - b.Position.y ) ) > 1 )
            return false;

        return true;
    }

    private static bool IsValidMove_PlanningState( Tile a, Tile b )
    {
        return true;
    }
}
