using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public GameObject grid;

    // Update is called once per frame
    public void activateGrid(bool active)
    {
        grid.SetActive(active);
    }
}
