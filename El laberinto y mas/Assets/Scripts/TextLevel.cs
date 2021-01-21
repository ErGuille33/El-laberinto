using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextLevel : MonoBehaviour
{
    public GameManager gm;
    void Update()
    {
        if (gm.iceLevelsToPlay)
        {
            GetComponent<Text>().text = "PISO DE HIELO" + " - " + (gm.levelToPlay + 1);
        }
        else
        {
            GetComponent<Text>().text = "CLASICO" + " - " + (gm.levelToPlay + 1);
        }
    }
}
