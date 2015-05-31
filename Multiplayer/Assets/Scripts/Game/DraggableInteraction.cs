using UnityEngine;
using System.Collections;

public class DraggableInteraction
{

    private static DraggableInteraction _instance;

    private DraggableInteraction() { }

    public static DraggableInteraction SINGLETON
    {
        get
        {
            if( _instance == null )
                _instance = new DraggableInteraction();

            return _instance;
        }
    }

    public void HandleDraggableInteraction( Draggable a, Draggable b, bool isLocal )
    {
        if( !a || !b ) //One or more objects are null.
            return;

        if( a.IsUIObject && b.IsUIObject ) //Both objects are UI objects.
            return;

        if( a.IsUIObject ) //Try spawning a unit.
        {
            var tile = b.GetComponent<Tile>();
            var unit = a.GetComponent<UnitPanelUI>();

            if( tile.Side == Side.RED )
                return;

            Spawn( unit, tile );
        } else //Try swapping.
        {
            Tile first, second;
            first = a.GetComponent<Tile>();
            second = b.GetComponent<Tile>();

            if( second.Contains && first.Contains.Side != second.Contains.Side )
            {
                if( first.Contains == null )
                    Debug.Log( "FIRST IS NULL! " + first.Position );
                else if( second.Contains == null )
                    Debug.Log( "SECOND IS NULL! " + second.Position );

                Debug.Log( first.Contains.Side + ", " + second.Contains.Side );

                Fight( first, second, isLocal );
            } else
                Swap( first, second, isLocal );
        }
    }

    void Swap( Tile swapFrom, Tile swapTo, bool isLocal )
    {
        if( !swapFrom || !swapTo )
            return;

        if( isLocal && ValidMoveCheck.IsValidMove( swapFrom, swapTo ) )
            swapFrom.Contains.SwapWith( swapTo );
        else
            swapFrom.Contains.SwapWith( swapTo );

        if( isLocal )
            RPCManager.SINGLETON.SendMove( new Vector3( swapFrom.Position.x, swapFrom.Position.y, 0 ), new Vector3( swapTo.Position.x, swapTo.Position.y, 0 ) );

        if( !isLocal && GameState.CurrentState == EGameState.PLAY && GameState.CurrentPlayerTurn == Side.RED )
            GameState.NextPlayerTurn();

        Debug.Log( "isLocal: " + isLocal + "  SWAPPED" );
    }

    void Fight( Tile fightFrom, Tile fightTo, bool isLocal )
    {
        if( !fightFrom || !fightTo )
            return;

        if( ValidMoveCheck.IsValidMove( fightFrom, fightTo ) && ValidMoveCheck.CanFight( fightFrom, fightTo ) )
        {
            Tile winner = ValidMoveCheck.ResolveFight( fightFrom, fightTo );

            if( winner == fightFrom )
            {
                Debug.Log( "isLocal: " + isLocal + "fightFrom wins!" );

                fightTo.Contains.Die( isLocal );
                if( isLocal ) GameMap.SINGLETON.MoveRowForwards( fightFrom.Position.y, fightFrom.Contains.Side );
                else Swap( fightFrom, fightTo, isLocal );
            } 
            else if( winner == fightTo )
            {
                Debug.Log( "isLocal: " + isLocal + "fightTo wins!" );

                fightFrom.Contains.Die( isLocal );
                if( isLocal ) GameMap.SINGLETON.MoveRowForwards( fightTo.Position.y, fightTo.Contains.Side );
                else Swap( fightFrom, fightTo, isLocal );
            } 
            else
            {
                Debug.Log( "Nobody wins! " );

                //if( isLocal ) RPCManager.SINGLETON.SendMove( new Vector3( fightFrom.Position.x, fightFrom.Position.y, 0 ), new Vector3( fightTo.Position.x, fightTo.Position.y, 0 ) );

                int fightFromY, fightToY;
                fightFromY = fightFrom.Position.y;
                fightToY = fightTo.Position.y;

                Side fightFromSide, fightToSide;
                fightFromSide = fightFrom.Side;
                fightToSide = fightTo.Side;

                fightTo.Contains.Die( isLocal );
                fightFrom.Contains.Die( isLocal );

                if( isLocal ) GameMap.SINGLETON.MoveRowForwards( fightFromY, fightFromSide );
                if( isLocal ) GameMap.SINGLETON.MoveRowForwards( fightToY, (fightFromSide == Side.RED ? Side.BLUE : Side.RED) );
            }

            if( GameState.CurrentState == EGameState.PLAY )
                GameState.NextPlayerTurn();
        }
    }

    public void Spawn( UnitPanelUI unitPanel, Tile spawnOn )
    {
        if( !unitPanel || !spawnOn ) // One or more objects are null
            return;

        if( unitPanel.AmountSpawnable <= 0 )
            return;

        GameObject spawner = GameObject.Instantiate( Resources.Load( "UnitSpawner" ) as GameObject );
        spawner.GetComponent<UnitSpawner>().Init( spawnOn, unitPanel.Type, Side.BLUE );

        unitPanel.AmountSpawnable--;
    }
}
