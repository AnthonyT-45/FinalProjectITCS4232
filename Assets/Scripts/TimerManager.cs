using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using UnityEngine.SceneManagement;


public class TimerManager : MonoBehaviour
{

    public static TimerManager instance { get; private set; }

    public float timer { get; private set; }
    public bool timerOn { get; private set; }

    private Movement movement;


    // Start is called before the first frame update
    void Start()
    {
        timer = 180f;
        timerOn = true;
        
    }

    // Update is called once per frame

    // Timer logic.
    void Update()
    {
        if (timerOn)
        {
            timer -= Time.deltaTime;

            if (timer <= 0)
            {
                timerOn = false;
                Destroy(movement.gameObject);
                movement.LoadScene("GameLoss", "GameLossScreen");
            }
        }
    }

    // Manages the instances of Timer Manager. Avoids duplicate Timer Manager.
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            movement = FindObjectOfType<Movement>();
            Debug.Log("TimerManager instance created.");
        }
        else if (instance != this)
        {
            Debug.Log("TimerManager instance already exists, destroying duplicate.");
            Destroy(gameObject);
        }
    }

    // Formats text into an actual clock-like timer.
    public string FormatTimerText()
    {
        int min = Mathf.FloorToInt(timer / 60);
        int sec = Mathf.FloorToInt(timer % 60);

        return string.Format("{0:00}:{1:00}", min, sec);
    }

    // Turns timer on when player loads inside the house.

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Inside")
        {
            timerOn = true;
        }
    }


}
