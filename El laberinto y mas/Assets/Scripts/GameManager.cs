using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Advertisements;
//Game manager de todo el juego
public class GameManager : MonoBehaviour, IUnityAdsListener
{

    [SerializeField]
    private LevelPackage[] levelPackages;

    [SerializeField]
    private LevelManager levelManager = null;

    SaveGame saveGame;

    [SerializeField]
    private int[] packsLevel;

    [SerializeField]
    private int nHints;

    [SerializeField]
    private HintsText hintsText;

    [SerializeField]
    private TextLevel levelText;


    private int hintsAvaiable;

    private int packageNum;
    private int levelNum;

    private int packageToPlay;
    private int levelToPlay;

    private bool runningGame;
    private bool levelFinished;

    void Awake()
    {
        if (_instance != null)
        {
            _instance.levelManager = levelManager;
            _instance.hintsText = hintsText;
            _instance.levelText = levelText;
            DestroyImmediate(gameObject);
            return;
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }

        saveGame = gameObject.AddComponent<SaveGame>();
        QualitySettings.vSyncCount = 0;   // Deshabilitamos el vSync
        Application.targetFrameRate = 60; // 60 fps, son 60 fps
    }

    private void Start()
    {
        Advertisement.AddListener(this);
        Advertisement.Initialize(gameId, testMode);

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

        hintsText.updateHints(hintsAvaiable);

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
                   
                }
            }
        }
    }

    //Inicio de nuevo nivel
    public void StartNewLevel()
    {
        packageNum = packageToPlay;
        levelNum = levelToPlay;

        levelManager.setFinishedLevel(false);

        runningGame = true;
        levelFinished = false;

        levelManager.setNewLevel(levelPackages[packageNum].levels[levelNum]);

        levelManager.setActualHints(0);
        levelManager.startNewLevel(levelPackages[packageNum].isIce);

        levelText.updateLevelText(levelPackages[packageNum].isIce, levelNum);
        hintsText.updateHints(hintsAvaiable);
    }

    public void nextLevel()
    {
        levelToPlay++;
        ShowRewardedVideo();
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
            hintsText.updateHints(hintsAvaiable);
        }
    }

    public void buyHint()
    {
        hintsAvaiable += 1;
        saveGame.saveLevel(hintsAvaiable, packsLevel);
        hintsText.updateHints(hintsAvaiable);
    }


    public void home()
    {
        SceneManager.LoadScene("Menu");
    }

    public void changeScene()
    {
        runningGame = true;
        levelFinished = false;
        SceneManager.LoadScene("GameScene");
    }

    public void Pause()
    {
        runningGame = false;
    }

    public void Run()
    {
        runningGame = true;
    }

    public int getPackageNum()
    {
        return packageNum;
    }
    public void selectLevel(int pack, int num)
    {
        packageToPlay = pack;
        levelToPlay = num;
        changeScene();
    }

    public int getLastLevel(int pack)
    {
        return packsLevel[pack];
    }


    public int getLevelNum() { return levelNum; }

    public LevelPackage getActualPackage() { return levelPackages[packageNum]; }

    public LevelPackage[] getLevelPackages() { return levelPackages; }

    public bool getRunningGame() { return runningGame; }

    public bool getLevelFinished() { return levelFinished; }

    public int[] getPackLevels() { return packsLevel; }

    string gameId = "3974381";
    string myPlacementId = "rewardedVideo";
    bool testMode = true;

    public void ShowRewardedVideo()
    {
        if (Advertisement.IsReady(myPlacementId))
        {
            Advertisement.Show(myPlacementId);
        }
        else
        {
            Debug.Log("No esta ready hermano");
        }
    }

    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
        //
    }

    public void OnUnityAdsReady(string placementId)
    {
        if (placementId == myPlacementId)
        {
            // Para cuando ya puedes usar el boton de anuncio
        }
    }


    public void OnUnityAdsDidError(string message)
    {
        print("Ha habido un error con el anuncio");
    }

    public void OnUnityAdsDidStart(string placementId)
    {
        // Cosas opcionales que puedes hacer cuando el anuncio empieza
    }

    public void OnDestroy()
    {
        Advertisement.RemoveListener(this);
    }

    public static GameManager _instance;


}
