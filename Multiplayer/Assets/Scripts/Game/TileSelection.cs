using UnityEngine;
using System.Collections;

public class TileSelection : MonoBehaviour
{
    private Tile tile;

    void Awake()
    {
        tile = transform.parent.GetComponent<Tile>();
    }

    void Update()
    {
        if( Input.GetMouseButtonUp( 0 ) || ( Input.touchCount > 0 && Input.GetTouch( 0 ).phase == TouchPhase.Ended ) )
        {
            var ray = Camera.main.ScreenPointToRay( Input.mousePosition );
            RaycastHit hit;
            if( Physics.Raycast( ray, out hit ) )
            {
                if( hit.collider.gameObject == gameObject )
                {
                    Select();
                } else if( ( hit.collider.tag == "Terrain" || hit.collider.tag == "Tile" ) && !UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject() )
                {
                    Deselect();
                }
            }
        }
    }

    void Select()
    {
        UnitSelectorSelection.SINGLETON.SelectTile( tile );
    }

    void Deselect()
    {
        UnitSelectorSelection.SINGLETON.SelectTile( null );
    }
}
