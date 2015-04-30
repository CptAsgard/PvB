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
        if( Input.GetMouseButtonUp( 0 ) )
        {
            var ray = Camera.main.ScreenPointToRay( Input.mousePosition );
            RaycastHit hit;
            if( Physics.Raycast( ray, out hit ) )
            {
                if( hit.collider.gameObject == gameObject )
                {
                    Select();
                }
            } else
            {
                Deselected();
            }
        }
    }

    void Select()
    {
        UnitSelectorSelection.SINGLETON.SelectTile( tile );
    }

    void Deselected()
    {
        UnitSelectorSelection.SINGLETON.SelectTile( null );
    }
}
