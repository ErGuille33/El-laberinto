using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PackagesContent : MonoBehaviour
{
    public GameObject button;
    public GameObject iceButton;
    void Start()
    {
        for(int i = 0; i < GameManager._instance.getLevelPackages().Length; i++)
        {
            GameObject aux;
            if(GameManager._instance.getLevelPackages()[i].isIce)
            {
                aux = Instantiate(iceButton);
            }
            else
            {
                aux = Instantiate(button);
            }
            aux.transform.SetParent(transform)  ;
            //aux.GetComponent<PackageButton>().num = i;
        }
    }
}
