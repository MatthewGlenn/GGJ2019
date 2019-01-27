using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public float speed;             //Floating point variable to store the player's movement speed.

    private Rigidbody2D rb2D;       //Store a reference to the Rigidbody2D component required to use 2D Physics.

    private Animator animator;      //Stores the animation controller for the player
    private BoxCollider2D boxCollider;  //Stores BoxCollider for player
    private SpriteRenderer spriteRenderer; //Stores sprite renderer for player. Referenced to send triggers and play animations

    private Transform _playerTransform;

    //Holds reference to Item held in player's hand
    private Item heldItem;
    //Holds reference to "Memory" associated with the item the player last picked up
    private String memory;
    public Text printMemory;
    private int txtAdv;

    private bool holdingItem;

    //True if the player is currently overlapping an interactable object
    private bool isTouching;
    //Holds reference to object currently touching player
    private Collider2D touchingObject;

    //True if the player is currently moving
    private bool isWalking;
    //True if the character sprite is facing left
    private bool facingLeft;

    public AudioClip footsteps1;
    public AudioClip footsteps2;
    public AudioClip footsteps3;
    public AudioClip footsteps4;
    public AudioClip footsteps5;
    public AudioClip pickup;
    public AudioClip putdown;

    public GameObject _textBackground;
    private Image _textBackgroundImage;



    // Use this for initialization
    void Start()
    {
        //Get and store a references to components
        rb2D = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        _playerTransform = GetComponent<Transform>();

        //Initialize simple variables
        isTouching = false;
        touchingObject = null;
        isWalking = false;
        facingLeft = true;
        holdingItem = false;
        txtAdv = 0;

        _textBackgroundImage = _textBackground.GetComponent<Image>();
    }


    /** Built-in function automatically called when 
     *  Collidable object "Other" touches player
     * 
     **/
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Item" || other.tag == "Interactable" || other.tag == "Trash" )
        {
            isTouching = true;
            touchingObject = other;
        }
    }


    /** Built-in function automatically called when 
     *  Collidable object "Other" stops touching player
     * 
     **/
    private void OnTriggerExit2D( Collider2D other )
    {
        isTouching = false;
        touchingObject = null;
    }

    /** Private function called whenever player hits space bar (interact button)
     *  If the player is touching an interactable object, like a decoration or a break up item,
     *  the object will be handled accordingly
     * 
     **/

    private void Interact()
    {
        Input.ResetInputAxes();
        //Play animation for interating with object
        animator.PlayInFixedTime("PlayerInteract",  0, float.NegativeInfinity);

        //If no interactable object present, return
        if (!isTouching || touchingObject == null)
        {
            if ( holdingItem )
                animator.SetTrigger("StandStillWithItem");
            else
                animator.SetTrigger("StandStill");
            return;
        }

        //If the player is holding an item they can throw away, dispose of the item
        if ( touchingObject.tag == "Trash" && holdingItem )
        {
            AudioManager.instance.PlaySingle(putdown);
            Debug.Log("GET DUNKED ON");
            heldItem = null;
            holdingItem = false;
            txtAdv = 0;
            printMemory.text = "";
        }
        //If the object is an Item, add it to player's "heldItem"
        //and remove it from play
        else if( touchingObject.tag == "Item" )
        {
            AudioManager.instance.PlaySingle(pickup);
            animator.SetTrigger("StandStillWithItem");
            
            heldItem = touchingObject.gameObject.GetComponent<Item>();

            memory = heldItem.memory;

            Destroy(touchingObject.gameObject);
            heldItem = null;
            holdingItem = true;
           // touchingObject.gameObject.SetActive(false);
           // touchingObject = null;
        }
        //If the object is an Interactable decoration, make it do a special animation
        else if(touchingObject.tag == "Interactable")
        {
            animator.SetTrigger("StandStill");

        }

        

    }

    void Update() {
        // Z-index fix
        spriteRenderer.sortingOrder = (int) (_playerTransform.position.y * -100);   
    }

    //FixedUpdate is called at a fixed interval and is independent of frame rate. Put physics code here.
    void FixedUpdate()
    {
        if( isWalking && !AudioManager.instance.efxSource.isPlaying  )
        {
            AudioManager.instance.RandomizeSfx(footsteps1, footsteps2, footsteps3, footsteps4, footsteps5);
        }

        if (holdingItem && (txtAdv <= memory.Length * 2))
        {
          //  var tempColor = _textBackgroundImage.color;
         //   tempColor.a = 0.75f;
         //   _textBackgroundImage.color = tempColor;
            txtAdv++;
            printMemory.text = memory.Substring(0, (txtAdv / 2));
        }
      /*  else
        {
            var tempColor = _textBackgroundImage.color;
            tempColor.a = 0.0f;
            _textBackgroundImage.color = tempColor;
        }*/

        //check if player has hit Space Bar
        if (Input.GetKeyDown("space"))
        {
            //If true, call the interact method
            Interact();
        }


        //Movement update***********************************

        //Store the current horizontal input in the float moveHorizontal.
        float moveHorizontal = Input.GetAxis("Horizontal");

        //Store the current vertical input in the float moveVertical.
        float moveVertical = Input.GetAxis("Vertical");


        //Check if player needs to flip direction to face walking direction
        if ((moveHorizontal > 0 && facingLeft) || (moveHorizontal < 0 && !facingLeft))
        {
            spriteRenderer.flipX = !spriteRenderer.flipX;
            facingLeft = !facingLeft;
        }


        //Check if player is currently moving, and if not, trigger a walking animation.
        //This helps reduce the amount of signals sent to the Animator Controller
        if ( !isWalking && ( moveHorizontal != 0 || moveVertical != 0 ) )
        {
            isWalking = true;

            if ( !holdingItem )
            {
                animator.SetTrigger("Walk");
            }
            else
            {
                animator.SetTrigger("WalkWithItem");
            }
        }
        //Else if already moving but input = 0, stop moving
        else if( isWalking && (moveHorizontal == 0 && moveVertical == 0) )
        {
            isWalking = false;
            if (!holdingItem )
                animator.SetTrigger("StandStill");
            else
                animator.SetTrigger("StandStillWithItem");

        }


        //Use the two store floats to create a new Vector2 variable movement.
        Vector2 movement = new Vector2(moveHorizontal, moveVertical);

        //Move the rigid body accordingly based on movement and speed
        rb2D.MovePosition(rb2D.position + (movement*speed*Time.deltaTime));
    }
}
