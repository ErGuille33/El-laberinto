using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PackagesViewport : MonoBehaviour
{
    void Update()
    {
        if (GameManager._instance.state != GameManager.State.PACK)
        {
            transform.GetChild(0).gameObject.SetActive(false);
        }
        else transform.GetChild(0).gameObject.SetActive(true);
    }
}
