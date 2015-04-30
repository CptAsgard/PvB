using UnityEngine;
using System.Collections;

public class UnitSpawner : MonoBehaviour {

    private Tile SpawnOnTile;
    private UnitType Type;

    public void Init( Tile spawnOn, UnitType type )
    {
        transform.position = spawnOn.transform.position;
        this.Type = type;
    }
}