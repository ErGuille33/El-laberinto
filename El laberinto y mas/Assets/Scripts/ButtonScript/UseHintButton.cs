using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class UseHintButton : MonoBehaviour
{
    public void useHint()
    {
        GameManager._instance.useHint();
    }
}
