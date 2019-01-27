using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject levelImage;
    public GameObject maleChar;
    public GameObject femaleChar;

    void Awake() {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        {
            Destroy(levelImage);
             Debug.Log("The left key was pressed");
        }
        else if(Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) {
            Destroy(levelImage);
            Debug.Log("The right key was pressed");
        }
    }

}
