using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TileFader : MonoBehaviour, MessageReceiver<FadeInTiles>
{
    public bool FadeOutInstead;

    void Start() {
        this.Subscribe<FadeInTiles>( Messenger.Bus );
    }

    public void HandleMessage( FadeInTiles msg ) {
        if( FadeOutInstead )
            FadeOut();
        else
            FadeIn();
    }

    public void FadeIn() {
        iTween.ColorTo( gameObject, iTween.Hash( "a", 1.0f, "easetype", iTween.EaseType.easeInOutSine, "delay", 4.0f ) );
    }

    public void FadeOut() {
        iTween.ColorTo( gameObject, iTween.Hash( "a", 0.0f, "easetype", iTween.EaseType.easeInOutSine, "delay", 4.0f ) );
    }
}