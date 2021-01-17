using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public LevelPackage[] levelPackages; 
    public LevelManager levelManager;

    public Text textLevel;



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
    }

    // Update is called once per frame
    void Update()
    {
        if (levelManager.finishedLevel)
        {
            StartNewLevel();
        }
    }

    private void StartNewLevel()
    {
        print("fasfdsa");
        levelManager.setFinishedLevel(false);
        levelToPlay++;
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

    static GameManager _instance;


}
