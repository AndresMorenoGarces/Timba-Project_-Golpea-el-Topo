using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class UIScript : MonoBehaviour {
    public TMP_Dropdown difficultDroptown;
    public TextMeshProUGUI smasherScore, smasherLastScore, smasherBestScore, moleScore, moleLastScore, moleBestScore;
    public TMP_InputField limitSimultaneMoleTextInt, timeToSpawnMoleTextInt;
    private GameManager gameManager;

    private void Awake(){
        gameManager = GameObject.Find("GameManager_Container").GetComponent<GameManager>();
    }
    private void Start() {
        moleLastScore.text = "" + PlayerPrefs.GetInt("Mole_Last_Score");
        smasherLastScore.text = "" + PlayerPrefs.GetInt("Smasher_Last_Score");
        PopulateList();
    }
    private void Update() {
        SmasherScoreText();
        MoleScoreText();
    }
    private void OnEnable() {
        GameManager.OnUIShow += ShowMoleValues;
        GameManager.OnUIMoleLimitsInteract += ModifyMoleLimits;
        GameManager.OnUITimeToSpawnInteract += ModifyTimeToSpawn;
    }
    private void OnDisable() {
        GameManager.OnUIShow -= ShowMoleValues;
        GameManager.OnUIMoleLimitsInteract -= ModifyMoleLimits;
        GameManager.OnUIMoleLimitsInteract -= ModifyMoleLimits;
        GameManager.OnUITimeToSpawnInteract += ModifyTimeToSpawn;
    }

    public void DroptownIndexer(int index) { // Funcion que me permite asignar un valor al enum segun lo que se establezca desde Game 
        gameManager.GetComponent<GameManager>().gameDificult = (GameDificult)index;
    }
    private int ModifyMoleLimits() {
        if (int.TryParse(limitSimultaneMoleTextInt.text, out int moleNumberParsed))
            return moleNumberParsed;
        else
            return 1;
    }
    private float ModifyTimeToSpawn() {
        if (int.TryParse(timeToSpawnMoleTextInt.text, out int timeToSpawnParsed))
            return timeToSpawnParsed;
        else
            return 0.5f;
    }
    private void ShowMoleValues(int _limitSimultaneMoles, float _timeToSpawnMole) { // Funcion que obtiene y muestra las variables tipo TMP en Game
        limitSimultaneMoleTextInt.text = _limitSimultaneMoles.ToString(); 
        timeToSpawnMoleTextInt.text = _timeToSpawnMole.ToString(); 
    }
    private void SmasherScoreText() { // Funcion que muestra el texto en game del Smasher (encargado del golpear el topo)
        smasherScore.text = "" + gameManager.GetComponent<GameManager>().SetEntyScore(false);
        smasherBestScore.text = "" + PlayerPrefs.GetInt("Smasher_Best_Score");
    }
    private void MoleScoreText() { // Funcion que muestra el texto en game del topo
        moleScore.text = "" + gameManager.GetComponent<GameManager>().SetEntyScore(true);
        moleBestScore.text = "" + PlayerPrefs.GetInt("Mole_Best_Score");
    }
    private void PopulateList() { 
        string[] enumNames = Enum.GetNames(typeof(GameDificult));
        List<string> names = new List<string>(enumNames);
        difficultDroptown.AddOptions(names);
    }
}