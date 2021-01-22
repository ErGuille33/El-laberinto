using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
//Game manager de todo el juego
public class GameManager : MonoBehaviour
{
    public enum State { RUN, PAUSE, PAUSE2, END, PACK, LEV }

    public LevelPackage[] levelPackages;
    public LevelManager levelManager = null;

    SaveGame saveGame;

    public int[] packsLevel;
    public int nHints;

    public State state;

    public int hintsAvaiable;

    public bool iceLevelsToPlay;

    public static int packageNum;
    public static int levelNum;

    void Awake()
    {
        if (_instance != null)
        {
            _instance.levelManager = levelManager;
            DestroyImmediate(gameObject);
            return;
        }
        else _instance = this;

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

        //if (state != State.PACK && state != State.LEV)
        //{
        //    StartNewLevel();
        //    state = State.RUN;
        //}
    }

    void Update()
    {
        if (state != State.PACK && state != State.LEV)
        {
            if (levelManager.finishedLevel)
            {
                if (levelNum - 1 > packsLevel[0])
                    packsLevel[0] = levelNum - 1;
                saveGame.saveLevel(hintsAvaiable, packsLevel);

                state = State.END;
            }
        }
    }

    //Inicio de nuevo nivel
    public void StartNewLevel()
    {
        levelManager.setFinishedLevel(false);
        state = State.RUN;

        levelManager.setNewLevel(levelPackages[packageNum].levels[levelNum]);

        levelManager.setActualHints(0);
        levelManager.startNewLevel(levelPackages[packageNum].isIce);
    }

    public void nextLevel()
    {
        levelNum++;
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
        SceneManager.LoadScene("GameScene");
    }

    public void selectPackage(int num)
    {
        packageNum = num;
        state = State.LEV;
    }

    public int getPackageNum()
    {
        return packageNum;
    }
    public void selectLevel(int num)
    {
        levelNum = num;
        changeScene();
    }

    public State getState() { return state; }

    public static GameManager _instance;


}
