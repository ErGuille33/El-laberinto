using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PruebaButton : MonoBehaviour
{
    public GameManager gm;
    void Start()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(changeScene);
    }

    void changeScene()
    {
        gm.changeScene();
    }
}
