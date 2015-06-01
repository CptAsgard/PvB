using UnityEngine;
using System.Collections;

public class Tile : MonoBehaviour, MessageReceiver<StartGame>
{

    private Side __side;
    public Side Side {
        get {
            return __side;
        }
        set {
            __side = value;
            //SetColor( value );
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

    private Unit __contains;
    public Unit Contains
    {
        get
        {
            return __contains;
        }
        set
        {
            __contains = value;

            if( value )
                __contains.OnTile = this;
        }
    }

    [SerializeField]
    private Material BlueMaterial;

    [SerializeField]
    private Material RedMaterial;

    void Start()
    {
        this.Subscribe<StartGame>( Messenger.Bus );
    }

    private void SetWorldPosition() {
        gameObject.transform.position = new Vector3( (Position.x * 0.07f) - 0.025f, 0, (Position.y * 0.0755f) + 1.0245f );
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

    public void HandleMessage( StartGame msg )
    {
        Vector3 moveTo = new Vector3( transform.position.x + ( Side == Side.BLUE ? -0.391f : 0.391f ), transform.position.y, transform.position.z );
        iTween.MoveTo( gameObject, iTween.Hash( "delay", 4f, "time", 4, "position", moveTo, "easetype", iTween.EaseType.easeInOutQuad ) );
    }
}
