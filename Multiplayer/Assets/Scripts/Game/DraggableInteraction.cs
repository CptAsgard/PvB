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
                Swap( first, second );
        }
    }

    void Swap( Tile swapFrom, Tile swapTo )
    {
        if( !swapFrom || !swapTo )
            return;

        if( ValidMoveCheck.IsValidMove( swapFrom, swapTo ) )
            swapFrom.Contains.SwapWith( swapTo );

        Debug.Log( "SWAPPED" );
    }

    void Fight( Tile fightFrom, Tile fightTo, bool isLocal )
    {
        if( !fightFrom || !fightTo )
            return;

        if( ValidMoveCheck.IsValidMove( fightFrom, fightTo ) )
        {
            Unit winner = ValidMoveCheck.ResolveFight( fightFrom.Contains, fightTo.Contains );

            if( winner == fightFrom.Contains )
            {
                fightTo.Contains.Die();
                if( isLocal ) GameMap.SINGLETON.MoveRowForwards( fightFrom.Position.y, fightFrom.Contains.Side );
            } else if( winner == fightTo.Contains )
            {
                fightFrom.Contains.Die();
                if( isLocal ) GameMap.SINGLETON.MoveRowForwards( fightTo.Position.y, fightTo.Contains.Side );
            } else
            {
                fightTo.Contains.Die();
                fightFrom.Contains.Die();

                if( isLocal ) GameMap.SINGLETON.MoveRowForwards( fightFrom.Position.y, fightFrom.Contains.Side );
                if( isLocal ) GameMap.SINGLETON.MoveRowForwards( fightTo.Position.y, fightTo.Contains.Side );
            }
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
