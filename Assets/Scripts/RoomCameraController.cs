using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomCameraController : MonoBehaviour
{
    public GameObject camera;

    private Transform _roomTrans;

    // Start is called before the first frame update
    void Start()
    {
        _roomTrans = GetComponent<Transform>();
    }

    void OnTriggerEnter2D(Collider2D col) {
        camera.transform.position = new Vector3(_roomTrans.position.x, _roomTrans.position.y, -10.0f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
