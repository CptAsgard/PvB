using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/**
 * Simple handler for the ending screen
 */
public class EndScreenUI : MonoBehaviour, MessageReceiver<EndGame> {

    Image targetImg;

    void Awake()
    {
        this.Subscribe<EndGame>(Messenger.Bus);

    }

    public void HandleMessage(EndGame msg)
    {
        Transform target;
        if( msg.winner == Side.BLUE ) {
            target = transform.GetChild( 1 );
            target.gameObject.SetActive( true );
        } else {
            target = transform.GetChild( 0 );
            target.gameObject.SetActive( true );
        }

        targetImg = target.GetComponent<Image>();

        targetImg.canvasRenderer.SetAlpha( 0.0f );
        targetImg = target.gameObject.GetComponent<Image>();
        targetImg.CrossFadeAlpha( 1.0f, 0.5f, false );
    }
}
