using UnityEngine;
using System.Collections;

public class DraggableInteraction {
    
    private static DraggableInteraction _instance;

    private DraggableInteraction() { }

    public static DraggableInteraction SINGLETON
    {
        get
        {
            if(_instance == null)
                _instance = new DraggableInteraction();

            return _instance;
        }
    }

    public void HandleDraggableInteraction(Draggable a, Draggable b)
    {
        if (!a || !b) //One or more objects are null.
            return;

        if (a.IsUIObject && b.IsUIObject) //Both objects are UI objects.
            return;

        Debug.Log("Dragged " + a.gameObject.name + " onto " + b.gameObject.name);

        if (a.IsUIObject) //Try spawning a unit.
        {
            Spawn( a.GetComponent<UnitPanelUI>(), b.GetComponent<Tile>() );
        }
        else //Try swapping.
        {
            Swap( a.GetComponent<Tile>(), b.GetComponent<Tile>() );
        }
    }

    void Swap( Tile swapFrom, Tile swapTo )
    {
        if( !swapFrom || !swapTo )
            return;

        if( ValidMoveCheck.IsValidMove( swapFrom, swapTo ) )
            swapFrom.Contains.SwapWith( swapTo );
    }

    void Spawn( UnitPanelUI unitPanel, Tile spawnOn )
    {
        if( !unitPanel || !spawnOn ) // One or more objects are null
            return;

        if( unitPanel.AmountSpawnable <= 0 )
            return;

        GameObject spawner = GameObject.Instantiate( Resources.Load( "UnitSpawner" ) as GameObject );
        spawner.GetComponent<UnitSpawner>().Init( spawnOn, unitPanel.Type );

        unitPanel.AmountSpawnable--;
    }
}
