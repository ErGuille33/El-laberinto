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
            DestroyImmediate(gameObject);
            return;
        }
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

    // Update is called once per frame
    void Update()
    {
        
    }

    private void StartNewScene()
    {
        if (levelManager)
        {
            //LAnzar el nivel
        }
    }

    static GameManager _instance;


}
