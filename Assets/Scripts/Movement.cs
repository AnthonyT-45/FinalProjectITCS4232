using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Movement : MonoBehaviour
{

    // Character movement speed

    public float speed = 5.0f;


    // Sprite animation

    public SpriteRenderer spriteRenderer;
    public Sprite[] leftSprites;
    public Sprite[] rightSprites;
    private int spriteIndex = 0;
    private float spriteChange = 0.1f;
    private float spriteTimer;
    public Sprite leftIdle;
    public Sprite rightIdle;
    private Sprite idleStatus;

    public static Movement instance = null;

    // Collision checks

    private bool door = false;
    public bool tree = false;
    private bool houseExit = false;
    private bool _key;
    private bool pond = false;
    private bool kitchen = false;
    private bool inside = false;
    private bool bookshelf = false;
    private bool bookshelfReward = false;
    private bool safekey1 = false;
    private bool safekey2 = false;
    private bool safekey3 = false;
    private bool safe = false;
    private bool fridge = false;
    private bool cone = false;
    private bool table = false;

    public bool key
    {
        get { return _key;  }
        set { _key = value; }
    }

    // Dialogue

    private Dialogue dialogue;
    private DialogueManager dialogueManager;
    private DialogueTrigger dialogueTrigger;
    private Dialogue enterHouse;

    // Money (player reward system)

    public static int money = 0;


    // Start is called before the first frame update
    void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;

        dialogueManager = FindObjectOfType<DialogueManager>();

        idleStatus = leftIdle;
        spriteRenderer.sprite = idleStatus;

    }

    // Update is called once per frame
    void Update()
    {
        if(dialogueManager != null && !dialogueManager.dialogueActive)
        {
            // Player movement

            Vector3 move = Vector3.zero;


            bool animation = false;

            if (Input.GetKey(KeyCode.W))
            {
                move.y += 1;
                
            }

            if (Input.GetKey(KeyCode.S))
            {
                move.y -= 1;
                
            }

            if (Input.GetKey(KeyCode.A))
            {
                move.x -= 1;
                AnimateSprite(leftSprites);
                animation = true;
                idleStatus = leftIdle;
                
            }

            if (Input.GetKey(KeyCode.D))
            {
                move.x += 1;
                AnimateSprite(rightSprites);
                animation = true;
                idleStatus = rightIdle;
            }
            
            if (!animation)
            {
                spriteRenderer.sprite = idleStatus;
                spriteIndex = 0;
                spriteTimer = 0;
            }

            if (animation)
            {
                //Debug.Log("Move Vector: " + move);
            }

            move.Normalize();

            GetComponent<Rigidbody2D>().velocity = move * speed;

        }
        else
        {
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }


        // Player interactions with E press.


        if (Input.GetKeyDown(KeyCode.E))
        {
            if (door && !dialogueManager.dialogueActive)
            {
                if (key)
                {
                    //Debug.Log("Trying to load scene Inside");
                    LoadScene("Inside", "Spawn");
                }
                else
                {
                    dialogueManager.StartDialogue(new Dialogue() { sentences = new string[] { "You need a key to unlock this door." } });
                }
            }
            else if (tree && !dialogueManager.dialogueActive)
            {
                if (!key)
                {
                    dialogueTrigger.TriggerDialogue();
                    key = true;
                }
            }
            else if (dialogueManager != null && dialogueManager.dialogueActive)
            {
                dialogueManager.DisplayNextSentence();
            }
            else if (pond && !dialogueManager.dialogueActive)
            {
                dialogueManager.StartDialogue(new Dialogue() { sentences = new string[] { "Unfortunately, you cannot swim." } });
            }
            else if (bookshelf && !dialogueManager.dialogueActive)
            {
                dialogueManager.StartDialogue(new Dialogue() { sentences = new string[] { "Just an ordinary bookshelf." } });
            }
            else if (bookshelfReward && !dialogueManager.dialogueActive)
            {
                money += 10;
                dialogueManager.StartDialogue(new Dialogue() { sentences = new string[] { "You found 10 dollars hidden in a book." } });
            }
            else if (safe && !dialogueManager.dialogueActive)
            {
                if (!safekey1 || !safekey2 || !safekey3)
                {
                    dialogueManager.StartDialogue(new Dialogue() { sentences = new string[] { "Locked safe." } });
                }
                else if(safekey1 && safekey2 && safekey3)
                {
                    money += 100;
                    dialogueManager.StartDialogue(new Dialogue() { sentences = new string[] { "Safe unlocked. Found 100 dollars." } });
                    safe = false;
                }
            }
            else if (fridge && !dialogueManager.dialogueActive)
            {
                dialogueManager.StartDialogue(new Dialogue() { sentences = new string[] { "You found a safe key." } });
                safekey1 = true;
            }
            else if (cone && !dialogueManager.dialogueActive)
            {
                dialogueManager.StartDialogue(new Dialogue() { sentences = new string[] { "You found a safe key." } });
                safekey2 = true;
            }
            else if (table && !dialogueManager.dialogueActive)
            {
                dialogueManager.StartDialogue(new Dialogue() { sentences = new string[] { "You found a safe key." } });
                safekey3 = true;
            }



        }
    }


    // Detects when player collides with objects.
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Door"))
        {
            //Debug.Log("Collided with Door");
            door = true;
        }
        else if (other.CompareTag("Tree"))
        {
            Debug.Log("Colliding with tree");
            tree = true;
            dialogueTrigger = other.GetComponent<DialogueTrigger>();
        }
        else if (other.CompareTag("Pond"))
        {
            Debug.Log("Collided with Pond");
            pond = true;
        }
        else if (other.CompareTag("Return"))
        {
            houseExit = true;
            Destroy(gameObject);
            SceneManager.LoadScene("GameWin");

        }
        else if (other.CompareTag("Kitchen"))
        {
            kitchen = true;
            SceneManager.LoadScene("Kitchen");
            LoadScene("Kitchen", "KitchenSpawn");

        }
        else if (other.CompareTag("Inside"))
        {
            inside = true;
            LoadScene("Inside", "InsideSpawn");

        }
        else if (other.CompareTag("Bookshelf"))
        {
            bookshelf = true;
        }
        else if (other.CompareTag("BookshelfReward"))
        {
            bookshelfReward = true;
        }
        else if (other.CompareTag("Safe"))
        {
            safe = true;
        }
        else if (other.CompareTag("Fridge"))
        {
            fridge = true;
        }
        else if (other.CompareTag("Cone"))
        {
            cone = true;
        }
        else if (other.CompareTag("Table"))
        {
            table = true;
        }

    }

    // Detects when player is no longer colliding with an object.
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Door"))
        {
            door = false;
        }
        else if (other.CompareTag("Tree"))
        {
            tree = false;
            dialogueTrigger = null;
        }
        else if (other.CompareTag("Pond"))
        {
            pond = false;
        }
        else if (other.CompareTag("Return"))
        {
            houseExit = false;
        }
        else if (other.CompareTag("Kitchen"))
        {
            kitchen = false;
        }
        else if (other.CompareTag("Inside"))
        {
            inside = false;
        }
        else if (other.CompareTag("Bookshelf"))
        {
            bookshelf = false;

        }
        else if (other.CompareTag("BookshelfReward"))
        {
            bookshelfReward = false;
        }
        else if (other.CompareTag("Safe"))
        {
            safe = false;
        }
        else if (other.CompareTag("Fridge"))
        {
            fridge = false;
        }
        else if (other.CompareTag("Cone"))
        {
            cone = false;
        }
        else if (other.CompareTag("Table"))
        {
            table = false;
        }

    }

    // Method for calling a scene load so that the player can spawn in a certain area in the transitioning scene.

    public void LoadScene(string sceneName, string spawnName)
    {
        SceneManager.LoadScene(sceneName);
        PlayerPrefs.SetString("Spawn Name", spawnName);

    }


    // Brings player to a certain location in a scene once a scene is loaded.
    void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
    {
        if (instance != this)
        {
            return;

        }

        dialogueManager = FindObjectOfType<DialogueManager>();

        string spawnInfo = PlayerPrefs.GetString("Spawn Name", "Spawn Location");
        GameObject spawn = GameObject.FindWithTag(spawnInfo);
        if (spawn != null)
        {
            transform.position = spawn.transform.position;  
        }

        PlayerPrefs.DeleteKey("Spawn Name");
    }

    // Sprite animation for walking left and right.

    void AnimateSprite(Sprite[] spriteWalk)
    {
        spriteTimer += Time.deltaTime;
        if (spriteTimer >= spriteChange)
        {
            spriteIndex++;
            
            if (spriteIndex >= spriteWalk.Length)
            {
                spriteIndex = 0;
            }
            spriteRenderer.sprite = spriteWalk[spriteIndex];
            spriteTimer = 0;
        }
    }

    // Makes sure that only one instance of this player exists at all times to avoid duplicate players when loading back into main scene.
    void Awake()
    {
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

    
}
