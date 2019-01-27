using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource efxSource;
    public AudioSource BackgroundMusic;
    public AudioSource roomAudioSrc;
    private AudioClip roomAudio;

    public static AudioManager instance = null;

    public float lowPitchRange = 0.95f;
    public float highPitchRange = 1.05f;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);

    }

    public void PlaySingle(AudioClip clip)
    {
        efxSource.clip = clip;
        efxSource.Play();
    }

    public void RandomizeSfx(params AudioClip[] clips)
    {
        int randomIndex = Random.Range(0, clips.Length);
        float randomPitch = Random.Range(lowPitchRange, highPitchRange);
        efxSource.clip = clips[randomIndex];
        efxSource.pitch = randomPitch;
        efxSource.Play();
    }

    public void addRoomAudio( AudioClip clip )
    {
        roomAudioSrc.clip = clip;
        roomAudioSrc.Play();
    }

    public void removeRoomAudio()
    {
        roomAudioSrc.clip = null;
        roomAudioSrc.Stop();
    }

    public void Update()
    {
        
    }

    public void StopAllMusic(){
        // if( GameObject.FindGameObjectsWithTag("Record").Length == 0 || GameObject.FindGameObjectsWithTag("Item").Length < 9 )
        // {
        //     Debug.Log("Stop all the music");
        //     //On Game End Pause Music
        //     roomAudioSrc.Stop();
        //     efxSource.Stop();
        //     BackgroundMusic.Stop();

        //     //Play Cinder
        // }

        Debug.Log("Stop all the music");
            //On Game End Pause Music
            roomAudioSrc.Stop();
            efxSource.Stop();
            BackgroundMusic.Stop();
    }

}
