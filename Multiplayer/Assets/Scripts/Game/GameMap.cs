using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

using System.Linq;

/**
 * Singleton class that contains the map of the game with tiles, and map-affecting convenience functions
 */
public class GameMap : MonoBehaviour, MessageReceiver<NetworkClientInitialized>, MessageReceiver<NetworkServerInitialized>, MessageReceiver<TurnStateChange>
{

    public const int GRID_WIDTH = 10;
    public const int GRID_HEIGHT = 3;

    public List<Tile> Tiles;

    public static GameMap SINGLETON;

    // Who has won the game, null if no winner
    private Side? winnerSide;

    void Awake()
    {
        SINGLETON = this;

        if( Tiles == null )
            Tiles = new List<Tile>( 0 );

        this.Subscribe<TurnStateChange>( Messenger.Bus );
    }

    void Update()
    {
        if( GameState.CurrentState == EGameState.PLAY && ( winnerSide = CheckEndGame() ) != null )
        {
            Messenger.Bus.Route( new EndGame() { winner = winnerSide.Value } );
            GameState.EndGame();
        }
    }

    public void HandleMessage( TurnStateChange msg )
    {
        Debug.Log( "Next player's turn: Side." + msg.Side.ToString() );
    }

    /**
     * Returns the tile at the specified grid position
     * @param pos The grid position at where you want to get the tile
     * @returns The tile at that grid position. Null if no tile found at that position
     */
    public Tile GetTileAt( GridPosition pos )
    {
        foreach( Tile t in Tiles )
        {
            if( t.Position.x == pos.x && t.Position.y == pos.y )
                return t;
        }

        return null;
    }

    /**
     * Move an entire row of a side forwards
     * @param row The row you want to move forwards
     * @param side The side of units that you want to move forwards
     */
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

            iTween.MoveTo( tile.Contains.gameObject, iTween.Hash( "position", moveToTile.transform.position, "time", 1, "easetype", iTween.EaseType.easeInOutSine ) );

            tile.Contains.SwapWith( moveToTile );

            Vector3 tilePos = tile.Position.ToVector3();
            Vector3 moveToPos = moveTo.ToVector3();

            RPCManager.SINGLETON.SendMove( tile.Position.ToVector3(), moveTo.ToVector3() );
        }
    }

    /**
     * Has the game ended?
     * @returns Enum of the winning side, null if no winner (yet)
     */
    public Side? CheckEndGame()
    {
        for( int i = 0; i < 3; i++ )
        {
            bool foundBlue = false;
            bool foundRed = false;

            var tiles = Tiles.Where( tile => tile.Contains && tile.Position.y == i ).ToList<Tile>();

            foreach( Tile t in tiles )
            {
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

    /**
     * Clears the list of tiles, and DESTROYS all tiles, to allow regeneration
     */
    public void ClearTiles()
    {
        foreach( Tile t in Tiles )
        {
            Destroy( t.gameObject );
        }

        Tiles.Clear();
    }

    /**
     * Encode a string of our formation. Formatted as a string of numbers, where
     * the number signifies the rank.
     * @returns Encoded string containing a sequential list of numbers that represent
     * the formation.
     */
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

    /**
     * Tries to decode a string of sequential encoded formation info, and 
     * spawns the unit on the correct position
     * @param formation The sequential encoded formation string
     * @param side The side that this formation info belongs to
     */
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