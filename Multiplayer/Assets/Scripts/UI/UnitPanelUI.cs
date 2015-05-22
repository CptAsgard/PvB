using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class UnitPanelUI : MonoBehaviour
{
    public UnitType Type;
    
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

    void DisablePanel()
    {
        gameObject.SetActive( false );
    }

    void EnablePanel()
    {
        gameObject.SetActive( true );
    }
}