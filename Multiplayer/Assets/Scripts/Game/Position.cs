using UnityEngine;
using System.Collections;

/**
 * @author Frank van Wattingen
 * 
 * Simple class to represent grid positions.
 */ 
public sealed class GridPosition {

    public int x, y;

    /**
     * CONSTRUCTOR: Sets the grid position to the specified X and Y
     * @param x The x position
     * @param y The y position
     */
    public GridPosition( int x, int y ) {
        this.x = x;
        this.y = y;
    }

    /**
     * CONSTRUCTOR: Takes the X and Y of a Vector3 and sets the grid position to it
     * @param pos The vector3 to take the X and Y from
     */
    public GridPosition( Vector3 pos ) {
        this.x = (int) pos.x;
        this.y = (int) pos.y;
    }

    /**
     * CONSTRUCTOR: Constructs a GridPosition from the X and Y components of a Vector2. Casts to int!
     * @param pos Vector2 pos to initialize the grid to.
     */
    public GridPosition( Vector2 pos ) {
        this.x = (int) pos.x;
        this.y = (int) pos.y;
    }

    /**
     * Returns the GridPosition as a vector formatted Vector3(x, y, 0)
     * @returns The Vector3 formatted Vector3(x, y, 0)
     */
    public Vector3 ToVector3() {
        return new Vector3( x, y, 0 );
    }

    public override string ToString() {
        return "GridPosition( x: " + x + " y: " + y + ")";
    }

    public override bool Equals( System.Object obj )
    {
        // If parameter is null return false.
        if( obj == null )
        {
            return false;
        }

        // If parameter cannot be cast to Point return false.
        GridPosition p = obj as GridPosition;
        if( (System.Object) p == null )
        {
            return false;
        }

        // Return true if the fields match:
        return ( x == p.x ) && ( y == p.y );
    }

    public bool Equals( GridPosition p )
    {
        // If parameter is null return false:
        if( (object) p == null )
        {
            return false;
        }

        // Return true if the fields match:
        return ( x == p.x ) && ( y == p.y );
    }

    public override int GetHashCode()
    {
        return x ^ y;
    }
}