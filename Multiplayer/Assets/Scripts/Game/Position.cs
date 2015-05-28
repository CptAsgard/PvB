using UnityEngine;
using System.Collections;

/**
 * @author Frank van Wattingen
 * 
 * Simple class to represent grid positions.
 */ 
public sealed class GridPosition {

    public int x, y;

    public GridPosition( int x, int y ) {
        this.x = x;
        this.y = y;
    }

    public GridPosition( Vector3 pos ) {
        this.x = (int) pos.x;
        this.y = (int) pos.y;
    }

    public GridPosition( Vector2 pos ) {
        this.x = (int) pos.x;
        this.y = (int) pos.y;
    }

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