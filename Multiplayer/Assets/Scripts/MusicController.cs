using UnityEngine;
using System.Collections;

public class MusicController : MonoBehaviour, MessageReceiver<StartBattleMusic>{

    public AudioClip battleMusic;
    public AudioClip calmMusic;

    public float targetVolume = 0.2f;
		
    void Start() {
        this.Subscribe( Messenger.Bus );

        GetComponent<FadingAudioSource>().Fade( calmMusic, targetVolume, true );
    }
		
    void Update() {
			
    }

    public void HandleMessage( StartBattleMusic msg ) {
        GetComponent<FadingAudioSource>().Fade( battleMusic, targetVolume, true );
    }
}