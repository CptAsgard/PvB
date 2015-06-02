using UnityEngine;
using System.Collections;

/**
 * Helper class to move away the unit panel when game starts
 */
public class UnitPanelBackgroundUI : MonoBehaviour, MessageReceiver<StartGame>
{
    void Start()
    {
        this.Subscribe<StartGame>( Messenger.Bus );
    }

    public void HandleMessage( StartGame msg )
    {
        iTween.MoveTo( gameObject, gameObject.transform.position - new Vector3( 0, 100, 0 ), 2 );
    }
}