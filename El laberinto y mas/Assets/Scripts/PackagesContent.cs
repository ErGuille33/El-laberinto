using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PackagesContent : MonoBehaviour
{
    public GameObject button;
    public GameObject iceButton;
    void Start()
    {
        for(int i = 0; i < GameManager._instance.levelPackages.Length; i++)
        {
            GameObject aux;
            if(GameManager._instance.levelPackages[i].isIce)
            {
                aux = Instantiate(iceButton);
            }
            else
            {
                aux = Instantiate(button);
            }
            aux.transform.parent = transform;
            aux.GetComponent<PackageButton>().num = i;
        }
    }
}
