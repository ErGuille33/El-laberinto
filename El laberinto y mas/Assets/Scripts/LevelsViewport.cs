using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelsViewport : MonoBehaviour
{
    void Update()
    {
        if(GameManager._instance.state != GameManager.State.LEV) 
        {
            transform.GetChild(0).gameObject.SetActive(false);
        } else transform.GetChild(0).gameObject.SetActive(true);
    }
}
