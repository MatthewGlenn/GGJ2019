using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomCameraController : MonoBehaviour
{
    public GameObject camera;

    private Transform _roomTrans;

    public AudioClip roomAudio;

    // Start is called before the first frame update
    void Start()
    {
        _roomTrans = GetComponent<Transform>();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        camera.transform.position = new Vector3(_roomTrans.position.x, _roomTrans.position.y, -10.0f);
        if (roomAudio != null)
        {
            Debug.Log("Adding Room audio");
            AudioManager.instance.addRoomAudio(roomAudio);
        }
            
    }

    void OnTriggerExit2D( Collider2D col )
    {
        if (roomAudio != null)
        {
            Debug.Log("Removing Room audio");
            AudioManager.instance.removeRoomAudio();
        }
            
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
