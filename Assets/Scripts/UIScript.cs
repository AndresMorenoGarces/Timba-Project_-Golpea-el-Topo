using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIScript : MonoBehaviour
{
    public TextMeshProUGUI playerTurnText;

    public void UpdatePlayerTurnText(bool isPlayerOneTurn) 
    {
        if (isPlayerOneTurn)
            playerTurnText.text = "Player One Turn";
        else
            playerTurnText.text = "Player Two Turn";
    }
}
