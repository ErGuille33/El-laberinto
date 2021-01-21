﻿using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
//Game manager de todo el juego
public class GameManager : MonoBehaviour
{
    public enum State { RUN, PAUSE, PAUSE2, END, MENU }

    public LevelPackage[] levelPackages;
    public LevelManager levelManager = null;

    SaveGame saveGame;

    public int[] packsLevel;
    public int nHints;

    public State state;

    public int hintsAvaiable;

    public int levelToPlay;
    public bool iceLevelsToPlay;


    void Awake()
    {
        if (_instance != null)
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

        print(hintsAvaiable);
        print(packsLevel[0]);

        if (state != State.MENU)
        {
            StartNewLevel();
            state = State.RUN;
        }
    }

    void Update()
    {
        if (state != State.MENU)
        {
            if (levelManager.finishedLevel)
            {
                if (levelToPlay - 1 > packsLevel[0])
                    packsLevel[0] = levelToPlay - 1;
                saveGame.saveLevel(hintsAvaiable, packsLevel);

                state = State.END;
            }
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
        else {
            
            levelManager.setTextAsset(levelPackages[1].levels[levelToPlay]);
            
        }

            levelManager.setActualHints(0);
            levelManager.startNewLevel(iceLevelsToPlay);
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
        saveGame.saveLevel(hintsAvaiable, packsLevel);

    }

    public void pause()
    {
        state = State.PAUSE2;
    }

    public void home()
    {
        SceneManager.LoadScene("Menu");
    }

    public void changeScene()
    {
        state = State.RUN;
        SceneManager.LoadScene("SampleScene");
    }

    public State getState() { return state; }

    static GameManager _instance;


}
