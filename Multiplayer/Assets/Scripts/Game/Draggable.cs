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

    void Awake() {
        IsUIObject = !gameObject.GetComponent<Collider>();
    }

    void Start()
    {
        origin = transform.position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if(DragPhysically)
            transform.position += new Vector3(eventData.delta.x, eventData.delta.y);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if(DragPhysically && ReturnToOrigin)
            transform.position = origin;

        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
 
        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            Draggable draggable = hit.collider.gameObject.GetComponent<Draggable>();
            if (draggable)
            {
                DraggableInteraction.SINGLETON.HandleDraggableInteraction(this, draggable, true);
                if (GameState.CurrentState == EGameState.PLAY)
                {
                    Tile t = GetComponent<Tile>();
                    Tile t2 = draggable.GetComponent<Tile>();
                    RPCManager.SINGLETON.SendMove(new Vector3(t.Position.x, t.Position.y, 0), new Vector3(t2.Position.x, t2.Position.y, 0));
                }
            }
        }
    }
}
