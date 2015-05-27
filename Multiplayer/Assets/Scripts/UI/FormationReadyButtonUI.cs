using UnityEngine;
using System.Collections;

public class FormationReadyButtonUI : MonoBehaviour, MessageReceiver<ClientFormationSetupReady>, MessageReceiver<ClientFormationSetupUnready>
{
    void Awake()
    {
        this.Subscribe<ClientFormationSetupReady>( Messenger.Bus );
        this.Subscribe<ClientFormationSetupUnready>( Messenger.Bus );
    }

    public void HandleMessage( ClientFormationSetupReady msg )
    {
        iTween.MoveTo( gameObject, gameObject.transform.position - new Vector3( 0, 100, 0 ), 2 );
    }

    public void HandleMessage( ClientFormationSetupUnready msg )
    {
        iTween.MoveTo( gameObject, gameObject.transform.position + new Vector3( 0, 100, 0 ), 2 );
    }

    public void OnButtonClick()
    {
        string formation = GameMap.SINGLETON.EncodeFormation();
        Debug.Log(formation);
        GameMap.SINGLETON.DecodeFormation(formation);
    }
}
