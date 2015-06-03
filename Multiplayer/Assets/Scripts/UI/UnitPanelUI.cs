using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

/**
 * Shows the rank and amount left of units to spawn
 */
public class UnitPanelUI : MonoBehaviour
{
    public UnitType Type;

    private Text text;
    
    [SerializeField]
    private int __amountSpawnable;
    public int AmountSpawnable
    {
        get {
            return __amountSpawnable;
        }
        set {
            if( __amountSpawnable <= 0 && value > 0 )
                EnablePanel();

            __amountSpawnable = value;

            if( __amountSpawnable <= 0 )
                DisablePanel();
        }
    }

    void Start()
    {
        text = transform.parent.GetChild( 1 ).GetChild( 0 ).GetComponent<Text>();
    }

    void Update()
    {
        text.text = ( (int) AmountSpawnable ).ToString();
    }

    void DisablePanel()
    {
        GetComponent<Draggable>().enabled = false;
    }

    void EnablePanel()
    {
        gameObject.SetActive( true );
    }
}