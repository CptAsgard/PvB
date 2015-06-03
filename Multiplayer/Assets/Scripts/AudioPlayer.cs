using UnityEngine;
using System.Collections;

public class AudioPlayer : MonoBehaviour, MessageReceiver<PlaySound> {
    public AudioClip[] AudioClips;
    private AudioSource[] AudioSources;

    public void Awake()
    {
        DontDestroyOnLoad(gameObject);

        AudioSources = new AudioSource[AudioClips.Length];
        for (int i = 0; i < AudioClips.Length; i++)
        {
            AudioClip clip = AudioClips[i];
            GameObject go = new GameObject("Audio (" + clip.name + ")");
            AudioSource source = go.AddComponent<AudioSource>();
            source.clip = clip;
            source.volume = 0.5f;
            AudioSources[i] = source;
            DontDestroyOnLoad(go);
        }

        this.Subscribe<PlaySound>( Messenger.Bus );
    }

    public void HandleMessage(PlaySound msg)
    {
        AudioSources[msg.audioclip].Play();
    }
}
