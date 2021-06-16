using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PauseButton : MonoBehaviour
{
    public void pause()
    {
        GameManager._instance.Pause();
    }
}
