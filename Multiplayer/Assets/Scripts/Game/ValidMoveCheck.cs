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

        if( Mathf.Abs( ( a.Position.x - b.Position.x ) + ( a.Position.y - b.Position.y ) ) > 1 )
            return false;

        // If the first tile doesn't have a unit on it, there's nothing to move
        if( a.Contains == null )
            return false;

        //if( ( a.Contains.Side == Side.BLUE && ( b.Contains && b.Contains.Side == Side.BLUE ) ) ||
        //    ( a.Contains.Side == Side.RED && ( b.Contains && b.Contains.Side == Side.RED ) ) )
        //{
        //     Trying to move out of line
        //    if( a.Position.y != b.Position.y )
        //    {
        //        {
        //            int x, y;
        //            x = 0;
        //            y = a.Position.y;

        //            GridPosition getAt;

        //             We'd like to test if we're the last one in the line
        //            if( a.Contains.Side == Side.BLUE )
        //                x = a.Position.x - 1;
        //            else
        //                x = a.Position.x + 1;

        //            getAt = new GridPosition( x, y );

        //            Tile targetTile = GameMap.SINGLETON.GetTileAt( getAt );

        //             If there's no unit behind us
        //            if( targetTile.Contains == null )
        //            {
        //                int _x, _y;
        //                _x = 0;
        //                _y = b.Position.y;

        //                GridPosition _getAt;

        //                 We'd like to test if 
        //                if( a.Contains.Side == Side.BLUE )
        //                    _x = b.Position.x + 1;
        //                else
        //                    _x = b.Position.x - 1;

        //                _getAt = new GridPosition( _x, _y );

        //                Tile _targetTile = GameMap.SINGLETON.GetTileAt( _getAt );

        //                if( !( _targetTile.Contains ) || ( _targetTile.Contains.Side != a.Contains.Side ) )
        //                    return false;
        //            }
        //        }
        //    }
        //}

        // if im moving to a different y
        // is there a unit of my team behind me?
        // if there is don't allow

        // We can move behind one of our units, or swap with one of our units. Valid move!
        return true;
    }

    private static bool IsValidMove_PlanningState( Tile a, Tile b )
    {
        return true;
    }
}
