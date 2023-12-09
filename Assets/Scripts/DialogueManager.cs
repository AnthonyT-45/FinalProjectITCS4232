using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class DialogueManager : MonoBehaviour
{

    // Some of the Dialogue implementation was aided with this tutorial.
    // This also involves some of DialogueTrigger and Dialogue.
    // https://www.youtube.com/watch?v=_nRzoTzeyxU

    public TextMeshProUGUI dialogue;
    public bool dialogueActive { get; private set; }
    private Queue<string> sentences;

    public GameObject canvas;

    public static DialogueManager instance;

    void Start()
    {
        sentences = new Queue<string>();

        // Deactiviates canvas at start so that dialogue boxes don't appear when loading scenes.

        if (canvas)
        {
            canvas.SetActive(false);
        }
    }

    // Method used to start dialogue when the player interacts with objects that have dialogue.
    public void StartDialogue(Dialogue dialogueText)
    {
        //Debug.Log("Starting conversation...");

        if(!dialogue || !canvas)
        {
            dialogue = FindObjectOfType<TextMeshProUGUI>();
            canvas = GameObject.FindWithTag("DialogueCanvas");
        }

        canvas.SetActive(true);

        dialogueActive = true;

        sentences.Clear();

        foreach (string sentence in dialogueText.sentences)
        {
            sentences.Enqueue(sentence);
        }
        
        DisplayNextSentence();

    }

    // Displays next sentence in the queue.
    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
        }

        string sentence = sentences.Dequeue();
        dialogue.text = sentence;

        Debug.Log(dialogue.text);
    }

    void EndDialogue()
    {
        Debug.Log("End of conversation.");
        canvas.SetActive(false);
        dialogueActive = false;
    }

    // Ensures that there is only one Dialogue Manager at all times when loading scenes.
    void Awake()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;

        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    // Confirms that dialogue managers are setup correctly in loaded scenes.

    void OnSceneLoaded(UnityEngine.SceneManagement.Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Inside" || scene.name == "MainScene" || scene.name == "Kitchen")
        {

            dialogue = GameObject.FindWithTag("Dialogue")?.GetComponent<TextMeshProUGUI>();
            canvas = GameObject.FindWithTag("DialogueCanvas");

            if (dialogue == null || canvas == null)
            {
                Debug.LogError("Failed to find dialogue parameters when the scene was loaded.");
            }
            else
            {
                canvas.SetActive(false);
            }
        }
    }
}
