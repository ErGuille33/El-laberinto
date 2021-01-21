using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public GameObject grid;
    public GameManager gm;

    // Update is called once per frame
    void Update()
    {
        if (gm.getState() == GameManager.State.END)
        {
            grid.SetActive(false);
        }
        else grid.SetActive(true);
    }
}
