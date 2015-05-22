using UnityEngine;
using System.Collections;

public class TileSelection : MonoBehaviour
{
    private Tile tile;

    void Awake()
    {
        tile = transform.parent.GetComponent<Tile>();
    }
}
