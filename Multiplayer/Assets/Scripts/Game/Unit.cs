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

/**
 * Unit component to handle swapping and death + network replication
 */
public class Unit : MonoBehaviour {

    public Tile OnTile;
    
    public Side Side;
    public UnitType Type;

    /**
     * Move this unit to the specified tile. Move the unit on that specified tile to this position.
     * @param tile The tile to move to
     */
    public void SwapWith( Tile tile )
    {
        Debug.Log( "SWAPPING: " + OnTile.Position + " with " + tile.Position );
        Debug.Log( "FROM TILE CONTAINS: " + (OnTile.Contains != null) + "   TO TILE CONTAINS: " + (tile.Contains != null) );

        Unit temp = tile.Contains;
        Tile currentTile = OnTile;

        OnTile.Contains = null;

        tile.Contains = this;
        //transform.position = tile.transform.position; // temp

        if( temp )
        {
            currentTile.Contains = temp;
            //temp.transform.position = currentTile.transform.position;
        }

        Messenger.Bus.Route(new PlaySound()
        {
            audioclip = 0
        });
    }

    /**
     * Kill this unit
     * @param isLocal If this function is called locally or through RPC
     */
    public void Die( bool isLocal )
    {
        if( isLocal )
            RPCManager.SINGLETON.SendKill( OnTile.Position.ToVector3() );

        Debug.Log( "UNIT DIED" );

        OnTile.Contains = null;

        iTween.ColorTo( gameObject.transform.GetChild( 0 ).GetChild( 0 ).gameObject, iTween.Hash( "time", 0.5f, "r", 0.0f, "g", 0.0f, "b", 0.0f, "a", 0.0f, "oncomplete", "KillSelf" ) );

        Messenger.Bus.Route<PlaySound>(new PlaySound()
        {
            audioclip = 1
        });
    }

    private void KillSelf()
    {
        Destroy( gameObject );
    }
}