using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class GameInfo
{
    public static LevelPackage levelPack;
    public static int level;
}

public class MenuManager : MonoBehaviour
{

    public LevelPackage[] levelPacks;
    private LevelPackage retLevPack;
    private int retLev;

    public void changeScene()
    {
        GameInfo.levelPack = levelPacks[0];
        GameInfo.level = 0;
        SceneManager.LoadScene("SampleScene");
    }
}
