using UnityEngine;
using System.Collections;

/**
 * Handles the animation for the bridge falling down
 */
public class BridgeAnimController : MonoBehaviour, MessageReceiver<StartGame>
{
    void Start()
    {
        this.Subscribe<StartGame>( Messenger.Bus );
    }

    void Update()
    {

    }

    public void HandleMessage( StartGame msg )
    {
        GetComponent<Animator>().Play( "BridgeDown" );
    }
}