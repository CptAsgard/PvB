using UnityEngine;
using System.Collections;

/**
 * Handles the interaction between two draggables.
 * Could mean a spawn, swap, or fight.
 */
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

            // If you're dragging a unit onto a unit of a different side
            if( second.Contains && first.Contains.Side != second.Contains.Side )
            {
                Fight( first, second, isLocal );
            } else
                Swap( first, second, isLocal );
        }
    }

    /**
     * Handles swap interaction and network replication between two draggables
     * @param swapFrom Which tile to swap from
     * @param swapTo Which tile to swap to
     * @param isLocal True if called locally, false if called by RPC.
     */
    void Swap( Tile swapFrom, Tile swapTo, bool isLocal )
    {
        if( !swapFrom || !swapTo )
            return;

        if( !isLocal || ( isLocal && ValidMoveCheck.IsValidMove( swapFrom, swapTo ) ) ) {
            if( swapFrom.Contains )
                iTween.MoveTo( swapFrom.Contains.gameObject, iTween.Hash( "position", swapTo.transform.position, "time", 1, "easetype", iTween.EaseType.easeInOutSine ) );

            if( swapTo.Contains )
                iTween.MoveTo( swapTo.Contains.gameObject, iTween.Hash( "position", swapFrom.transform.position, "time", 1, "easetype", iTween.EaseType.easeInOutSine ) );
            
            swapFrom.Contains.SwapWith( swapTo );

            // Replicate behaviour to other person if it's a local call
            if( isLocal && GameState.CurrentState == EGameState.PLAY)
                RPCManager.SINGLETON.SendMove( new Vector3( swapFrom.Position.x, swapFrom.Position.y, 0 ), new Vector3( swapTo.Position.x, swapTo.Position.y, 0 ) );

            Debug.Log( "isLocal: " + isLocal + "  SWAPPED" );
        }
    }

    /**
     * Handles fight interaction and network replication between two draggables
     * @param fightFrom Which tile wants to fight
     * @param fightTo Which tile to fight with
     * @param isLocal True if called locally, false if called by RPC.
     */
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
                if( isLocal ) GameMap.SINGLETON.MoveRowForwards( fightFrom.Position.y, fightFrom.Contains.Side ); // MoveRowForwards handles replication
                else Swap( fightFrom, fightTo, isLocal ); // If we are receicing a fight from the other player, replicate the fight
            } 
            else if( winner == fightTo )
            {
                Debug.Log( "isLocal: " + isLocal + "fightTo wins!" );

                fightFrom.Contains.Die( isLocal );
                if( isLocal ) GameMap.SINGLETON.MoveRowForwards( fightTo.Position.y, fightTo.Contains.Side ); // MoveRowForwards handles replication
                else Swap( fightFrom, fightTo, isLocal ); // If we are receicing a fight from the other player, replicate the fight
            } 
            else
            {
                Debug.Log( "Nobody wins! " );

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
        }
    }

    /**
     * Handles spawn interaction between UI draggable and Tile draggable
     * @param unitPanel The UI panel the spawn is happening with
     * @param spawnOn The tile that the user wants to spawn the unit on
     */
    public void Spawn( UnitPanelUI unitPanel, Tile spawnOn )
    {
        if( !unitPanel || !spawnOn ) // One or more objects are null
            return;

        if( unitPanel.AmountSpawnable <= 0 )
            return;


        Messenger.Bus.Route<PlaySound>(new PlaySound()
        {
            audioclip = 2
        });
        GameObject spawner = GameObject.Instantiate( Resources.Load( "UnitSpawner" ) as GameObject );
        spawner.GetComponent<UnitSpawner>().Init( spawnOn, unitPanel.Type, Side.BLUE );

        unitPanel.AmountSpawnable--;
    }
}
