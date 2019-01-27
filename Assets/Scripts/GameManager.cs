using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject levelImage;
    public GameObject maleChar;
    public GameObject femaleChar;
    public GameObject fadeToBlack;
    public GameObject[] sortingOrders;

    private Animator _fadeToBlackAnimator;

    public GameObject player;
    public RuntimeAnimatorController fAnimationController;
    public Sprite fSprite;
    private Animator _playerAnimatorController;
    private SpriteRenderer _playerSpriteRenderer;


    private bool hasPlayerChoosen = false;

    void Awake() {
        _fadeToBlackAnimator = fadeToBlack.GetComponent<Animator>();

        sortingOrders = GameObject.FindGameObjectsWithTag("ZSort");

        foreach (GameObject sortingOrder in sortingOrders)
        {
            sortingOrder.GetComponent<SpriteRenderer>().sortingOrder = (int) (sortingOrder.transform.position.y * -100);
        }
    }

    private void Start() {
        _playerAnimatorController = player.GetComponent<Animator>();
            _playerSpriteRenderer = player.GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (!hasPlayerChoosen) {
            if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
            {
                Destroy(levelImage);
                Debug.Log("The left key was pressed");
                player.GetComponent<Player>().isFemale = false;
                hasPlayerChoosen = true;
            }
            else if(Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) {
                Destroy(levelImage);
                player.GetComponent<Player>().isFemale = true;
                _playerAnimatorController.runtimeAnimatorController = fAnimationController;
                _playerSpriteRenderer.sprite = fSprite;
                Debug.Log("The right key was pressed");
                hasPlayerChoosen = true;
            }
        }
        
    }

    public void FadeToBlack() {

        _fadeToBlackAnimator.SetTrigger("EndGame");

    }

}
