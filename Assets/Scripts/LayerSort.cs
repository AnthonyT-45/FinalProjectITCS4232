using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LayerSort : MonoBehaviour
{

    private SpriteRenderer sprite;
    public Movement movement;

    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        // Player will go above a sprite.

        if(movement != null && movement.transform.position.y > transform.position.y - 1)
        {
            sprite.sortingOrder = 2;
        }
        else
        {
            // Player will go behind a sprite.
            sprite.sortingOrder = 0;
        }
    }
}
