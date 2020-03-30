using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIScript : MonoBehaviour
{
    public TMP_Dropdown difficultDroptown;

    [Header ("SmasherTexts")]
    public TextMeshProUGUI smasherScore, smasherLastScore, smasherBestScore;
    [Header ("MoleTexts")]
    public TextMeshProUGUI moleScore, moleLastScore, moleBestScore;
    [Header("DropDownText")]
    public TextMeshProUGUI limitSimultaneMoleText, timeToSpawnMoleText;

    private GameManager gameManager;

    private void Awake()
    {
        gameManager = GameObject.Find("GameManager_Container").GetComponent<GameManager>();
    }
    private void Start()
    {
        moleLastScore.text = "" + PlayerPrefs.GetInt("Mole_Last_Score");
        smasherLastScore.text = "" + PlayerPrefs.GetInt("Smasher_Last_Score");
        PopulateList();
    }
    private void Update()
    {
        SmasherScoreText();
        MoleScoreText();
        ShowMoleValues();
    }

    public void DroptownIndexer(int index) // Funcion que me permite asignar un valor al enum segun lo que se establezca desde Game
    {
        gameManager.GetComponent<GameManager>().gameDificult = (GameDificult)index;
    }
    private void PopulateList()
    {
        string[] enumNames = Enum.GetNames(typeof(GameDificult));
        List<string> names = new List<string>(enumNames);
        difficultDroptown.AddOptions(names);
    }
    private void ShowMoleValues() // Funcion que obtiene y muestra las variables tipo TMP en Game
    {
        limitSimultaneMoleText.text = "Moles: " + gameManager.SetLimitSimultaneMoles();
        timeToSpawnMoleText.text = "Time: " + gameManager.SetTimeToSpawnMole();
    }

    private void SmasherScoreText() // Funcion que muestra el texto en game del Smasher (encargado del golpear el topo)
    {
        smasherScore.text = "" + gameManager.GetComponent<GameManager>().SetSmasherScore();
        smasherBestScore.text = "" + PlayerPrefs.GetInt("Smasher_Best_Score");
    }
    private void MoleScoreText() // Funcion que muestra el texto en game del topo
    {
        moleScore.text = "" + gameManager.GetComponent<GameManager>().SetMoleScore();
        moleBestScore.text = "" + PlayerPrefs.GetInt("Mole_Best_Score");
    }
}