using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class MenuBackButton : MonoBehaviour
{
    public LevelsContent levels;
    void Start()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(Back);
    }

    // Update is called once per frame
    void Back()
    {
        levels.eraseChildren();
        if (GameManager._instance.state == GameManager.State.LEV)
        {
            GameManager._instance.state = GameManager.State.PACK;
        }
        else if (GameManager._instance.state == GameManager.State.PACK)
        {
            GameManager._instance.state = GameManager.State.INI;
        }

        if (GameManager._instance.state == GameManager.State.INI)
        {
            gameObject.SetActive(false);
        }
     
    }
}
