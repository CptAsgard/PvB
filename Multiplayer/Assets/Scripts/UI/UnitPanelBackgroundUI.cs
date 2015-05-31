using UnityEngine;
using System.Collections;

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