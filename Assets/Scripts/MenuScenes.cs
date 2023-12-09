using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScenes : MonoBehaviour
{
    // Method for user interface buttons to call to load different scenes.
    public void LoadMenuScenes(string name)
    {
        SceneManager.LoadScene(name);
    }

    /*public void DebugButton()
    {
        Debug.Log("Button pressed.");
    }*/
}
