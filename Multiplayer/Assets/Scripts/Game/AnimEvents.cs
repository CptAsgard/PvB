using UnityEngine;
using System.Collections;

public class AnimEvents : MonoBehaviour {

    public void StartTileFadeIn() {
        Messenger.Bus.Route( new FadeInTiles() );
    }
}