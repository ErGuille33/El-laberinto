using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class NextLevelButton : MonoBehaviour
{
    public void nextLevel()
    {
        GameManager._instance.nextLevel();
    }
}
