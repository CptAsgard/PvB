using UnityEngine;
using System.Collections;

public class UnitSpawner : MonoBehaviour {

    public GameObject UnitPrefabTEMP;

    private Tile SpawnOnTile;
    private UnitType Type;

    public void Init( Tile spawnOn, UnitType type, Side side )
    {
        transform.position = spawnOn.transform.position;
        this.Type = type;

        var g = GameObject.Instantiate( UnitPrefabTEMP, transform.position, transform.rotation ) as GameObject;
        var unit = g.GetComponent<Unit>();

        unit.OnTile = spawnOn;
        unit.Type = type;
        unit.Side = side;

        spawnOn.Contains = unit;

        Destroy( this.gameObject );
    }
}