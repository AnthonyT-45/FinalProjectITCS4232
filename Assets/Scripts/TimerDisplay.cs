using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class TimerDisplay : MonoBehaviour
{
    public TextMeshProUGUI timerText;

    // Start is called before the first frame update
    void Start()
    {
        TimerStartUp();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("TimerDisplay Update called");

        // Update the timer text.

        if (TimerManager.instance != null && TimerManager.instance.timerOn && SceneManager.GetActiveScene().name == "Inside")
        {
            Debug.Log("Timer current time: " + TimerManager.instance.FormatTimerText());

            timerText.text = TimerManager.instance.FormatTimerText();
        }
    }

    // Confirms if the Timer Manager has initialized the timer text.
    private void TimerStartUp()
    {
        GameObject timerGameObject = GameObject.FindWithTag("Timer");
        if (timerGameObject != null)
        {
            timerText = timerGameObject.GetComponent<TextMeshProUGUI>();
            if (timerText == null)
            {
                Debug.LogError("TextMeshProUGUI not found on object.");
            }
            else
            {
                Debug.Log("timerText assigned.");
            }
        }
        else
        {
            Debug.LogError("timer GameObject not found.");
        }
    }
}
