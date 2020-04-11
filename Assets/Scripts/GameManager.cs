using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class GameManager : MonoBehaviour { 
    /*Delegados y eventos*/
    public delegate int _OnUIMoleLimitsInteract();
    public static event _OnUIMoleLimitsInteract OnUIMoleLimitsInteract;
    public delegate float _OnUITimeToSpawnInteract();
    public static event _OnUITimeToSpawnInteract OnUITimeToSpawnInteract;
    public delegate void _OnUIShow(int _limitSimultaneMoles, float _timeToSpawnMole);
    public static event _OnUIShow OnUIShow;

    public GameDificult gameDificult;
    public int limitSimultaneMoles = 1;
    public float timeToSpawnMole = 1;
    public List<Transform> voidMoleTransforms;

    private int currentMole = 0, currentActiveMoles = 0, smasherScore = 0, moleScore = 0, entyScore = 0,stringMoleParsed = 0;
    private string stringMole, lastScoreCode, bestScoreCode;
    private bool isMoleValuesSave = false;
    private List<GameObject> moleToDestroy = new List<GameObject>();
    private List<GameObject> xObjectsToDestroy = new List<GameObject>();
    private List<Transform> oldMoles = new List<Transform>();
    private KeyCode[] keyCodeArray;
    private AudioManager audioManager;

    private void Awake() { 
        audioManager = GameObject.Find("AudioSystem").GetComponent<AudioManager>();
        GameDifficult();
    }
    private void Start() { 
        smasherScore = 0;
        if (limitSimultaneMoles == 0)
            limitSimultaneMoles = 1;
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
    private void Update() { 
        AssignMole();
        if (isMoleValuesSave == false)
            InstanceMole();
        GameDifficult();
    }
    private void AssignMole() { // La forma por la cual asigno un valor a cada keypad. 
        stringMole = Input.inputString;
        if (int.TryParse(stringMole, out stringMoleParsed) && int.Parse(stringMole) < 10)
            currentMole = stringMoleParsed;
        else
            return;
    }
    private void InstanceMole() { // Funcion que instancia un topo con posicion, cantidad y tiempo
        if (Input.GetKeyDown(keyCodeArray[currentMole]) && currentMole != 0 
        && currentActiveMoles < limitSimultaneMoles && !oldMoles.Contains(voidMoleTransforms[currentMole])) {
            currentActiveMoles++;
            new MoleScript(currentMole, voidMoleTransforms[currentMole]);
            oldMoles.Add(voidMoleTransforms[currentMole]);
            moleToDestroy.Add(GameObject.Find("Mole " + currentMole));
            if (currentActiveMoles == limitSimultaneMoles)
                StartCoroutine(DestroyMole());
            audioManager.AppearSound();
        }
    }
    private IEnumerator DestroyMole() {
        yield return new WaitForSeconds(timeToSpawnMole);
        for (int i = 0; i < moleToDestroy.Count; i++) // Provee la correspondiente puntuacion al debido ganador y despues lo borra
            if (moleToDestroy[i] != null) {
                UpdateEntyScore(true);
                SaveEntyScore(true);
                audioManager.WinSound();
                Destroy(moleToDestroy[i]);
            }
        DestroyXObject();
        currentActiveMoles = 0;
        oldMoles.Clear();
    }
    private void DestroyXObject() {
        for (int i = 0; i < xObjectsToDestroy.Count; i++) // borra el game object "X"
            if (xObjectsToDestroy[i] != null)
                Destroy(xObjectsToDestroy[i]);
    }
    private void GameDifficult() { // Esta funcion permite cambiar la dificultad del juego por unas preestablecidas o personalizarla con sus respectivas restricciones
        if (gameDificult == GameDificult.Easy)
        { limitSimultaneMoles = 3; timeToSpawnMole = 2; }
        else if (gameDificult == GameDificult.Normal)
        { limitSimultaneMoles = 2; timeToSpawnMole = 1; }
        else if (gameDificult == GameDificult.Hard)
        { limitSimultaneMoles = 1; timeToSpawnMole = 0.5f; }
        else if (gameDificult == GameDificult.Personalized) {
            if (OnUIMoleLimitsInteract != null && OnUITimeToSpawnInteract != null && OnUIShow != null && isMoleValuesSave) {
                limitSimultaneMoles = OnUIMoleLimitsInteract();
                timeToSpawnMole = OnUITimeToSpawnInteract();

                if (limitSimultaneMoles > 9) limitSimultaneMoles = 9;
                else if (limitSimultaneMoles < 1) limitSimultaneMoles = 1;
                if (timeToSpawnMole > 10) timeToSpawnMole = 10;
                else if (timeToSpawnMole < 0.5f) timeToSpawnMole = 0.5f;

                OnUIShow(limitSimultaneMoles, timeToSpawnMole);
                for (int i = 0; i < moleToDestroy.Count; i++)
                    if (moleToDestroy[i] != null)
                        Destroy(moleToDestroy[i]);
                currentActiveMoles = 0;
                oldMoles.Clear();
                DestroyXObject();
            }
        }
        if (OnUIShow != null && gameDificult != GameDificult.Personalized)
            OnUIShow(limitSimultaneMoles, timeToSpawnMole);
    }

    public void ExitAplicattion() {
        Application.Quit();
    }
    public void SaveMoleValueButton() { // Funcion que permite guardar el valor de topo
        if (isMoleValuesSave == false) { 
            isMoleValuesSave = true;
            StartCoroutine(WaitWhileSave());
        }
        IEnumerator WaitWhileSave() { 
            yield return new WaitForSeconds(1f);
            isMoleValuesSave = false;
        }
    }
    public int SetEntyScore(bool isMoleScore) { // Funcion que permite utilizar una variable desde otra clase
        if (isMoleScore)
            return moleScore;
        else
            return smasherScore;
    }
    public void UpdateEntyScore(bool isMoleScore) { // Funcion que actualiza la variable moleScore 
        if (isMoleScore)
            moleScore += 10;
        else
            smasherScore += 10;
    }
    public void SaveEntyScore(bool isMoleScore) { // Funcion que guarda las variable de la ultima partida y la mejor del topo
        entyScore = (isMoleScore)? moleScore : smasherScore; 
        lastScoreCode = (isMoleScore)? "Mole_Last_Score" : "Smasher_Last_Score"; 
        bestScoreCode = (isMoleScore)? "Mole_Best_Score" : "Smasher_Best_Score"; 
        PlayerPrefs.SetInt(lastScoreCode, entyScore);
        if (moleScore > PlayerPrefs.GetInt(bestScoreCode))
            PlayerPrefs.SetInt(lastScoreCode, entyScore);
    }
    public void ApplyXSprite(GameObject moleObject)  {
        GameObject _xObject = Instantiate((GameObject)Resources.Load("Prefabs/xObject", typeof(GameObject)));
        _xObject.transform.position = moleObject.transform.position;
        xObjectsToDestroy.Add(_xObject);
    }
}