using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //public GameObject playerOneInterface, playerTwoInterface;
    public List<Transform> moleTransforms;
    public int maxSimultaneMoles = 1;
    public float timeToSpawnMole = 2;
    //private bool isPlayerOneTurn = true;
    private GameObject uiScriptContainer;
    //private TurnManager turnManager;
    private KeyCode[] keyCodeArray;
    private int currentMole, simultaneActiveMole;

    private string stringMole;
    //private List<int> moleListNum = new List<int>();

    private void Awake()
    {
        //    turnManager = new TurnManager();
        //uiScriptContainer = GameObject.Find("uiScriptContainer");
    }
    void Start()
    {
        //uiScriptContainer.GetComponent<UIScript>().UpdatePlayerTurnText(true);
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
    void Update()
    {
        AssignMolePosition();
    }



    //private void UpdateTurn() 
    //{

    //}

    private void AssignMolePosition()
    {
        /* Implemente estos Ifs porque no pude obtener en int el valor de la tecla que este presionando.... Se ve horrible!!! ;( */
        if (Input.GetKey(KeyCode.Keypad0))
            currentMole = 0;
        else if (Input.GetKey(KeyCode.Keypad1))
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

        //stringMole = Input.inputString;
        //currentMole = (int) Input.GetAxis(stringMole);
        //print("Stringmole: "+ stringMole + ". CurrentMole: "+ currentMole);

        if (Input.GetKey(keyCodeArray[currentMole]) && simultaneActiveMole < maxSimultaneMoles)
        {
            new MoleScript(currentMole, moleTransforms[currentMole]);
            moleTransforms.Remove(moleTransforms[currentMole]);
            simultaneActiveMole++;
            StartCoroutine(DestroyMole());
        }
        IEnumerator DestroyMole()
        {
            yield return new WaitForSeconds(timeToSpawnMole);
            moleTransforms.Add(GameObject.Find("Mole " +currentMole).transform);
            Destroy(GameObject.Find("Mole " + currentMole));
        }
    }
}
