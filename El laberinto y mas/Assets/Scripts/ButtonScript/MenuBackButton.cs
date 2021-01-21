using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class MenuBackButton : MonoBehaviour
{
    // Start is called before the first frame update
    GameManager gm;
    void Start()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(Back);
    }

    // Update is called once per frame
    void Back()
    {
        if(GameManager._instance.state == GameManager.State.LEV)
        {
            GameManager._instance.state = GameManager.State.PACK;
        }
    }
}
