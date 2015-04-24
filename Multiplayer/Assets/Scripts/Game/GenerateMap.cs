using UnityEngine;
using System.Collections;

public class GenerateMap : MonoBehaviour {

    public GameObject TilePrefab;

    public bool ForceRegen;

	// Use this for initialization
	void Start () {
        if( GameMap.SINGLETON.Tiles.Count != 0 && !ForceRegen )
            return;

        StartCoroutine( GenMap() );
	}

    IEnumerator GenMap() {
        GameObject root = new GameObject();
        root.name = "- Tiles";

        GameMap.SINGLETON.ClearTiles();

        for( int i = 0; i < GameMap.GRID_WIDTH * GameMap.GRID_HEIGHT; i++ ) {
            Tile t = GameObject.Instantiate( TilePrefab ).GetComponent<Tile>();
            t.transform.parent = root.transform;

            t.Position = new GridPosition( i % GameMap.GRID_WIDTH, i / GameMap.GRID_WIDTH );

            if( i / GameMap.GRID_WIDTH > 4 ) {
                t.Side = Side.BLUE;
            } else {
                t.Side = Side.RED;
            }

            GameMap.SINGLETON.Tiles.Add( t );
        }

        yield return null;

        // No longer needed after map generation is done
        Destroy( this );
    }
}
