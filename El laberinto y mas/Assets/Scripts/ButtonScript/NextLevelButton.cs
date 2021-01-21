using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class NextLevelButton : MonoBehaviour
{

    void Start()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(nextLevel);
    }
    public void nextLevel()
    {
        GameManager._instance.nextLevel();
    }
}
