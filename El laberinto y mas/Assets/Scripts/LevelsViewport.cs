using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelsViewport : MonoBehaviour
{
    private bool created;
    void Update()
    {
        if (GameManager._instance.state != GameManager.State.LEV)
        {
            transform.GetChild(0).gameObject.SetActive(false);
            created = false;
        }
        else
        {
            transform.GetChild(0).gameObject.SetActive(true);
            if(!created)
                transform.GetChild(0).gameObject.GetComponent<LevelsContent>().createChildren();
            created = true;
        }
    }
}
