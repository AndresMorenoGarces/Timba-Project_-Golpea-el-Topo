using UnityEngine;

public class MoleSmasherScript : MonoBehaviour
{
    private GameManager gameManager;
    private AudioManager audioManager;

    private void OnMouseDown() // Funcion que destruye el topo, guarda y carga variables mediante funciones 
    {
        Destroy(gameObject);
        gameManager.ApplyXSprite(this.gameObject);
        gameManager.UpdateSmasherScore();
        gameManager.SaveSmasherScore();
        audioManager.PlayHit();
    }
    private void Awake()
    {
        gameManager = GameObject.Find("GameManager_Container").GetComponent<GameManager>();
        audioManager = GameObject.Find("AudioSystem").GetComponent<AudioManager>();
    }
}