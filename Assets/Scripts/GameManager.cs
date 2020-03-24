using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameDificult gameDificult;
    public int maxSimultaneMoles = 1;
    public float timeToSpawnMole = 1; 
    public List<Transform> voidMoleTransforms;

    private List<GameObject> moleToDestroy = new List<GameObject>();
    private List<Transform> oldMoles = new List<Transform>();
    private KeyCode[] keyCodeArray;
    private int currentMole = 0, simultaneActiveMole = 0, smasherScore = 0, moleScore = 0;

    private void Awake() 
    {
        GameDifficult();
    }
    private void Start()
    {
        smasherScore = 0;
        if (maxSimultaneMoles == 0)
            maxSimultaneMoles = 1;
        keyCodeArray = new KeyCode[10]
            {
                KeyCode.Keypad0,
                KeyCode.Keypad1,
                KeyCode.Keypad2,
                KeyCode.Keypad3,
                KeyCode.Keypad4,
                KeyCode.Keypad5,
                KeyCode.Keypad6,
                KeyCode.Keypad7,
                KeyCode.Keypad8,
                KeyCode.Keypad9,
            };
    }
    private void Update()
    {
        AssignMole();
        InstanceMole();
        GameDifficult();
    }

    private void InstanceMole() // Funcion que instancia un topo con posicion, cantidad y tiempo
    {
        if (Input.GetKeyDown(keyCodeArray[currentMole]) && simultaneActiveMole < maxSimultaneMoles 
            && currentMole != 0 && !oldMoles.Contains(voidMoleTransforms[currentMole]))
        {
            oldMoles.Add(voidMoleTransforms[currentMole]);
            new MoleScript(currentMole, voidMoleTransforms[currentMole]);
            simultaneActiveMole++;
            moleToDestroy.Add(GameObject.Find("Mole " + currentMole));
            if (simultaneActiveMole == maxSimultaneMoles)
                StartCoroutine(DestroyMole());
        }
        IEnumerator DestroyMole()
        {
            yield return new WaitForSeconds(timeToSpawnMole);
            for (int i = 0; i < moleToDestroy.Count; i++)
            {
                if (moleToDestroy[i] != null)
                {
                    UpdateMoleScore();
                    SaveMoleScore();
                    Destroy(moleToDestroy[i]);
                }
            }
            simultaneActiveMole = 0;
            oldMoles.Clear();
        }
        //Queria hacer mas compacto la atribucion de un valor a las teclas, en las lineas de abajo esta el intento
        /*stringMole = Input.inputString;
        currentMole = (int) Input.GetAxis(stringMole);*/
    }
    private void AssignMole() // La forma por la cual asigno un valor a cada keypad. Quice hacerlo mas compacto (esta en las lineas de arriba lo que queria hacer)
    {
        if (Input.GetKey(KeyCode.Keypad1))
            currentMole = 1;
        else if (Input.GetKey(KeyCode.Keypad2))
            currentMole = 2;
        else if (Input.GetKey(KeyCode.Keypad3))
            currentMole = 3;
        else if (Input.GetKey(KeyCode.Keypad4))
            currentMole = 4;
        else if (Input.GetKey(KeyCode.Keypad5))
            currentMole = 5;
        else if (Input.GetKey(KeyCode.Keypad6))
            currentMole = 6;
        else if (Input.GetKey(KeyCode.Keypad7))
            currentMole = 7;
        else if (Input.GetKey(KeyCode.Keypad8))
            currentMole = 8;
        else if (Input.GetKey(KeyCode.Keypad9))
            currentMole = 9;
        else
            currentMole = 0;
    }
    private void GameDifficult() // Esta funcion permite cambiar la dificultad del juego por unas preestablecidas o personalizarla con sus respectivas restricciones
    {
        if (gameDificult == GameDificult.Easy)
        { maxSimultaneMoles = 3; timeToSpawnMole = 2; }
        else if (gameDificult == GameDificult.Normal)
        { maxSimultaneMoles = 2; timeToSpawnMole = 1; }
        else if (gameDificult == GameDificult.Hard)
        { maxSimultaneMoles = 1; timeToSpawnMole = 0.5f; }
        else if (gameDificult == GameDificult.Personalized)
        {
            if (maxSimultaneMoles > 9 || maxSimultaneMoles < 1)
                maxSimultaneMoles = 9;
            if (timeToSpawnMole > 10 || timeToSpawnMole < 1)
                timeToSpawnMole = 10;
        }
    }

    public int SetSmasherScore() // Funcion que permite utilizar una variable desde otra clase
    {
        return smasherScore;
    }
    public int SetMoleScore() // Funcion que permite utilizar una variable desde otra clase
    {
        return moleScore;
    }
    public void UpdateSmasherScore() // Funcion que actualiza la variable smacherScore
    {
        smasherScore += 10;
    }
    public void UpdateMoleScore() // Funcion que actualiza la variable moleScore
    {
        moleScore += 10;
    }

    public void SaveSmasherScore() // Funcion que guarda las variable de la ultima partida y la mejor del topo
    {
        PlayerPrefs.SetInt("Smasher_Last_Score", smasherScore);
        if (smasherScore > PlayerPrefs.GetInt("Smasher_Best_Score"))
            PlayerPrefs.SetInt("Smasher_Best_Score", smasherScore);
    }
    public void SaveMoleScore()  // Funcion que guarda las variable de la ultima partida y la mejor del topo
    {
        PlayerPrefs.SetInt("Mole_Last_Score", moleScore);
        if (moleScore > PlayerPrefs.GetInt("Mole_Best_Score"))
            PlayerPrefs.SetInt("Mole_Best_Score", moleScore);
    }
}
