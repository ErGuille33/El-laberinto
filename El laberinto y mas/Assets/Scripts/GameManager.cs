using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
//Game manager de todo el juego
public class GameManager : MonoBehaviour
{
    public enum State { RUN, PAUSE, PAUSE2, END }

    public LevelPackage[] levelPackages; 
    public LevelManager levelManager;

    SaveGame saveGame;

    public int[] packsLevel;
    public int nHints;

    private State state;

    public int hintsAvaiable;

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
        QualitySettings.vSyncCount = 0;   // Deshabilitamos el vSync
        Application.targetFrameRate = 60; // Forzamos un máximo de 15 fps.
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
        saveGame.loadLevels(out hintsAvaiable, out packsLevel);

        StartNewLevel();

        state = State.RUN;
    }

    void Update()
    {
        if (levelManager.finishedLevel)
        {
            state = State.END;
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
        }
        else
        {
            levelManager.setTextAsset(levelPackages[0].levels[levelToPlay]);
        }
    }

    public void nextLevel()
    {
        levelToPlay++;
        StartNewLevel();
    }

    public void showHintsPanel()
    {
        state = State.PAUSE;
    }

    public void hideHintsPanel()
    {
        state = State.RUN;
    }

    public void useHint()
    {
        if (hintsAvaiable > 0)
        {
            if (levelManager.addHints())
            {
                hintsAvaiable -= 1;
            }
            hideHintsPanel();
        }
    }

    public void buyHint()
    {
        hintsAvaiable += 1;
    }

    public void pause()
    {
        state = State.PAUSE2;
    }

    public void home()
    {
        SceneManager.LoadScene("Menu");
    }

    public State getState() { return state; }

    static GameManager _instance;


}
