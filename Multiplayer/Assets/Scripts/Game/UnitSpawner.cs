using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

/**
 * Handles spawning of units on the game board
 */
public class UnitSpawner : MonoBehaviour {

    public GameObject UnitPrefabTEMP;
    public List<Sprite> UnitSprites;
    public Transform root;

    private Tile SpawnOnTile;
    private UnitType Type;

    /**
     * Spawn the unit and remove this spawner
     * @param spawnOn The tile to spawn the unit on
     * @param type The type (rank) of the unit
     * @param side The side of the unit (RED or BLUE)
     */
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
        
        if( unit.Side == Side.BLUE )
            unit.transform.GetChild( 0 ).GetChild( 0 ).GetComponent<SpriteRenderer>().sprite = UnitSprites[ (int) unit.Type ];
        else
            unit.transform.GetChild( 0 ).GetChild( 0 ).GetComponent<SpriteRenderer>().sprite = UnitSprites[ 8 ];

        spawnOn.Contains = unit;

        Destroy( this.gameObject );
    }
}