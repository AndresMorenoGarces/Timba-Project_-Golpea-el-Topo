using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIScript : MonoBehaviour
{
    public TMP_Dropdown difficultDroptown;
    //public TextMeshProUGUI difficultDroptownText;
    [Header ("SmasherTexts")]
    public TextMeshProUGUI smasherScore;
    public TextMeshProUGUI smasherLastScore;
    public TextMeshProUGUI smasherBestScore;

    [Header ("MoleTexts")]
    public TextMeshProUGUI moleScore;
    public TextMeshProUGUI moleLastScore;
    public TextMeshProUGUI moleBestScore;

    private GameDificult gameDificult;
    private GameManager gameManager;

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

    public void DroptownIndexer(int index)
    {
        gameManager.GetComponent<GameManager>().gameDificult = (GameDificult)index;
        //difficultDroptownText.text = gameDificult.ToString();
    }
    private void PopulateList() 
    {
        string[] enumNames = Enum.GetNames(typeof(GameDificult));
        List<string> names = new List<string>(enumNames);
        difficultDroptown.AddOptions(names);
    }
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
    }
}
