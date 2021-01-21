﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelsContent : MonoBehaviour
{
    public GameObject button;
    public GameObject iceButton;
    void Start()
    {
        for (int i = 0; i < GameManager._instance.levelPackages[GameManager._instance.getPackageNum()].levels.Length; i++)
        {
            GameObject aux;
            if (GameManager._instance.levelPackages[GameManager._instance.getPackageNum()].isIce)
            {
                aux = Instantiate(iceButton);
            }
            else
            {
                aux = Instantiate(button);
            }
            aux.transform.parent = transform;
            aux.GetComponent<LevelButton>().num = i;
        }
    }
}
