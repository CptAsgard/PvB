using UnityEngine;
using System.Collections;

/**
 * Moves the camera from the setup to the playmode position when the game starts
 */
public class CameraController : MonoBehaviour, MessageReceiver<StartGame>
{
    void Start()
    {
        this.Subscribe<StartGame>( Messenger.Bus );
    }

    public void HandleMessage( StartGame msg )
    {
        iTween.MoveTo( gameObject, iTween.Hash( "time", 3, "position", new Vector3( -0.365f, 5.6f, 1.5f ), "easeType", iTween.EaseType.easeInOutSine ) );
    }
}