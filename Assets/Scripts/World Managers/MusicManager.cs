using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioSource sfxAudioSource;
    public AudioClip[] songs;
    public OptionsManager options;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        PlaySong();
    }

    public void PlaySong(int scene = 0)
    {
        audioSource.clip = songs[scene];
        audioSource.Play();
        Debug.Log(audioSource.clip);
    }

    public void SetVolume(float volume)
    {
        audioSource.volume = volume;
    }



    // Update is called once per frame
    void Update()
    {
        options = FindObjectOfType<OptionsManager>();

        if (options != null)
        {
            audioSource.volume = options.GetMusicVol();
            sfxAudioSource.volume = options.GetSFXVol();
        }

    }
}
