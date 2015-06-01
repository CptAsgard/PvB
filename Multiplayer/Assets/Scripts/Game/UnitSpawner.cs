using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UnitSpawner : MonoBehaviour {

    public GameObject UnitPrefabTEMP;

    public Transform root;

    private Tile SpawnOnTile;
    private UnitType Type;

    public void Init( Tile spawnOn, UnitType type, Side side )
    {
        string findString = "- Units" + ( side == Side.BLUE ? "BLUE" : "RED" );
        root = GameObject.Find( findString ).transform;

        transform.position = spawnOn.transform.position;
        this.Type = type;

        var g = GameObject.Instantiate( UnitPrefabTEMP, transform.position, transform.rotation ) as GameObject;
        var unit = g.GetComponent<Unit>();
        g.transform.parent = root;

        unit.OnTile = spawnOn;
        unit.Type = type;
        unit.Side = side;
        unit.transform.GetChild(0).GetComponent<MeshRenderer>().material.color = ( unit.Side == Side.RED ? Color.red : Color.blue );

        if( side == Side.RED )
            unit.transform.GetChild( 1 ).gameObject.SetActive( false );

        spawnOn.Contains = unit;

        Destroy( this.gameObject );
    }
}