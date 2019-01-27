using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource efxSource;
    public static AudioManager instance = null;

    public float lowPitchRange = 0.95f;
    public float highPitchRange = 1.05f;

    private float sfxVolume = 0.02f;
    // Start is called before the first frame update
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
        efxSource.volume = sfxVolume;
        efxSource.Play();
    }

}
