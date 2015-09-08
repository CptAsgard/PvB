using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/**
 * Helper object to make rule checking easier
 */
public struct UnitTypeObj
{
    public UnitType type;
    public Tile associatedObject;
}

/**
 * Checks if the moves specified are allowed according to game rules
 */
public static class ValidMoveCheck
{
    private static Dictionary<UnitType, Tile> unitTypeObjDict;

    /**
     * Is a fight between the two specified units allowed?
     * @param a The first tile
     * @param b The second tile
     * @returns True if the fight is allowed, false if it isn't
     */
    public static bool CanFight( Tile a, Tile b ) {
        // If they're on different rows, don't fight
        if( a.Position.y != b.Position.y )
            return false;

        // If the sides are the same, don't fight
        if( a.Contains.Side == b.Contains.Side )
            return false;

        return true;
    }

    /**
     * Returns the winner of the fight. Make sure to call CanFight() before this!
     * @param a The first tile
     * @param b The second tile
     * @returns The tile that won the fight, null if both should die
     */
    public static Tile ResolveFight( Tile a, Tile b ) {
        if( unitTypeObjDict == null )
            unitTypeObjDict = new Dictionary<UnitType, Tile>();

        unitTypeObjDict.Clear();

        unitTypeObjDict.Add( a.Contains.Type, a );

        if( !unitTypeObjDict.ContainsKey( b.Contains.Type ) )
            unitTypeObjDict.Add( b.Contains.Type, b );

        // Miner beats bomb
        if( unitTypeObjDict.ContainsKey( UnitType.BOMB ) && unitTypeObjDict.ContainsKey( UnitType.MINER ) ) {
            return unitTypeObjDict[UnitType.MINER];
        }

        // Bomb kills both
        if( unitTypeObjDict.ContainsKey( UnitType.BOMB ) ) {
            return null;
        }

        // Spy kills marshall
        if( unitTypeObjDict.ContainsKey( UnitType.SPY ) && unitTypeObjDict.ContainsKey( UnitType.MARSHALL ) ) {
            return unitTypeObjDict[UnitType.SPY];
        }

        // Rank based
        if( (int) a.Contains.Type > (int) b.Contains.Type )
            return a;
        else if( (int) a.Contains.Type < (int) b.Contains.Type )
            return b;

        return null;
    }

    /**
     * If the move to the other tile is allowed
     * @param a The first tile
     * @param b The second tile
     * @returns True if the move is allowed, false if it isn't
     */
    public static bool IsValidMove( Tile a, Tile b ) {
        if( a.Contains == null && b.Contains == null )
            return false;

        if( GameState.CurrentState == EGameState.PLANNING )
            return IsValidMove_PlanningState( a, b );
        else if( GameState.CurrentState == EGameState.PLAY )
            return IsValidMove_PlayState( a, b );
        else
            return false;
    }

    /**
     * If the move to the other tile is allowed. Uses rules applicable to the PLAY state.
     * @param a The first tile
     * @param b The second tile
     * @returns True if the move is allowed, false if it isn't
     */
    private static bool IsValidMove_PlayState( Tile a, Tile b ) {
        // If the first tile doesn't have a unit on it, there's nothing to move
        if( a.Contains == null )
            return false;

        // If the distance between the two tiles is greater than 1, it's not a valid move
        if( Vector2.Distance( new Vector2( a.Position.x, a.Position.y ), new Vector2( b.Position.x, b.Position.y ) ) > 1 )
            return false;

        if( Mathf.Abs( ( a.Position.x - b.Position.x ) + ( a.Position.y - b.Position.y ) ) > 1 )
            return false;

        // You're only allowed to join a line of friendlies directly adjacent
        // You're not allowed to move backwards

        // if im moving to a different y
        // is there a friendly behind me right now or
        // is there no friendly in the next line
        // if there is don't allow

        if( !a.Contains || !b.Contains ) // As long as we're not trying to swap, but actually move to an empty spot, special rules apply!
        {
            int moveDir = -1;
            if( a.Contains.Side == Side.RED ) {
                moveDir = 1;
            }

            // dont allow moves backwards! That shit gets you shot
            if( a.Contains.Side == Side.RED ) {
                if( b.Position.x < a.Position.x ) {
                    return false;
                }
            } else {
                if( b.Position.x > a.Position.x ) {
                    return false;
                }
            }

            if( a.Position.y != b.Position.y ) // We're trying to switch line
            {
                // Is there anything directly behind me?
                Tile behindTile = GameMap.SINGLETON.GetTileAt(new GridPosition(a.Position.x - moveDir, a.Position.y));
                if( behindTile != null && behindTile.Contains ) {
                    return false;
                }

                // Is there nobody in front of me in my new place?
                if( !GameMap.SINGLETON.GetTileAt( new GridPosition( b.Position.x + moveDir, b.Position.y ) ).Contains ) {
                    return false;
                }
            }
        }

        // We can move behind one of our units, or swap with one of our units. Valid move!
        return true;
    }

    /**
     * If the move to the other tile is allowed. Uses rules applicable to the PLANNING state.
     * @param a The first tile
     * @param b The second tile
     * @returns True if the move is allowed, false if it isn't
     */
    private static bool IsValidMove_PlanningState( Tile a, Tile b ) {
        return true;
    }
}
