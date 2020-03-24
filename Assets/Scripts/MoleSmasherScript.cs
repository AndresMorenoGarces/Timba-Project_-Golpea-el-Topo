using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoleSmasherScript : MonoBehaviour
{
    private int score;
    private GameManager gameManager;

    private void OnMouseDown()
    {
        Destroy(gameObject);
        gameManager.UpdateSmasherScore();
        gameManager.SaveSmasherScore();
    }

    private void Awake()
    {
        gameManager = GameObject.Find("GameManager_Container").GetComponent<GameManager>();
    }
}
