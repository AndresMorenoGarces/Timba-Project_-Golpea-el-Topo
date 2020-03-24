using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIScript : MonoBehaviour
{
    [Header ("SmasherTexts")]
    public TextMeshProUGUI smasherScore;
    public TextMeshProUGUI smasherLastScore;
    public TextMeshProUGUI smasherBestScore;

    [Header ("MoleTexts")]
    public TextMeshProUGUI moleScore;
    public TextMeshProUGUI moleLastScore;
    public TextMeshProUGUI moleBestScore;

    private GameManager gameManager;

    private void SmasherScoreText() 
    {
        smasherScore.text = "" + gameManager.GetComponent<GameManager>().SetSmasherScore();
        smasherBestScore.text = "" + PlayerPrefs.GetInt("Smasher_Best_Score");
    }
    private void MoleScoreText() 
    {
        moleScore.text = "" + gameManager.GetComponent<GameManager>().SetMoleScore();
        moleBestScore.text = "" + PlayerPrefs.GetInt("Mole_Best_Score");
    }
    private void Awake()
    {
        gameManager = GameObject.Find("GameManager_Container").GetComponent<GameManager>();
    }
    private void Start()
    {
        moleLastScore.text = "" + PlayerPrefs.GetInt("Mole_Last_Score");
        smasherLastScore.text = "" + PlayerPrefs.GetInt("Smasher_Last_Score");
    }
    private void Update()
    {
        SmasherScoreText();
        MoleScoreText();
    }
}
