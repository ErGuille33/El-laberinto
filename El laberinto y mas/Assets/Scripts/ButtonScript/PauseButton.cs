using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseButton : MonoBehaviour
{
    public GameManager gm;
    void Start()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(pause);
    }
    public void pause()
    {
        gm.pause();
    }
}
