using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

using System.Linq;

public class GameMap : MonoBehaviour, MessageReceiver<NetworkClientInitialized>, MessageReceiver<NetworkServerInitialized>, MessageReceiver<TurnStateChange>
{

    public const int GRID_WIDTH = 10;
    public const int GRID_HEIGHT = 3;

    public List<Tile> Tiles;

    public static GameMap SINGLETON;

    private Side? winnerSide;

    void Awake()
    {
        SINGLETON = this;

        if( Tiles == null )
            Tiles = new List<Tile>( 0 );

        this.Subscribe<TurnStateChange>( Messenger.Bus );
    }

    void Update() {
        if( GameState.CurrentState == EGameState.PLAY && (winnerSide = CheckEndGame()) != null ) 
        {
            Messenger.Bus.Route( new EndGame() { winner = winnerSide.Value } );
            GameState.EndGame();
        }
    }

    public void HandleMessage( TurnStateChange msg ) {
        Debug.Log( "Next player's turn: Side." + msg.Side.ToString() );
    }

    public Tile GetTileAt( GridPosition pos )
    {
        foreach( Tile t in Tiles )
        {
            if( t.Position.x == pos.x && t.Position.y == pos.y )
                return t;
        }

        return null;
    }

    public void MoveRowForwards( int row, Side side )
    {
        var tiles = Tiles.Where( tile => tile.Contains && tile.Contains.Side == side && tile.Position.y == row ).ToList<Tile>();

        if( side == Side.RED )
            tiles.Reverse();

        foreach( Tile tile in tiles )
        {
            GridPosition moveTo;

            if( side == Side.RED )
            {
                moveTo = new GridPosition( tile.Position.x + 1, tile.Position.y );
            } else
            {
                moveTo = new GridPosition( tile.Position.x - 1, tile.Position.y );
            }

            Tile moveToTile = GetTileAt( moveTo );

            tile.Contains.SwapWith( moveToTile );

            Vector3 tilePos = tile.Position.ToVector3();
            Vector3 moveToPos = moveTo.ToVector3();

            RPCManager.SINGLETON.SendMove( tile.Position.ToVector3(), moveTo.ToVector3() );
        }
    }

    public Side? CheckEndGame() 
    {
        for( int i = 0; i < 3; i++ ) {
            bool foundBlue = false;
            bool foundRed = false;

            var tiles = Tiles.Where( tile => tile.Contains && tile.Position.y == i ).ToList<Tile>();

            foreach( Tile t in tiles ) {
                if( t.Side == Side.BLUE )
                    foundBlue = true;
                else
                    foundRed = true;
            }

            if( !foundBlue )
                return Side.RED;

            if( !foundRed )
                return Side.BLUE;
        }

        return null;
    }

    public void ClearTiles()
    {
        foreach( Tile t in Tiles )
        {
            Destroy( t.gameObject );
        }

        Tiles.Clear();
    }

    public string EncodeFormation()
    {
        string encodedFormation = "";

        foreach( Tile t in Tiles )
        {
            if( t.Side == Side.RED )
                continue;

            if( t.Contains )
                encodedFormation += (int) t.Contains.Type;
        }

        return encodedFormation;
    }

    public void DecodeFormation( string formation, Side side )
    {
        char[] formationArray = formation.ToCharArray();
        int i = 0;

        for( int y = 0; y < GRID_HEIGHT; y++ )
        {
            for( int x = ( GRID_WIDTH / 2 ) - 1; x >= 0; x-- )
            {
                Tile tile = GetTileAt( new GridPosition( x, y ) );

                GameObject spawner = GameObject.Instantiate( Resources.Load( "UnitSpawner" ) as GameObject );
                spawner.GetComponent<UnitSpawner>().Init( tile, (UnitType) int.Parse( formationArray[ i ].ToString() ), side );

                i++;
            }
        }
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