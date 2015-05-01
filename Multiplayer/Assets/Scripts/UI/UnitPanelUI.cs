using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class UnitPanelUI : MonoBehaviour, IPointerClickHandler, IDragHandler, IEndDragHandler
{
    public UnitType Type;
    public int AmountSpawnable;

    private UnitPanelSelectionUI selection;

    private RectTransform _transform = null;
    private Vector3 originalPos;

    void Start()
    {
        _transform = GetComponent<RectTransform>();
        selection = transform.parent.GetComponent<UnitPanelSelectionUI>();

        originalPos = _transform.position;
    }

    public void OnPointerClick( PointerEventData eventData )
    {
        selection.SelectedPanel = this;
    }

    public void OnDrag( PointerEventData eventData )
    {
        _transform.position += new Vector3( eventData.delta.x, eventData.delta.y );
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        ReturnToStartingPos();
    }

    public void ReturnToStartingPos()
    {
        _transform.position = originalPos;
    }
}