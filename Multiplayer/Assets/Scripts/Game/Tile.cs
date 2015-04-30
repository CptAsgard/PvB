using UnityEngine;
using System.Collections;

public class Tile : MonoBehaviour {

    private Side __side;
    public Side Side {
        get {
            return __side;
        }
        set {
            __side = value;
            SetColor( value );
        }
    }

    private GridPosition __position;
    public GridPosition Position {
        get {
            return __position;
        }
        set {
            __position = value;
            SetWorldPosition();
        }
    }

    public Unit Contains;

    [SerializeField]
    private Material BlueMaterial;

    [SerializeField]
    private Material RedMaterial;

    private void SetWorldPosition() {
        gameObject.transform.position = new Vector3( Position.x * 1.5f, 0, Position.y * 1.5f );
    }

    private void SetColor( Side side ) {
        switch( side ) {
            case global::Side.BLUE:
                gameObject.GetComponentInChildren<Renderer>().material = BlueMaterial;
                break;

            case global::Side.RED:
                gameObject.GetComponentInChildren<Renderer>().material = RedMaterial;
                break;
        }
    }

    public void OnTileSelected()
    {
        GetComponentInChildren<Renderer>().material.color = Color.magenta;
    }

    public void OnTileDeselected()
    {
        GetComponentInChildren<Renderer>().material.color = Color.green;
    }
}
