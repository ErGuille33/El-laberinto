using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextLevel : MonoBehaviour
{
    void Update()
    {
        if (GameManager._instance.getActualPackage().isIce)
        {
            GetComponent<Text>().text = "PISO DE HIELO" + " - " + (GameManager._instance.getLevelNum() + 1);
        }
        else
        {
            GetComponent<Text>().text = "CLASICO" + " - " + (GameManager._instance.getLevelNum() + 1);
        }
    }
}
