﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HomeButton : MonoBehaviour
{
    public GameManager gm;
    void Start()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(home);
    }
    public void home()
    {
        gm.home();
    }
}
