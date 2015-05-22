using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class UnitSelectorDrag : MonoBehaviour
{
    private Tile startingTile;

    public void OnDrag( BaseEventData eventData )
    {
        // Saves original position and records which unit is being dragged
        var ptrEventData = (PointerEventData) eventData;

        Debug.Log( "start drag" );

        startingTile = ptrEventData.pointerCurrentRaycast.gameObject.transform.parent.GetComponent<Tile>();
    }

    public void OnEndDrag( BaseEventData eventData )
    {
        // move unit that's currently being dragged to the place where this drag stopped OR return to original position
        var ptrEventData = (PointerEventData) eventData;
        
        Tile tileTo;

        if( (tileTo = ptrEventData.pointerCurrentRaycast.gameObject.transform.parent.GetComponent<Tile>()).transform.tag == "Tile" )
        {
            if( startingTile == tileTo )
                return;

            // Swap units (or not if there's nothing to swap with)!
            startingTile.Contains.SwapWith( tileTo );
        }
    }
}