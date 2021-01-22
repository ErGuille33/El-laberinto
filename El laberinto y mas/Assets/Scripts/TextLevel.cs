using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextLevel : MonoBehaviour
{
    void Update()
    {
        if (GameManager._instance.levelPackages[GameManager.packageNum].isIce)
        {
            GetComponent<Text>().text = "PISO DE HIELO" + " - " + (GameManager.levelNum + 1);
        }
        else
        {
            GetComponent<Text>().text = "CLASICO" + " - " + (GameManager.levelNum + 1);
        }
    }
}
