using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

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
        if( tile && tile.Contains && tile.Contains.Side == Side.RED )
            return;

        if( GameState.CurrentState == EGameState.END )
            return;

        if(DragPhysically && ReturnToOrigin)
            transform.position = origin;

        if( GameState.CurrentState == EGameState.PLAY && GameState.CurrentPlayerTurn == Side.RED )
            return;

        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
 
        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            Draggable draggable = hit.collider.gameObject.GetComponent<Draggable>();

            if (draggable)
            {
                if( IsUIObject && draggable.GetComponent<Tile>().Contains )
                    return;

                DraggableInteraction.SINGLETON.HandleDraggableInteraction(this, draggable, true);
                if (GameState.CurrentState == EGameState.PLAY)
                {
                    //Tile t = GetComponent<Tile>();
                    //Tile t2 = draggable.GetComponent<Tile>();
                    //RPCManager.SINGLETON.SendMove(new Vector3(t.Position.x, t.Position.y, 0), new Vector3(t2.Position.x, t2.Position.y, 0));
                }
            }
        }
    }
}
