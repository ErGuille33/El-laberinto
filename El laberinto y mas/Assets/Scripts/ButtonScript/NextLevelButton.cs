using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class NextLevelButton : MonoBehaviour
{
    public GameManager gm;

    void Start()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(nextLevel);
    }
    public void nextLevel()
    {
        gm.nextLevel();
    }
}
