using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextLevel : MonoBehaviour
{

    [SerializeField]
    Text textComponent;

    public void updateLevelText(bool isIce, int levelNum)
    {
        if (textComponent != null)
        {
            if (isIce)
            {
                textComponent.text = "PISO DE HIELO" + " - " + (levelNum + 1);
            }
            else
            {
                textComponent.text = "CLASICO" + " - " + (levelNum + 1);
            }
        }
    }
}
