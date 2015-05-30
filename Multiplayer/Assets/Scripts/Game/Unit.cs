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

    public void SwapWith( Tile tile )
    {
        Debug.Log( "SWAPPING: " + OnTile.Position + " with " + tile.Position );
        Debug.Log( "FROM TILE CONTAINS: " + (OnTile.Contains != null) + "   TO TILE CONTAINS: " + (tile.Contains != null) );

        Unit temp = tile.Contains;
        Tile currentTile = OnTile;

        OnTile.Contains = null;

        tile.Contains = this;
        transform.position = tile.transform.position; // temp

        if( temp )
        {
            currentTile.Contains = temp;
            temp.transform.position = currentTile.transform.position;
        }
    }

    public void Die( bool isLocal )
    {
        if( isLocal )
            RPCManager.SINGLETON.SendKill( OnTile.Position.ToVector3() );

        Debug.Log( "UNIT DIED" );

        OnTile.Contains = null;
        Destroy( gameObject );
    }
}