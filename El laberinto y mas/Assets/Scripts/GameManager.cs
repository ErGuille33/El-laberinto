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
    SaveGame saveGame;

    public int[] packsLevel;
    public int nHints;
    public Text hintsNum;

    public GameObject panelFin;
    public GameObject panelHint;
    public GameObject grid;
    public GameObject player;

    public static State state;

    private int hintsAvaiable;

    public int levelToPlay;
    public bool iceLevelsToPlay;


    void Awake()
    {
        if(_instance != null)
        {
            _instance.levelManager = levelManager;

            DestroyImmediate(gameObject);
            return;
        }
        saveGame = gameObject.AddComponent<SaveGame>();

    }

    private void Start()
    {
        int maxSize = 1;
        int auxSize = 0;

        for(int i = 0; i< levelPackages.Length; i++)
        {
            auxSize = levelPackages[i].levels.Length;

            if(auxSize > maxSize)
                maxSize = levelPackages[i].levels.Length;
        }

        saveGame.setPacks(levelPackages.Length);
        saveGame.loadLevels(out nHints, out packsLevel);

        print(nHints);
        print(packsLevel[0]);

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
        saveGame.saveLevel(nHints, packsLevel);

        state = State.RUN;
        
        if (!iceLevelsToPlay)
        {
       
            levelManager.setNewLevel(levelPackages[0].levels[levelToPlay]);
            if(levelToPlay - 1 > packsLevel[0] )
                packsLevel[0] = levelToPlay - 1;
            saveGame.saveLevel(nHints, packsLevel);
            
        }
        else {
            
            levelManager.setTextAsset(levelPackages[1].levels[levelToPlay]);

            if (levelToPlay - 1 > packsLevel[0])
                packsLevel[0] = levelToPlay - 1;
            saveGame.saveLevel(nHints, packsLevel);
            
        }
        
  
        levelManager.startNewLevel(iceLevelsToPlay);


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
