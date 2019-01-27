using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    //Text associated with item
    public string memory;

    private Transform _itemTransform;
    private SpriteRenderer _itemSpriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        _itemSpriteRenderer = GetComponent<SpriteRenderer>();
        _itemTransform = GetComponent<Transform>();

        _itemSpriteRenderer.sortingOrder = (int) ((_itemTransform.position.y + 3) * -100); 
    }

    // Update is called once per frame
    void Update()
    {

    }


}
