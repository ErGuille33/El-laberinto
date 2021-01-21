using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelButton : MonoBehaviour
{
    public int num;

    void Start()
    {
        gameObject.GetComponent<Button>().GetComponentInChildren<Text>().text = num.ToString();
        gameObject.GetComponent<Button>().onClick.AddListener(selectPackage);
    }

    void selectPackage()
    {
        GameManager._instance.selectLevel(num);
    }
}
