using UnityEngine;
using System.Collections;

/**
 * Generic class for animation events
 */
public class AnimEvents : MonoBehaviour
{
    /**
     * Fade in the game tiles on the bridge
     */
    public void StartTileFadeIn()
    {
        Messenger.Bus.Route( new FadeInTiles() );
    }
}