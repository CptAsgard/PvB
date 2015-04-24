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

}