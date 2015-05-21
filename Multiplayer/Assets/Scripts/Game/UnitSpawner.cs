using UnityEngine;
using System.Collections;

public class UnitSpawner : MonoBehaviour {

    public GameObject UnitPrefabTEMP;

    private Tile SpawnOnTile;
    private UnitType Type;

    public void Init( Tile spawnOn, UnitType type )
    {
        transform.position = spawnOn.transform.position;
        this.Type = type;

        var g = GameObject.Instantiate( UnitPrefabTEMP, transform.position, transform.rotation ) as GameObject;
        var unit = g.GetComponent<Unit>();

        unit.OnTile = spawnOn;
        spawnOn.UnitOnTile = unit;

        Destroy( this.gameObject );
    }
}