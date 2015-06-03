using UnityEngine;
using System.Collections;

public class MusicController : MonoBehaviour, MessageReceiver<StartBattleMusic>{

    public AudioClip battleMusic;
    public AudioClip calmMusic;
		
    void Start() {
        this.Subscribe<StartBattleMusic>( Messenger.Bus );

        GetComponent<FadingAudioSource>().Fade( calmMusic, 0.5f, true );
    }
		
    void Update() {
			
    }

    public void HandleMessage( StartBattleMusic msg ) {
        GetComponent<FadingAudioSource>().Fade( battleMusic, 0.5f, true );
    }
}