using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSenseManager : MonoBehaviour
{
    private Transform _itemTransform;

    public GameObject playerObject;
    public GameObject audioObject;
    private AudioSource _GOAudioSource;
    // Start is called before the first frame update
    void Start()
    {
        _itemTransform = GetComponent<Transform>();
        _GOAudioSource = audioObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(_itemTransform.position, playerObject.transform.position);
        
        if (distance < 2.5f) {
            float volume = (100 - (distance * 40.0f)) / 100;
            _GOAudioSource.volume = volume;
        }
    }
}
