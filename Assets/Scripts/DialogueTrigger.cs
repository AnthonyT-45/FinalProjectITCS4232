using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;
    public Movement movement;

    private void Start()
    {
        movement = FindObjectOfType<Movement>();
    }
    
    // Method to trigger dialogue. May remove later.

    public void TriggerDialogue ()
    {
        DialogueManager dialogueManager = FindObjectOfType<DialogueManager>();
        dialogueManager.StartDialogue(dialogue);
        movement.key = true;
    }
}
