using UnityEngine;
using System.Collections;

public static class ValidMoveCheck
{
    public static Unit ResolveFight( Unit a, Unit b )
    {
        return a;
    }

    public static bool IsValidMove( Tile a, Tile b )
    {
        if( a.Contains == null && b.Contains == null )
            return false;

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

        GridPosition getAt;

        // If the first tile doesn't have a unit on it, there's nothing to move
        if( a.Contains == null )
            return false;

        int x, y;
        x = y = 0;

        // We'd like to test if we can move this unit behind our others
        if( a.Contains.Side == Side.BLUE && ( b.Contains && b.Contains.Side == Side.BLUE ) )
            x = b.Position.x - 1;
        else
            x = b.Position.x + 1;

        getAt = new GridPosition( x, y );

        Tile targetTile = GameMap.SINGLETON.GetTileAt( getAt );

        // If we can't move directly behind our other units, it's not a valid move
        if( targetTile.Contains == null )
            return false;

        // We can move behind one of our units, or swap with one of our units. Valid move!
        return true;
    }

    private static bool IsValidMove_PlanningState( Tile a, Tile b )
    {
        return true;
    }
}
