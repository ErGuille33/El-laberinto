using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HintsText : MonoBehaviour
{

    void Update()
    {
        GetComponent<Text>().text = GameManager._instance.getHintsAvaliable().ToString();
    }
}
