using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public LevelPackage[] levelPackages; 
    public LevelManager levelManager;
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
        if(iceLevelsToPlay)
            levelManager.setTextAsset(levelPackages[1].levels[levelToPlay]);
        else
            levelManager.setTextAsset(levelPackages[0].levels[levelToPlay]);

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
