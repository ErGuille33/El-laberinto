using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
//Game manager de todo el juego
public class GameManager : MonoBehaviour
{
    public LevelPackage[] levelPackages; 
    public LevelManager levelManager;

    public Text textLevel;
    SaveGame saveGame;

    public int[] packsLevel;
    public int nHints;


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

    }

    // Update is called once per frame
    void Update()
    {
        if (levelManager.finishedLevel)
        {
            levelToPlay++;
            StartNewLevel();
        }
    }
    //Inicio de nuevo nivel
    private void StartNewLevel()
    {
        
        levelManager.setFinishedLevel(false);
        saveGame.saveLevel(nHints, packsLevel);

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

    static GameManager _instance;


}
