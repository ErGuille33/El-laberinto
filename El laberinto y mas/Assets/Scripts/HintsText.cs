using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HintsText : MonoBehaviour
{
    public GameManager gm;

    void Update()
    {
        GetComponent<Text>().text = gm.hintsAvaiable.ToString();
    }
}
