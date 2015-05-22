using UnityEngine;
using System.Collections;

public class UnitSelectorSelection : MonoBehaviour
{
    public Unit SelectedUnit;
    public Tile SelectedTile;

    public static UnitSelectorSelection SINGLETON;

    void Awake()
    {
        SINGLETON = this;
    }
}
