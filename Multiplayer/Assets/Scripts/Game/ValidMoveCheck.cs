using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public struct UnitTypeObj
{
    public UnitType type;
    public Tile associatedObject;
}

public static class ValidMoveCheck
{
    private static Dictionary<UnitType, Tile> unitTypeObjDict;

    public static bool CanFight( Tile a, Tile b )
    {
        // If they're on different rows, don't fight
        if( a.Position.y != b.Position.y )
            return false;

        // If the sides are the same, don't fight
        if( a.Contains.Side == b.Contains.Side )
            return false;

        return true;
    }

    /**
     * Returns the winner of the fight
     */
    public static Tile ResolveFight( Tile a, Tile b )
    {
        if( unitTypeObjDict == null )
            unitTypeObjDict = new Dictionary<UnitType, Tile>();

        unitTypeObjDict.Clear();

        unitTypeObjDict.Add( a.Contains.Type, a );
        
        if( !unitTypeObjDict.ContainsKey( b.Contains.Type ) )
            unitTypeObjDict.Add( b.Contains.Type, b );

        // Miner beats bomb
        if( unitTypeObjDict.ContainsKey( UnitType.BOMB ) && unitTypeObjDict.ContainsKey( UnitType.MINER ) )
        {
            return unitTypeObjDict[ UnitType.MINER ];
        }

        // Bomb kills both
        if( unitTypeObjDict.ContainsKey( UnitType.BOMB ) )
        {
            return null;
        }

        // Spy kills marshall
        if( unitTypeObjDict.ContainsKey( UnitType.SPY ) && unitTypeObjDict.ContainsKey( UnitType.MARSHALL ) )
        {
            return unitTypeObjDict[ UnitType.SPY ];
        }

        // Rank based
        if( (int) a.Contains.Type > (int) b.Contains.Type )
            return a;
        else if( (int) a.Contains.Type < (int) b.Contains.Type )
            return b;
            
        return null;
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
        if( Vector2.Distance( new Vector2( a.Position.x, a.Position.y ), new Vector2( b.Position.x, b.Position.y ) ) > 1 )
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
