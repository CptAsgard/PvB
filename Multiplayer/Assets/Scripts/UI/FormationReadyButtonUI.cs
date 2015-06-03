using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/**
 * Button to press if the formation is ready and the player wants to start
 */
public class FormationReadyButtonUI : MonoBehaviour, MessageReceiver<ClientFormationSetupReady>
{
    private bool hasBeenClicked;

    void Awake()
    {
        this.Subscribe<ClientFormationSetupReady>( Messenger.Bus );
    }

    public void HandleMessage( ClientFormationSetupReady msg )
    {
        Messenger.Bus.Route<PlaySound>(new PlaySound()
        {
            audioclip = 2
        });
        iTween.MoveTo( gameObject, iTween.Hash( "time", 2, "y", 168, "islocal", true ) );
    }

    public void OnButtonClick()
    {
        if( hasBeenClicked )
            return;

        hasBeenClicked = true;
        GetComponent<Button>().interactable = false;

        string formation = GameMap.SINGLETON.EncodeFormation();
        RPCManager.SINGLETON.SendFormation( formation );

        StartCoroutine( waitForGameStart() );
    }

    IEnumerator waitForGameStart()
    {
        bool breakWhile = false;

        while( !breakWhile )
        {
            bool emptyFound = false;
            foreach( Tile t in GameMap.SINGLETON.Tiles )
            {
                if( t.Contains == null )
                    emptyFound = true;
            }

            if( emptyFound )
                yield return new WaitForSeconds( 1.0f );
            else
                breakWhile = true;
        }

        GameState.StartGame();

        iTween.MoveTo( gameObject, gameObject.transform.position + new Vector3( 0, 500, 0 ), 2 );
    }
}
