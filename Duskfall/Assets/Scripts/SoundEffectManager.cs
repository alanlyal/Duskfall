using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffectManager : MonoBehaviour
{
    private static SoundEffectManager instance;

    private AudioSource audioSource;
    private SoundEffectLibrary soundEffectLibrary;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            audioSource = GetComponent<AudioSource>();
            soundEffectLibrary = GetComponent<SoundEffectLibrary>();
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // to play a sound call "SoundEffectManager.Play("soundname");" on the given object
    public static void Play(string soundName)
    {
        if (instance == null)
        {
            Debug.LogWarning("SoundEffectManager not found in the scene!");
            return;
        }

        AudioClip audioClip = instance.soundEffectLibrary.GetRandomClip(soundName);
        if (audioClip != null)
        {
            instance.audioSource.PlayOneShot(audioClip);
        }
    }
}
