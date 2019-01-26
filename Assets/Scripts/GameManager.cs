using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    private GameObject levelImage;

    void Awake() {
        
        InitGame();
    }

    void InitGame()
    {
        levelImage = GameObject.Find("LevelImage");
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        {
             Debug.Log("The left key was pressed");
        }
        else if(Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) {
            Debug.Log("The right key was pressed");
        }
    }

}
