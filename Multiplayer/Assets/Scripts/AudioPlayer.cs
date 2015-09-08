using UnityEngine;
using System.Collections;

public class AudioPlayer : MonoBehaviour, MessageReceiver<PlaySound>
{
    public AudioClip[] AudioClips;

    // Volume for when the clips spawn initially
    public float targetVolume = 0.2f;

    private AudioSource[] AudioSources;

    public void Awake()
    {
        DontDestroyOnLoad(gameObject);

        this.Subscribe(Messenger.Bus);

        GenerateAudioClips();
    }

    public void HandleMessage(PlaySound msg)
    {
        AudioSources[msg.audioclip].Play();
    }

    void GenerateAudioClips()
    {
        AudioSources = new AudioSource[AudioClips.Length];
        for (int i = 0; i < AudioClips.Length; i++)
        {
            AudioClip clip = AudioClips[i];
            GameObject go = new GameObject("Audio (" + clip.name + ")");
            AudioSource source = go.AddComponent<AudioSource>();

            source.clip = clip;
            source.volume = targetVolume;

            AudioSources[i] = source;
            DontDestroyOnLoad(go);
        }
    }
}
