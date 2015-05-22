using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Unit))]
public class UnitMover : MonoBehaviour {

    private Unit Target;

    void Awake() {
        Target = GetComponent<Unit>();
    }

    void StartSmoothTowards( GridPosition p ) {
        Target.OnTile = GameMap.SINGLETON.GetTileAt( p );

        transform.position = new Vector3( Target.transform.position.x, Target.transform.position.y, Target.transform.position.z );
    }
}
