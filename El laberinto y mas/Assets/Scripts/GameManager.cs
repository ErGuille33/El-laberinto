using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
//Game manager de todo el juego
public class GameManager : MonoBehaviour
{
    public enum State { RUN, PAUSE }

    public LevelPackage[] levelPackages; 
    public LevelManager levelManager;

    public Text textLevel;
    public Text hintsNum;

    public GameObject panelFin;
    public GameObject panelHint;
    public GameObject grid;
    public GameObject player;

    public static State state;

    private int hintsAvaiable;

#if UNITY_EDITOR
    public int levelToPlay;
    public bool iceLevelsToPlay;
#endif

    void Awake()
    {
        if(_instance != null)
        {
            _instance.levelManager = levelManager;
            StartNewLevel();
            DestroyImmediate(gameObject);
            return;
        }
        StartNewLevel();
        state = State.RUN;
    }

    // Update is called once per frame
    void Update()
    {
        if (levelManager.finishedLevel)
        {
            panelFin.SetActive(true);
            grid.SetActive(false);
            player.SetActive(false);
            state = State.PAUSE;
        }
    }
    //Inicio de nuevo nivel
    private void StartNewLevel()
    {
        levelManager.setFinishedLevel(false);
        state = State.RUN;
        
        if (!iceLevelsToPlay)
        {
            levelManager.setNewLevel(levelPackages[0].levels[levelToPlay]);
        }
        else levelManager.setTextAsset(levelPackages[1].levels[levelToPlay]);
  
        levelManager.startNewLevel();


        if (iceLevelsToPlay)
        {
            levelManager.setTextAsset(levelPackages[1].levels[levelToPlay]);
            textLevel.text = "PISO DE HIELO" + " - " + (levelToPlay + 1);
        }
        else
        {
            levelManager.setTextAsset(levelPackages[0].levels[levelToPlay]);
            textLevel.text = "CLASICO" + " - " + (levelToPlay + 1);
        }
    }

    public void nextLevel()
    {
        levelToPlay++;
        StartNewLevel();
        panelFin.SetActive(false);
        grid.SetActive(true);
        player.SetActive(true);
    }

    public void showHintsPanel()
    {
        panelHint.SetActive(true);
        hintsNum.text = hintsAvaiable.ToString();
        state = State.PAUSE;
    }

    public void hideHintsPanel()
    {
        panelHint.SetActive(false);
        state = State.RUN;
    }

    public void useHint()
    {
        if (hintsAvaiable > 0)
        {
            // colocar pistas
            hintsAvaiable -= 1;
            hideHintsPanel();
            state = State.RUN;
        }
    }

    public void buyHint()
    {
        hintsAvaiable += 1;
        hintsNum.text = hintsAvaiable.ToString();
    }

    static GameManager _instance;


}
