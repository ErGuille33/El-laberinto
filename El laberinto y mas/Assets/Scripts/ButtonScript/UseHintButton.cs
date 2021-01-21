using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class UseHintButton : MonoBehaviour
{
    public GameManager gm;
    public void useHint()
    {
        gm.useHint();
    }
}
