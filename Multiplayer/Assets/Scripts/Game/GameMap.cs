using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameMap : MonoBehaviour {

    public const int GRID_WIDTH = 10;
    public const int GRID_HEIGHT = 3;

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

    public string EncodeFormation()
    {
        string encodedFormation = "";

        foreach( Tile t in Tiles )
            encodedFormation += t.Contains.Type;

        return encodedFormation;
    }

    public void DecodeFormation()
    {

    }
}

// check formation if filled, skip red tiles
// if formation full return true
// if true show button
// if button pressed EncodeFormation() and RPC