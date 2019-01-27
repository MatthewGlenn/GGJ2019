using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSenseManager : MonoBehaviour
{
    private Transform _itemTransform;

    private GameObject[] playerObjects;
    public GameObject audioObject;
    private AudioSource _GOAudioSource;
    // Start is called before the first frame update
    void Start()
    {
        _itemTransform = GetComponent<Transform>();
        _GOAudioSource = audioObject.GetComponent<AudioSource>();

        playerObjects = GameObject.FindGameObjectsWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (playerObjects.Length > 1) {
            playerObjects = GameObject.FindGameObjectsWithTag("Player");
        }
        

        foreach ( var playerObject in playerObjects) {
            float distance = Vector3.Distance(_itemTransform.position, playerObject.transform.position);
        
            if (distance < 5.0f) {
                float volume = (100 - (distance * 20.0f)) / 100;
                _GOAudioSource.volume = volume;
            }
        }
        
    }
}
