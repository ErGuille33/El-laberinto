using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelPause : MonoBehaviour
{
    public GameManager gm;
    void Update()
    {
        if (gm.getState() == GameManager.State.PAUSE2)
        {
            gameObject.GetComponent<Image>().enabled = true;
            transform.GetChild(0).gameObject.SetActive(true);
        }
        else
        {
            gameObject.GetComponent<Image>().enabled = false;
            transform.GetChild(0).gameObject.SetActive(false);
        }
    }
}
