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

    [SerializeField]
    private UnitPanelUI __s;
    public UnitPanelUI SelectedPanel {
        get {
            return __s;
        } set {
            OnPanelDeselected( __s );
            __s = value;
            OnPanelSelected( __s );
        }
    }

    void Update()
    {
        if( Input.GetMouseButtonUp( 0 ) )
        {
            if( SelectedPanel != null && UnitSelectorSelection.SINGLETON.SelectedTile != null )
                TrySpawnUnit( SelectedPanel.Type );
        }
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
        if( UnitSelectorSelection.SINGLETON.SelectedTile == null ) return;
        if( SelectedPanel.AmountSpawnable <= 0 ) return;

        // TODO if( client's side != side clicked ) return

        GameObject spawner = GameObject.Instantiate( Resources.Load( "UnitSpawner" ) as GameObject );
        spawner.GetComponent<UnitSpawner>().Init( UnitSelectorSelection.SINGLETON.SelectedTile, type );

        SelectedPanel.AmountSpawnable--;
        SelectedPanel = null;

        UnitSelectorSelection.SINGLETON.SelectTile( null );
    }
}