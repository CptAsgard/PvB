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

    public delegate void OnMoveTo( GridPosition to );
    public OnMoveTo onMoveTo;

    public delegate void OnSwapWith( GridPosition target );
    public OnMoveTo onSwapWith;

    public void MoveToPosition( GridPosition tile ) 
    {
        // Trigger event
        onMoveTo( tile );
    }

    public void SwapWith( GridPosition tile ) 
    {
        // Trigger event
        onSwapWith( tile );
    }
}