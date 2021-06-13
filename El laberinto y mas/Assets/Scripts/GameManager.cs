using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
//Game manager de todo el juego
public class GameManager : MonoBehaviour
{
    //public enum State { RUN, PAUSE, PAUSE2, END, PACK, LEV, INI }

    [SerializeField]
    private LevelPackage[] levelPackages;

    [SerializeField]
    private GridManager gridManager;

    [SerializeField]
    private LevelManager levelManager = null;

    [SerializeField]
    private GameObject IniMenu;

    SaveGame saveGame;

    [SerializeField]
    private int[] packsLevel;

    [SerializeField]
    private int nHints;

    [SerializeField]
    private int hintsAvaiable;

    [SerializeField]
    private int packageNum;
    [SerializeField]
    private int levelNum;

#if UNITY_EDITOR
    [SerializeField]
    private bool fromMenu;

    [SerializeField]
    private int packageToPlay;

    [SerializeField]
    private int levelToPlay;

    private bool runningGame;
    private bool levelFinished;
#endif

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

        print("Pistas disponibles de guardado: " + hintsAvaiable);
        print("Niveles completados de guardado: " + packsLevel[0]);
    }

    void Update()
    {
        //Solo si esta en el juego
        if (runningGame)
        {
            if (levelManager != null)
            {
                if (levelManager.finishedLevel)
                {
                    if (levelNum > packsLevel[packageNum])
                        packsLevel[packageNum] = levelNum+1;
                    saveGame.saveLevel(hintsAvaiable, packsLevel);

                    if(levelNum >= levelPackages[packageNum].levels.Length)
                    {
                        home();
                    }
                    runningGame = false;
                    levelFinished = true;
                    gridManager.activateGrid(false);
                   
                }
            }
        }
        /*
        else if(state == State.INI)
        {
            IniMenu.SetActive(true);
        }
        else
        {
            IniMenu.SetActive(false);
        }*/
    }

    //Inicio de nuevo nivel
    public void StartNewLevel()
    {
        if (!fromMenu)
        {
            packageNum = packageToPlay;
            levelNum = levelToPlay;
        }

        levelManager.setFinishedLevel(false);

        runningGame = true;
        levelFinished = false;

        gridManager.activateGrid(true);

        levelManager.setNewLevel(levelPackages[packageNum].levels[levelNum]);

        levelManager.setActualHints(0);
        levelManager.startNewLevel(levelPackages[packageNum].isIce);
    }

    public void nextLevel()
    {
        levelToPlay++;
        StartNewLevel();
    }


    public void useHint()
    {
        if (hintsAvaiable > 0)
        {
            if (levelManager.addHints())
            {
                hintsAvaiable -= 1;
            }
        }
    }

    public void buyHint()
    {
        hintsAvaiable += 1;
        saveGame.saveLevel(hintsAvaiable, packsLevel);
    }


    public void home()
    {
        SceneManager.LoadScene("Menu");
    }

    public void changeScene()
    {
        runningGame = true;
        levelFinished = false;

        fromMenu = true;
        SceneManager.LoadScene("GameScene");
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

    public int getLastLevel(int pack)
    {
        return packsLevel[pack];
    }


    public int getLevelNum() { return levelNum; }
    public int getHintsAvaliable() { return hintsAvaiable; }

    public LevelPackage getActualPackage() { return levelPackages[packageNum]; }

    public LevelPackage[] getLevelPackages() { return levelPackages; }

    public bool getRunningGame() { return runningGame; }

    public bool getLevelFinished() { return levelFinished; }

    public int[] getPackLevels() { return packsLevel; }


    public static GameManager _instance;


}
