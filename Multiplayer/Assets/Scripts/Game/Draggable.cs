using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

/**
 * Attach to a gameObject that needs to be draggable
 * For example the Tile and UnitPanelUI
 */
public class Draggable : MonoBehaviour, IDragHandler, IEndDragHandler
{
    [HideInInspector]
    public bool IsUIObject;     //Object is an UI element.
    public bool DragPhysically; //Object will change position to follow the cursor.
    public bool ReturnToOrigin; //Object will return to its original position after being dragged.

    private Vector3 origin;
    private Tile tile;

    void Awake() {
        IsUIObject = !gameObject.GetComponent<Collider>();
    }

    void Start()
    {
        origin = transform.position;

        if( !IsUIObject)
            tile = GetComponent<Tile>();
    }

    public void OnDrag(PointerEventData eventData)
    {
        if( tile && tile.Contains && tile.Contains.Side == Side.RED )
            return;

        if(DragPhysically)
            transform.position += new Vector3(eventData.delta.x, eventData.delta.y);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // Don't allow dragging of enemy units
        if( tile && tile.Contains && tile.Contains.Side == Side.RED )
            return;

        // Don't allow dragging if the game has ended
        if( GameState.CurrentState == EGameState.END )
            return;

        if(DragPhysically && ReturnToOrigin)
            transform.position = origin;

        // Don't allow dragging if it's not your turn
        if( GameState.CurrentState == EGameState.PLAY && GameState.CurrentPlayerTurn == Side.RED )
            return;

        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        
        // Check which tile is under the mouse
        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            Draggable draggable = hit.collider.gameObject.GetComponent<Draggable>();

            if (draggable)
            {
                // If we're trying to drag an UI spawner onto a tile that contains a unit, don't spwan
                if( IsUIObject && draggable.GetComponent<Tile>().Contains )
                    return;

                DraggableInteraction.SINGLETON.HandleDraggableInteraction(this, draggable, true);
            }
        }
    }
}
