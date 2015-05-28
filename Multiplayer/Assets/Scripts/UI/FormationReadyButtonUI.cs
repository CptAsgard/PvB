using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FormationReadyButtonUI : MonoBehaviour, MessageReceiver<ClientFormationSetupReady>, MessageReceiver<ClientFormationSetupUnready>
{
    private bool hasBeenClicked;

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
        if( hasBeenClicked )
            return;

        hasBeenClicked = true;
        GetComponent<Button>().interactable = false;

        string formation = GameMap.SINGLETON.EncodeFormation();
        RPCManager.SINGLETON.SendFormation(formation);

        GameState.StartGame();
    }
}
