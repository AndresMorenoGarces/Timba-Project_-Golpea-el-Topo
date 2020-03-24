using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoleSmasherScript : MonoBehaviour
{
    private GameManager gameManager;

    private void OnMouseDown() // Funcion que destruye el topo, guarda y carga variables mediante funciones 
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
