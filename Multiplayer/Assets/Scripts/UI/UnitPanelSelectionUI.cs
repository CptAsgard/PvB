using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UnitPanelSelectionUI : MonoBehaviour
{
    public static UnitPanelSelectionUI SINGLETON;

    void Awake()
    {
        SINGLETON = this;
    }

    private void OnPanelSelected( UnitPanelUI panel )
    {
        if( panel == null ) return;

        panel.GetComponent<Image>().color = Color.red;
    }

    private void OnPanelDeselected( UnitPanelUI panel )
    {
        if( panel == null ) return;

        panel.GetComponent<Image>().color = Color.white;

        if( panel.AmountSpawnable <= 0 )
            panel.gameObject.SetActive( false );
    }

    public void TrySpawnUnit( UnitType type )
    {
        //if( UnitSelectorSelection.SINGLETON.SelectedTile == null ) 
        //    return;

        //if( SelectedPanel.AmountSpawnable <= 0 ) 
        //    return;

        //if( UnitSelectorSelection.SINGLETON.SelectedTile.UnitOnTile != null )
        //    return;

        //GameObject spawner = GameObject.Instantiate( Resources.Load( "UnitSpawner" ) as GameObject );
        //spawner.GetComponent<UnitSpawner>().Init( UnitSelectorSelection.SINGLETON.SelectedTile, type );

        //SelectedPanel.AmountSpawnable--;
        //SelectedPanel = null;
    }
}