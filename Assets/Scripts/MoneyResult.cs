using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MoneyResult : MonoBehaviour
{
    public TextMeshProUGUI money;

    // Start is called before the first frame update

    // Edits the game winning text to show how much money the player earned.
    void Start()
    {
        money.text = "Total Money Earned: " + Movement.money.ToString();
    }

}
