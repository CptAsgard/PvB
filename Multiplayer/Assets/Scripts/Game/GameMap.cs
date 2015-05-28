using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class GameMap : MonoBehaviour, MessageReceiver<NetworkClientInitialized>, MessageReceiver<NetworkServerInitialized> {

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
            if( t.Position.x == pos.x && t.Position.y == pos.y )
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

        foreach (Tile t in Tiles)
        {
            if( t.Side == Side.RED )
                continue;

            if (t.Contains)
                encodedFormation += (int) t.Contains.Type;
        }

        return encodedFormation;
    }

    public void DecodeFormation(string formation)
    {
        char[] formationArray = formation.ToCharArray();
        int i = 0;

        for (int y = 0; y < GRID_HEIGHT; y++)
        {
            for (int x = (GRID_WIDTH / 2) - 1; x >= 0; x--)
            {
                Tile tile = GetTileAt(new GridPosition(x, y));

                GameObject spawner = GameObject.Instantiate(Resources.Load("UnitSpawner") as GameObject);
                spawner.GetComponent<UnitSpawner>().Init(tile, (UnitType) int.Parse(formationArray[i].ToString()));

                i++;
            }
        }

        Debug.Log("Complete formation: " + EncodeFormation());
    }

    public void HandleMessage( NetworkClientInitialized msg )
    {
        GameState.Initialize( Side.RED );
    }

    public void HandleMessage( NetworkServerInitialized msg )
    {
        GameState.Initialize( Side.BLUE );
    }
}