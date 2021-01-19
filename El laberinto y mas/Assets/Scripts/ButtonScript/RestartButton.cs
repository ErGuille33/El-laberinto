﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class RestartButton : MonoBehaviour
{
    public Button restartButton;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(restartLevel);
    }

    // Update is called once per frame
    private void restartLevel()
    {
        
    }
}