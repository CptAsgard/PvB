using UnityEngine;
using System.Collections;

public enum Side
{
    RED,
    BLUE
}

public enum UnitType
{
    BOMB = 0,       // Destroys all and self
    SPY = 1,        // Only defeats Marshall
    MINER = 2,      // Destroys bomb without dying
    SCOUT = 3,
    LIEUTENANT = 4,
    COLONEL = 5,
    GENERAL = 6,
    MARSHALL = 7
}

public class Unit : MonoBehaviour {

    public Tile OnTile;
    
    public Side Side;
    public UnitType Type;

    public void MoveTo( Tile tile ) 
    {
        // Is there a unit on the tile I have to move to?
        // Tell it to take my position instead!
        if( tile == OnTile )
            return;

        if( tile.UnitOnTile == this )
            return;

        tile.UnitOnTile = this;

        transform.position = tile.transform.position;
    }

    public void SwapWith( Tile tile )
    {
        if( tile == OnTile )
            return;

        if( tile.UnitOnTile == this )
            return;

        if( tile.UnitOnTile != null )
            tile.UnitOnTile.MoveTo( this.OnTile );

        tile.UnitOnTile = this;

        transform.position = tile.transform.position;
    }
}