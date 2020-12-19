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
#endif
    // Start is called before the first frame update
    void Start()
    {
        if(_instance != null)
        {
            _instance.levelManager = levelManager;
            DestroyImmediate(gameObject);
            return;
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
