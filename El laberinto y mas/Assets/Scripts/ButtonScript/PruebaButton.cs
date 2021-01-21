using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PruebaButton : MonoBehaviour
{
    public MenuManager mm;
    void Start()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(changeScene);
    }

    void changeScene()
    {
        mm.changeScene();
    }
}
