﻿using UnityEngine;
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

    public void SelectTile( Tile tile )
    {
        // Trigger deselect event
        if( SelectedTile != null )
            SelectedTile.OnTileDeselected();

        // Select new if not same tile
        if( SelectedTile != tile )
            SelectedTile = tile;
        else
            SelectedTile = null;

        // Trigger select event
        if( SelectedTile != null )
            SelectedTile.OnTileSelected();
    }
}