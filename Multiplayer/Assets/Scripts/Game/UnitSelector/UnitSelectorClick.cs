using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class UnitSelectorClick : MonoBehaviour
{
    public void OnPointerClick( BaseEventData eventData )
    {
        Debug.Log( "click" );
    }
}
