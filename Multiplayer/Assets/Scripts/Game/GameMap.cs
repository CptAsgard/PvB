using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameMap : MonoBehaviour {

    public const int GRID_WIDTH = 3;
    public const int GRID_HEIGHT = 10;

    public List<Tile> Tiles;

    public static GameMap SINGLETON;

    void Awake() {
        SINGLETON = this;

        if( Tiles == null )
            Tiles = new List<Tile>( 0 );
    }

    public Tile GetTileAt( GridPosition pos ) {
        foreach( Tile t in Tiles ) {
            if( t.Position == pos )
                return t;
        }

        return null;
    }

    public void MoveRowForwards( int row, Side side ) {

    }

    public void ClearTiles() {
        foreach( Tile t in Tiles ) {
            Destroy( t.gameObject );
        }

        Tiles.Clear();
    }
}
