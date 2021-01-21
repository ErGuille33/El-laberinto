using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PackageButton : MonoBehaviour
{
    public int num;

    void Start()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(selectPackage);
    }

    void selectPackage()
    {
        GameManager._instance.selectPackage(num);
    }
}
