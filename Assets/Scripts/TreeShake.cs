using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class TreeShake : MonoBehaviour
{
    public float speed = 1.0f;
    public float distance = 2.0f;

    private Movement movement;
    private DialogueManager dialogueManager;

    private Vector3 startPos;

    private Coroutine shakeCoroutine;

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
        movement = FindObjectOfType<Movement>();
        dialogueManager = FindObjectOfType<DialogueManager>();
    }

    // Update is called once per frame

    // Checks if the tree needs to shake or not.
    void Update()
    {
       if(movement.tree && dialogueManager.dialogueActive)
        {
            if(shakeCoroutine == null)
            {
                shakeCoroutine = StartCoroutine(MoveTree());
            }
        }
       else
        {
            if(shakeCoroutine != null)
            {
                StopCoroutine(shakeCoroutine);
                shakeCoroutine = null;
                transform.position = startPos;
            }
        }
    }

    // Formula for tree movement.
    private IEnumerator MoveTree()
    {

        while(true)
        {
            float positionX = startPos.x + Mathf.Sin(Time.time * speed) * distance;
            transform.position = new Vector3(positionX, transform.position.y, transform.position.z);

            yield return null;
        }
    }
}
