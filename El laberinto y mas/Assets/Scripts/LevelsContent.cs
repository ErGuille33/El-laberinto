using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelsContent : MonoBehaviour
{
    public GameObject button;
    public GameObject iceButton;
    public GameObject candado;

    public void createChildren()
    {
        eraseChildren();
        for (int i = 0; i < GameManager._instance.levelPackages[GameManager._instance.getPackageNum()].levels.Length; i++)
        {
            if (i == 0 || i <= GameManager._instance.getLastLevel(GameManager._instance.getPackageNum()))
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
                aux.transform.SetParent(transform) ;
                aux.GetComponent<LevelButton>().num = i;
            } else
            {
                GameObject aux = Instantiate(candado);
                aux.transform.SetParent(transform);
            }
        }
    }

    public void eraseChildren()
    {
        foreach (Transform child in transform) Destroy(child.gameObject);
    }
}
